using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MakaleWeb_MVC.Models;

namespace MakaleWeb_MVC.filter
{
    public class AuthAdmin : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
           if(SessionUser.Login!=null && SessionUser.Login.Admin==false)
            {
                filterContext.Result = new RedirectResult("/Home/YetkisizErisim");
            }
        }
    }
}