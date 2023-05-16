using MakaleEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MakaleWeb_MVC.Models
{
    public static class SessionUser
    {
        public static Kullanici Login {
            get 
            { 
               if(HttpContext.Current.Session["login"]!=null)
                {
                    return HttpContext.Current.Session["login"] as Kullanici;   
                }
               return null;
            }
            set 
            {
                HttpContext.Current.Session["login"] = value;
            
            } 
        
        }
    }
}