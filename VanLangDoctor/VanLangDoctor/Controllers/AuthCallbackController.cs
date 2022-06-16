using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Auth.OAuth2.Responses;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using VanLangDoctor.Areas.Admin;

namespace VanLangDoctor.Controllers
{
    public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {
        protected override FlowMetadata FlowData => new AppFlowMetadata(Server.MapPath("~/Content/") + "token.json");

        public override Task<ActionResult> IndexAsync(AuthorizationCodeResponseUrl authorizationCode, CancellationToken taskCancellationToken)
        {

            return base.IndexAsync(authorizationCode, taskCancellationToken);
        }
    }
}