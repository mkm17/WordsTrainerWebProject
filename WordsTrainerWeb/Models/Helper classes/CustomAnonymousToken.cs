using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WordsTrainerWeb.Models.Helper_classes
{
    public class CustomAnonymousToken : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext
        filterContext)
        {
            if (IsUserAuthenticated(filterContext.HttpContext))
            {
                filterContext.Result = new RedirectResult("/Home/Learn");
            }

        }

        private bool IsUserAuthenticated(HttpContextBase context)
        {
            return context.User != null && context.User.Identity != null &&
            context.User.Identity.IsAuthenticated;
        }

    }
}