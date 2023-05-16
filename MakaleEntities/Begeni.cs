﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakaleEntities
{

    [Table("Begeni")]
    public class Begeni
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int Id { get; set; }

        public virtual Kullanici Kullanici { get; set; }
        public virtual Makale Makale { get; set; }  
    }
}
