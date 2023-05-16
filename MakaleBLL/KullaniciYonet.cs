using MakaleCommon;
using MakaleDAL;
using MakaleEntities;
using MakaleEntities.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MakaleBLL
{
    public class KullaniciYonet
    {
        Repository<Kullanici> rep_kul=new Repository<Kullanici>();

        public MakaleBLLSonuc<Kullanici> ActivateUser(Guid id)
        {
            MakaleBLLSonuc<Kullanici> sonuc = new MakaleBLLSonuc<Kullanici>();
            sonuc.nesne= rep_kul.Find(x => x.AktifGuid == id);

            if(sonuc.nesne!=null)
            {
                if(sonuc.nesne.Aktif)
                {
                    sonuc.hatalar.Add("Kullanıcı zaten daha önce aktif edilmiştir.");
                }
                else
                {
                    sonuc.nesne.Aktif = true;
                    rep_kul.Update(sonuc.nesne);
                }
            }
            else
            {
                sonuc.hatalar.Add("Aktifleştirilecek kullanıcı bulunamadı");
            }

            return sonuc;
        }

        public MakaleBLLSonuc<Kullanici> KullaniciBul(int id)
        {
            MakaleBLLSonuc<Kullanici> sonuc = new MakaleBLLSonuc<Kullanici>();
            sonuc.nesne = rep_kul.Find(x => x.Id == id);

            if(sonuc.nesne==null)
            {
                sonuc.hatalar.Add("Kullanıcı bulunamadı");
            }

            return sonuc;
        }

        public MakaleBLLSonuc<Kullanici> KullaniciKaydet(RegisterModel model)
        {
            MakaleBLLSonuc<Kullanici> sonuc = new MakaleBLLSonuc<Kullanici>();

            Kullanici nesne = new Kullanici();
            nesne.Email = model.Email;
            nesne.KullaniciAdi=model
                .KullaniciAdi;  

            sonuc = KullaniciKontrol(nesne);

            if (sonuc.hatalar.Count > 0)
            {
                sonuc.nesne = nesne;
                return sonuc;
            }
            else
            {
              int islemsonuc=rep_kul.Insert(new Kullanici()
                {
                      KullaniciAdi=model.KullaniciAdi,  
                      Email=model.Email,    
                      Sifre=model.Sifre ,
                      AktifGuid=Guid.NewGuid(),
                      ProfilResimDosyaAdi="user_1.jpg"
                });

                if(islemsonuc>0)
                {
                    sonuc.nesne= rep_kul.Find(x => x.KullaniciAdi == model.KullaniciAdi && x.Email == model.Email);

                    string siteUrl = ConfigHelper.Get<string>("SiteRootUri");

                    string aktivateUrl = $"{siteUrl}/Home/HesapAktiflestir/{sonuc.nesne.AktifGuid}";
                    //https:/localhost:44325/Home/HesapAktiflestir/dcaad31a-a109-4dca-beba-1bb2c88793de

                    string body = $"Merhaba {sonuc.nesne.Adi} {sonuc.nesne.Soyad} <br /> Hesabınızı aktifleştirmek için <a href='{aktivateUrl}' target='_blank'>tıklayınız</a>";

                    MailHelper.SendMail(body, sonuc.nesne.Email, "Hesap Aktifleştirme");

                }
                return sonuc;
            }           
        }

        public void KullaniciKaydet(Kullanici kullanici)
        {
            //Yazılacak...
        }

        MakaleBLLSonuc<Kullanici> sonuc = new MakaleBLLSonuc<Kullanici>();

        public MakaleBLLSonuc<Kullanici> KullaniciUpdate(Kullanici model)
        {
            sonuc = KullaniciKontrol(model);         

            if (sonuc.hatalar.Count>0)
            {
                sonuc.nesne = model;
                return sonuc;
            }
            else
            {
                sonuc.nesne = rep_kul.Find(x => x.Id == model.Id);

                sonuc.nesne.Adi = model.Adi;
                sonuc.nesne.Soyad = model.Soyad;
                sonuc.nesne.Email = model.Email;
                sonuc.nesne.KullaniciAdi = model.KullaniciAdi; sonuc.nesne.Sifre = model.Sifre;
                sonuc.nesne.ProfilResimDosyaAdi = model.ProfilResimDosyaAdi;

                if (rep_kul.Update(sonuc.nesne) < 1)
                {
                    sonuc.hatalar.Add("Profil bilgileri güncellenemedi");
                }
             return sonuc;
            }     

        }


        public MakaleBLLSonuc<Kullanici> KullaniciKontrol(Kullanici kullanici)
        {
            Kullanici k1 = rep_kul.Find(x => x.Email == kullanici.Email);

            Kullanici k2 = rep_kul.Find(x => x.KullaniciAdi == kullanici.KullaniciAdi);

            if(k1!=null && k1.Id!=kullanici.Id)
            {
                sonuc.hatalar.Add("Bu email adresi kayıtlı");
            }

            if(k2!=null && k2.Id!=kullanici.Id) 
            {
                sonuc.hatalar.Add("Bu kullanıcı adı kayıtlı");
            }

            return sonuc;
        }

        public MakaleBLLSonuc<Kullanici> LoginKontrol(LoginModel model)
        {
            MakaleBLLSonuc<Kullanici> sonuc = new MakaleBLLSonuc<Kullanici>();

           sonuc.nesne=rep_kul.Find(x=>x.KullaniciAdi==model.KullaniciAdi && x.Sifre==model.Sifre);    

            if(sonuc.nesne==null)
            {
                sonuc.hatalar.Add("Kullanıcı adı yada şifre hatalı");
            }
            else
            {
                if(!sonuc.nesne.Aktif)
                {
                    sonuc.hatalar.Add("Kullanıcı aktifleştirilmemiştir.Lütfen e-posta adresinizi kontrol ediniz.");
                }             
            }

            return sonuc;
        }

        public MakaleBLLSonuc<Kullanici> KullaniciSil(int id)
        {
            Kullanici kullanici = rep_kul.Find(x => x.Id == id);
            if(kullanici!=null)
            {
               if(rep_kul.Delete(kullanici)<1)
                {
                    sonuc.hatalar.Add("Kullanıcı silinemedi");
                }
            }
            else
            {
                sonuc.hatalar.Add("Kullanıcı bulunamadı");
            }

            return sonuc;
        }

        public List<Kullanici> KullaniciListesi()
        {
            return rep_kul.Liste();
        }

    }
}
