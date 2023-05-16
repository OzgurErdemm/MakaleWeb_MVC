using MakaleBLL;
using MakaleCommon;
using MakaleEntities;
using MakaleEntities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MakaleWeb_MVC.Models;
using System.Data.Entity;
using MakaleWeb_MVC.filter;


namespace MakaleWeb_MVC.Controllers
{
    [Exc]
    public class HomeController : Controller
    {
        MakaleYonet my = new MakaleYonet();
        KategoriYonet ky = new KategoriYonet();
        KullaniciYonet kuly = new KullaniciYonet();
        BegeniYonet by = new BegeniYonet();
        public ActionResult Index()
        {
            // Test test = new Test();
            //  test.EkleTest();
            // test.UpdateTest();
            // test.DeleteTest();
            // test.YorumTest();         
            //int i = 0;
            //int s = 1 / i;
            return View(my.Listele().Where(x=>x.Taslak==false).OrderByDescending(x=>x.DegistirmeTarihi).Take(9).ToList());
        }

        [Auth]
        public ActionResult Begendiklerim()
        {
            var query = by.ListQueryable().Include("Kullanici").Include("Makale").Where(x => x.Kullanici.Id == SessionUser.Login.Id).Select(x => x.Makale).Include("Kategori").Include("Kullanici").OrderByDescending(x => x.DegistirmeTarihi);

            return View("Index", query.ToList());
        }
        public ActionResult Kategori(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Kategori kat = ky.KategoriBul(id.Value);
            
            return View("Index",kat.Makaleler.Where(x=>x.Taslak==false).OrderByDescending(x=>x.DegistirmeTarihi).ToList());
        }

        public ActionResult EnBegenilenler()
        {
            return View("Index",my.Listele().Where(x=>x.Taslak==false).OrderByDescending(x=>x.BegeniSayisi).ToList());
        }

        public ActionResult SonYazilanlar()
        {
            return View("Index", my.Listele().Where(x=>x.Taslak==false).OrderByDescending(x => x.DegistirmeTarihi).ToList());
        }

        public ActionResult Hakkımızda()
        {
            return View();
        }

      
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(LoginModel model)
        {
            if(ModelState.IsValid)
            {
              MakaleBLLSonuc<Kullanici> sonuc=kuly.LoginKontrol(model);

                if(sonuc.hatalar.Count>0)
                {
                    sonuc.hatalar.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);  
                }

               SessionUser.Login = sonuc.nesne;
                Uygulama.login = sonuc.nesne.KullaniciAdi;

                return RedirectToAction("Index");

            }         

            return View(model);
        }

        public ActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KayitOl(RegisterModel model)
        {        

            if(ModelState.IsValid)
            {
              MakaleBLLSonuc<Kullanici> sonuc=kuly.KullaniciKaydet(model);

                if(sonuc.hatalar.Count>0)
                {
                    //ModelState.AddModelError("", "Bu kullanıcı adı yada email kayıtlı");
                    sonuc.hatalar.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);  
                }
                else
                {
                    return RedirectToAction("KayitBasarili");
                }               
          
            }
            return View(model);
        }

        public ActionResult KayitBasarili()
        {
            return View();
        }

        public ActionResult HesapAktiflestir(Guid id)
        {
            MakaleBLLSonuc<Kullanici> sonuc=kuly.ActivateUser(id);
            if(sonuc.hatalar.Count>0)
            {
                TempData["hatalar"] = sonuc.hatalar;
                return RedirectToAction("Error");
            }
          
            return View();  
        }

        public ActionResult Cikis()
        {
            Session.Clear();    
            return RedirectToAction("Index");  
        }

        public ActionResult Error()
        {
            List<string> errors = new List<string>();
            
            if(TempData["hatalar"]!=null)
            {
                ViewBag.hatalar = TempData["hatalar"];
            }
            else
            { ViewBag.hatalar = errors; }
           
            return View(); 
        }

        [Auth]
        public ActionResult ProfilGoster() 
        {
            MakaleBLLSonuc<Kullanici> sonuc = kuly.KullaniciBul(SessionUser.Login.Id);

            if(sonuc.hatalar.Count>0)
            {
                TempData["hatalar"] = sonuc.hatalar;
                return RedirectToAction("Error");
            }

            return View(sonuc.nesne);
        
        }

        [Auth]
        public ActionResult ProfilDegistir()
        {

            MakaleBLLSonuc<Kullanici> sonuc = kuly.KullaniciBul(SessionUser.Login.Id);

            if (sonuc.hatalar.Count > 0)
            {
                TempData["hatalar"] = sonuc.hatalar;
                return RedirectToAction("Error");
            }
            return View(sonuc.nesne);
        }

        [Auth]
        [HttpPost]  
        public ActionResult ProfilDegistir(Kullanici model,HttpPostedFileBase profilresim)
        {
            ModelState.Remove("DegistirenKullanici");

            if(ModelState.IsValid) 
            {
                if (profilresim != null && (profilresim.ContentType == "image/jpg" || profilresim.ContentType == "image/jpeg" || profilresim.ContentType == "image/png"))
                {
                    string dosya = $"user_{model.Id}.{profilresim.ContentType.Split('/')[1]}";

                    profilresim.SaveAs(Server.MapPath($"~/resim/{dosya}"));

                    model.ProfilResimDosyaAdi = dosya;
                }

                Uygulama.login = model.KullaniciAdi;

                MakaleBLLSonuc<Kullanici> sonuc=  kuly.KullaniciUpdate(model);

                if (sonuc.hatalar.Count > 0)
                {
                    sonuc.hatalar.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

                SessionUser.Login = sonuc.nesne;
                return RedirectToAction("ProfilGoster");
            }
           else
            { 
                return View(model); 
            }          
           
        }

        [Auth]
        public ActionResult ProfilSil()
        {
           MakaleBLLSonuc<Kullanici> sonuc=kuly.KullaniciSil(SessionUser.Login.Id);

            if(sonuc.hatalar.Count>0)
            {
                TempData["hatalar"] = sonuc.hatalar;
                return RedirectToAction("Error");
            }

            Session.Clear();
            return RedirectToAction("Index");
        }


        public ActionResult YetkisizErisim()
        {
            return View();
        }

        public ActionResult HataliIslem()
        {
            return View();
        }

        //public PartialViewResult kategoriPartial()
        //{
        //    KategoriYonet ky = new KategoriYonet();
        //    List<Kategori> liste = ky.Listele();
        //    return PartialView("_PartialPagekat2", liste);
        //}
    }
}