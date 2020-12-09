namespace PasswordWallet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IpAddress = c.String(nullable: false),
                        IncorrectLoginTrials = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUser = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                        IdIpAddress = c.Int(nullable: false),
                        Correct = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IPAddresses", t => t.IdIpAddress, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.IdUser, cascadeDelete: false)
                .Index(t => t.IdUser)
                .Index(t => t.IdIpAddress);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PasswordHash = c.String(nullable: false),
                        Salt = c.String(),
                        IsPasswordStoredAsHash = c.Boolean(nullable: false),
                        IncorrectLogins = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Passwords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PasswordHash = c.String(nullable: false),
                        IdUser = c.Int(nullable: false),
                        WebAddress = c.String(),
                        Description = c.String(),
                        Login = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.IdUser, cascadeDelete: false)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.SharedPasswords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUser = c.Int(nullable: false),
                        IdPassword = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Passwords", t => t.IdPassword, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.IdUser, cascadeDelete: false)
                .Index(t => t.IdUser)
                .Index(t => t.IdPassword);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logins", "IdUser", "dbo.Users");
            DropForeignKey("dbo.Passwords", "IdUser", "dbo.Users");
            DropForeignKey("dbo.SharedPasswords", "IdUser", "dbo.Users");
            DropForeignKey("dbo.SharedPasswords", "IdPassword", "dbo.Passwords");
            DropForeignKey("dbo.Logins", "IdIpAddress", "dbo.IPAddresses");
            DropIndex("dbo.SharedPasswords", new[] { "IdPassword" });
            DropIndex("dbo.SharedPasswords", new[] { "IdUser" });
            DropIndex("dbo.Passwords", new[] { "IdUser" });
            DropIndex("dbo.Logins", new[] { "IdIpAddress" });
            DropIndex("dbo.Logins", new[] { "IdUser" });
            DropTable("dbo.SharedPasswords");
            DropTable("dbo.Passwords");
            DropTable("dbo.Users");
            DropTable("dbo.Logins");
            DropTable("dbo.IPAddresses");
        }
    }
}
