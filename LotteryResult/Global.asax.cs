using LotteryResult.Models;
using LotteryResult.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace LotteryResult
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application.Add("result_collection_stats", new ResultCollectionStats());
        }

        public class ResultCollectionStats
        {
            public List<RewardStatus> currentRewardList { get; set; }
            public int currentRewardOrder { get; set; }
            public reward_type currentReward { get; set; }
            public int currentResultOrder { get; set; }
            public round currentRound { get; set; }

            public class RewardStatus
            {
                public bool isUsed { get; set; }
                public reward_type reward { get; set; }
            }

            public ResultCollectionStats()
            {
                currentResultOrder = 1;
                currentRewardOrder = 1;
            }
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                }
            }
        }
    }
}
