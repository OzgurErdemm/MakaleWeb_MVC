using MakaleEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakaleDAL
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Makale>  Makaleler { get; set; }  
        public DbSet<Kullanici> Kullanicilar { get; set; }  
        public DbSet<Yorum> Yorumlar { get; set; }
        public DbSet<Begeni> Begeniler { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new VeriTabaniOlusturucu());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kategori>().HasMany(k => k.Makaleler).WithRequired(x => x.Kategori).WillCascadeOnDelete(true);

            modelBuilder.Entity<Makale>().HasMany(m => m.Yorumlar).WithRequired(x => x.Makale).WillCascadeOnDelete(true);

            modelBuilder.Entity<Makale>().HasMany(m => m.Begeniler).WithRequired(x => x.Makale).WillCascadeOnDelete(true);

        }



    }
}
