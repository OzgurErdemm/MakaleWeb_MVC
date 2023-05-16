namespace MakaleDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Begeni",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kullanici_Id = c.Int(),
                        Makale_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanici", t => t.Kullanici_Id)
                .ForeignKey("dbo.Makale", t => t.Makale_Id)
                .Index(t => t.Kullanici_Id)
                .Index(t => t.Makale_Id);
            
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(maxLength: 30),
                        Soyad = c.String(maxLength: 30),
                        KullaniciAdi = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 200),
                        Sifre = c.String(nullable: false, maxLength: 20),
                        Aktif = c.Boolean(nullable: false),
                        AktifGuid = c.Guid(nullable: false),
                        Admin = c.Boolean(nullable: false),
                        KayitTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Makale",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 100),
                        Icerik = c.String(nullable: false, maxLength: 1000),
                        Taslak = c.Boolean(nullable: false),
                        BegeniSayisi = c.Int(nullable: false),
                        KayitTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 30),
                        Kategori_Id = c.Int(),
                        Kullanici_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kategori", t => t.Kategori_Id)
                .ForeignKey("dbo.Kullanici", t => t.Kullanici_Id)
                .Index(t => t.Kategori_Id)
                .Index(t => t.Kullanici_Id);
            
            CreateTable(
                "dbo.Kategori",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 50),
                        Aciklama = c.String(maxLength: 150),
                        KayitTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Yorum",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 300),
                        KayitTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 30),
                        Kullanici_Id = c.Int(),
                        Makale_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanici", t => t.Kullanici_Id)
                .ForeignKey("dbo.Makale", t => t.Makale_Id)
                .Index(t => t.Kullanici_Id)
                .Index(t => t.Makale_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yorum", "Makale_Id", "dbo.Makale");
            DropForeignKey("dbo.Yorum", "Kullanici_Id", "dbo.Kullanici");
            DropForeignKey("dbo.Makale", "Kullanici_Id", "dbo.Kullanici");
            DropForeignKey("dbo.Makale", "Kategori_Id", "dbo.Kategori");
            DropForeignKey("dbo.Begeni", "Makale_Id", "dbo.Makale");
            DropForeignKey("dbo.Begeni", "Kullanici_Id", "dbo.Kullanici");
            DropIndex("dbo.Yorum", new[] { "Makale_Id" });
            DropIndex("dbo.Yorum", new[] { "Kullanici_Id" });
            DropIndex("dbo.Makale", new[] { "Kullanici_Id" });
            DropIndex("dbo.Makale", new[] { "Kategori_Id" });
            DropIndex("dbo.Begeni", new[] { "Makale_Id" });
            DropIndex("dbo.Begeni", new[] { "Kullanici_Id" });
            DropTable("dbo.Yorum");
            DropTable("dbo.Kategori");
            DropTable("dbo.Makale");
            DropTable("dbo.Kullanici");
            DropTable("dbo.Begeni");
        }
    }
}
