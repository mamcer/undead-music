namespace Undead.Music.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Host",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Playlist_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Playlist", t => t.Playlist_Id)
                .Index(t => t.Playlist_Id);
            
            CreateTable(
                "dbo.Playlist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlaylistSong",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.Int(nullable: false),
                        Song_Id = c.Int(),
                        User_Id = c.Int(),
                        Playlist_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Song", t => t.Song_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.Playlist", t => t.Playlist_Id)
                .Index(t => t.Song_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Playlist_Id);
            
            CreateTable(
                "dbo.Song",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Album = c.String(nullable: false, maxLength: 255),
                        Artist = c.String(nullable: false, maxLength: 255),
                        Year = c.Int(),
                        Genre = c.String(maxLength: 255),
                        Duration = c.Time(nullable: false, precision: 7),
                        Artwork = c.Binary(),
                        Bitrate = c.Int(nullable: false),
                        Hash = c.String(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NickName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Host", "Playlist_Id", "dbo.Playlist");
            DropForeignKey("dbo.PlaylistSong", "Playlist_Id", "dbo.Playlist");
            DropForeignKey("dbo.PlaylistSong", "User_Id", "dbo.User");
            DropForeignKey("dbo.PlaylistSong", "Song_Id", "dbo.Song");
            DropIndex("dbo.PlaylistSong", new[] { "Playlist_Id" });
            DropIndex("dbo.PlaylistSong", new[] { "User_Id" });
            DropIndex("dbo.PlaylistSong", new[] { "Song_Id" });
            DropIndex("dbo.Host", new[] { "Playlist_Id" });
            DropTable("dbo.User");
            DropTable("dbo.Song");
            DropTable("dbo.PlaylistSong");
            DropTable("dbo.Playlist");
            DropTable("dbo.Host");
        }
    }
}
