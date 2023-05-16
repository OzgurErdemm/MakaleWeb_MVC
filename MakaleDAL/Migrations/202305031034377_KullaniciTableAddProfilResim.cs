namespace MakaleDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KullaniciTableAddProfilResim : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kullanici", "ProfilResimDosyaAdi", c => c.String(maxLength: 30));
            Sql("Update Kullanici set ProfilResimDosyaAdi='user_1.jpg'");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kullanici", "ProfilResimDosyaAdi");
        }
    }
}
