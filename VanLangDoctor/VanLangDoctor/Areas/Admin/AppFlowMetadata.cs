using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
using System;
using System.Web.Mvc;

namespace VanLangDoctor.Areas.Admin
{
    public class AppFlowMetadata : FlowMetadata
    {
        public override IAuthorizationCodeFlow Flow { get { return flow; } }

        private readonly IAuthorizationCodeFlow flow;

        public AppFlowMetadata(string path)
        {
            flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "473600138329-u9jp0j9hmuosn28q5sijlij7nu6q58h8.apps.googleusercontent.com",
                    ClientSecret = "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"
                },
                Scopes = new[] { CalendarService.Scope.Calendar, CalendarService.Scope.CalendarEvents },
                DataStore = new FileDataStore(path, true)
            });
        }

        public override string GetUserId(Controller controller)
        {
            var user = controller.Session["user"];
            if (user == null)
            {
                user = Guid.NewGuid();
                controller.Session["user"] = user;
            }
            return user.ToString();
        }
    }
}