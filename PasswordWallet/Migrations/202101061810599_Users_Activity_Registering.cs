namespace PasswordWallet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users_Activity_Registering : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataChanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUser = c.Int(nullable: false),
                        IdModifiedRecord = c.Int(nullable: false),
                        PreviousValueOfRecord = c.String(),
                        PresentValueOfRecord = c.String(),
                        Time = c.DateTime(nullable: false),
                        IdActionType = c.Int(nullable: false),
                        IdTableName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActionTypes", t => t.IdActionType, cascadeDelete: true)
                .ForeignKey("dbo.TableNames", t => t.IdTableName, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdUser)
                .Index(t => t.IdActionType)
                .Index(t => t.IdTableName);
            
            CreateTable(
                "dbo.ActionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TableNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TableName = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FunctionRuns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUser = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                        IdFunction = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Functions", t => t.IdFunction, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdUser)
                .Index(t => t.IdFunction);
            
            CreateTable(
                "dbo.Functions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FunctionName = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Passwords", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FunctionRuns", "IdUser", "dbo.Users");
            DropForeignKey("dbo.FunctionRuns", "IdFunction", "dbo.Functions");
            DropForeignKey("dbo.DataChanges", "IdUser", "dbo.Users");
            DropForeignKey("dbo.DataChanges", "IdTableName", "dbo.TableNames");
            DropForeignKey("dbo.DataChanges", "IdActionType", "dbo.ActionTypes");
            DropIndex("dbo.FunctionRuns", new[] { "IdFunction" });
            DropIndex("dbo.FunctionRuns", new[] { "IdUser" });
            DropIndex("dbo.DataChanges", new[] { "IdTableName" });
            DropIndex("dbo.DataChanges", new[] { "IdActionType" });
            DropIndex("dbo.DataChanges", new[] { "IdUser" });
            DropColumn("dbo.Passwords", "Deleted");
            DropColumn("dbo.Users", "Deleted");
            DropTable("dbo.Functions");
            DropTable("dbo.FunctionRuns");
            DropTable("dbo.TableNames");
            DropTable("dbo.ActionTypes");
            DropTable("dbo.DataChanges");
        }
    }
}
