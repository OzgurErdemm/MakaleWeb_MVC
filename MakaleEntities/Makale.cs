using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakaleEntities
{
    [Table("Makale")]
    public class Makale:BaseClass
    {
        [Required,StringLength(100),DisplayName("Makale")]
        public string Baslik { get; set; }

        [Required, StringLength(1000)]
        public string Icerik { get; set; }
        public bool Taslak { get; set; }
        public int BegeniSayisi { get; set; }


        public virtual List<Yorum> Yorumlar { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual Kategori Kategori { get; set; }  
        public virtual List<Begeni> Begeniler { get; set; }

        public Makale()
        {
            Yorumlar = new List<Yorum>();
            Begeniler= new List<Begeni>();  
        }
    }
}
