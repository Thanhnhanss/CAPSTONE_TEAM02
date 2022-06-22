using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace VanLangDoctor.Controllers
{
    public class WebexAuthController : Controller
    {
        private const string CLIENT_ID = "C962ad74755f90c9b55962bc56d80d96a6922ed7636c733889013f70fab476784";

        private const string CLIENT_SECRECT = "6f1bdf28c779fd26a57ae5e4ec0ad599aeb102bf3303df78fca3f8ca0bda9ef4";

        private const string SCOPE = "meeting%3Aschedules_read%20meeting%3Aschedules_write";

        private const string POST_REDIRECT_URI = "https://localhost:44311/webex/auth-callback";

        [Route("webex/auth")]
        public ActionResult Auth(string redirectUrl = "/")
        {
            redirectUrl = System.Net.WebUtility.UrlEncode(redirectUrl);
            var encodedPostRedirectUrl = System.Net.WebUtility.UrlEncode(POST_REDIRECT_URI);
            var url = $"https://webexapis.com/v1/authorize?response_type=code&client_id={CLIENT_ID}&redirect_uri={encodedPostRedirectUrl}&scope={SCOPE}&state={redirectUrl}";
            return Redirect(url);
        }

        [Route("webex/auth-callback")]
        public async Task<ActionResult> AuthCallbackAsync(string code, string state)
        {
            var httpClient = HttpClientFactory.Create();

            var body = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "client_id", CLIENT_ID },
                { "client_secret", CLIENT_SECRECT },
                { "code", code },
                { "redirect_uri", POST_REDIRECT_URI }
            };
            var content = new FormUrlEncodedContent(body);

            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new System.Uri("https://webexapis.com/v1/access_token"),
                Content = content
            };

            var responseMessage = await httpClient.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode();
            var message = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<WebexAccessTokenResponse>(message);
            Session["ACCESS_TOKEN"] = response.AccessToken;
            return Redirect(state);
        }

        class WebexAccessTokenResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
        }
    }
}