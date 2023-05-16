using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakaleEntities.ViewModel
{
    public class RegisterModel
    {
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(30)]
        public string KullaniciAdi { get; set; }

        [DisplayName("E-posta"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(200),EmailAddress(ErrorMessage ="{0} için geçerli bir eposta adresi giriniz")]
        public string Email { get; set; }


        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(20)]
        public string Sifre { get; set; }

        [DisplayName("Şifre(Tekrar)"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(20),Compare(nameof(Sifre),ErrorMessage ="{0} ile {1} uyuşmuyor")]
        public string Sifre2 { get; set; }
    }
}
