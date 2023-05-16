using MakaleBLL;
using MakaleEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace MakaleWeb_MVC.Models
{
    public class CacheHelper
    {
       public static List<Kategori> KategoriCache()
        {
            var kategoriler=WebCache.Get("kat-cache");

            if(kategoriler == null)
            {
                KategoriYonet ky = new KategoriYonet();
                kategoriler = ky.Listele();
                WebCache.Set("kat-cache", kategoriler, 20, true);
            }

            return kategoriler;
        }

        public static void CacheTemizle()
        {
            WebCache.Remove("kat-cache");
        }

    }
}