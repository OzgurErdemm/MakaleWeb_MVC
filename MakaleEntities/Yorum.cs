using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakaleEntities
{
    [Table("Yorum")]
    public class Yorum:BaseClass
    {
        [Required,StringLength(300)]
        public string Text { get; set; }

        public virtual Makale Makale { get; set; }
        public virtual Kullanici Kullanici { get; set; }
    }
}
