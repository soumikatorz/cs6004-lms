namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        UpdatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ISBN = c.String(),
                        Name = c.String(),
                        PublishedDate = c.DateTime(nullable: false),
                        Charge = c.Double(nullable: false),
                        PenaltyCharge = c.Double(nullable: false),
                        AgeRestricted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Press_ID = c.Guid(),
                        Publisher_ID = c.Guid(),
                        UpdatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Presses", t => t.Press_ID)
                .ForeignKey("dbo.Publishers", t => t.Publisher_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy_Id)
                .Index(t => t.Press_ID)
                .Index(t => t.Publisher_ID)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        UpdatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.BookCopies",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CopyNumber = c.Int(nullable: false),
                        Location = c.String(),
                        Available = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Book_ID = c.Guid(),
                        UpdatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => t.Book_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy_Id)
                .Index(t => t.Book_ID)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        IssuedOn = c.DateTime(nullable: false),
                        ReturnedOn = c.DateTime(),
                        LoanCharge = c.Double(nullable: false),
                        PenaltyCharge = c.Double(),
                        DueDate = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        BookCopy_ID = c.Guid(),
                        LoanedBy_Id = c.String(maxLength: 128),
                        LoanType_ID = c.Guid(),
                        Member_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BookCopies", t => t.BookCopy_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.LoanedBy_Id)
                .ForeignKey("dbo.LoanTypes", t => t.LoanType_ID)
                .ForeignKey("dbo.Members", t => t.Member_ID)
                .Index(t => t.BookCopy_ID)
                .Index(t => t.LoanedBy_Id)
                .Index(t => t.LoanType_ID)
                .Index(t => t.Member_ID);
            
            CreateTable(
                "dbo.LoanTypes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Duration = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        UpdatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Membership_ID = c.Guid(),
                        UpdatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Memberships", t => t.Membership_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy_Id)
                .Index(t => t.Membership_ID)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.Memberships",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        MaxLoans = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        UpdatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.Presses",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        UpdatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Location = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        UpdatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Book_ID = c.Guid(nullable: false),
                        Author_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_ID, t.Author_ID })
                .ForeignKey("dbo.Books", t => t.Book_ID, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.Author_ID, cascadeDelete: true)
                .Index(t => t.Book_ID)
                .Index(t => t.Author_ID);
            
            CreateTable(
                "dbo.CategoryBooks",
                c => new
                    {
                        Category_ID = c.Guid(nullable: false),
                        Book_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_ID, t.Book_ID })
                .ForeignKey("dbo.Categories", t => t.Category_ID, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_ID, cascadeDelete: true)
                .Index(t => t.Category_ID)
                .Index(t => t.Book_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Authors", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Books", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Publishers", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Books", "Publisher_ID", "dbo.Publishers");
            DropForeignKey("dbo.Presses", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Books", "Press_ID", "dbo.Presses");
            DropForeignKey("dbo.BookCopies", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Members", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Memberships", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Members", "Membership_ID", "dbo.Memberships");
            DropForeignKey("dbo.Loans", "Member_ID", "dbo.Members");
            DropForeignKey("dbo.LoanTypes", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Loans", "LoanType_ID", "dbo.LoanTypes");
            DropForeignKey("dbo.Loans", "LoanedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Loans", "BookCopy_ID", "dbo.BookCopies");
            DropForeignKey("dbo.BookCopies", "Book_ID", "dbo.Books");
            DropForeignKey("dbo.Categories", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CategoryBooks", "Book_ID", "dbo.Books");
            DropForeignKey("dbo.CategoryBooks", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.BookAuthors", "Author_ID", "dbo.Authors");
            DropForeignKey("dbo.BookAuthors", "Book_ID", "dbo.Books");
            DropIndex("dbo.CategoryBooks", new[] { "Book_ID" });
            DropIndex("dbo.CategoryBooks", new[] { "Category_ID" });
            DropIndex("dbo.BookAuthors", new[] { "Author_ID" });
            DropIndex("dbo.BookAuthors", new[] { "Book_ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Publishers", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Presses", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Memberships", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Members", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Members", new[] { "Membership_ID" });
            DropIndex("dbo.LoanTypes", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Loans", new[] { "Member_ID" });
            DropIndex("dbo.Loans", new[] { "LoanType_ID" });
            DropIndex("dbo.Loans", new[] { "LoanedBy_Id" });
            DropIndex("dbo.Loans", new[] { "BookCopy_ID" });
            DropIndex("dbo.BookCopies", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.BookCopies", new[] { "Book_ID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Categories", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Books", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Books", new[] { "Publisher_ID" });
            DropIndex("dbo.Books", new[] { "Press_ID" });
            DropIndex("dbo.Authors", new[] { "UpdatedBy_Id" });
            DropTable("dbo.CategoryBooks");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Publishers");
            DropTable("dbo.Presses");
            DropTable("dbo.Memberships");
            DropTable("dbo.Members");
            DropTable("dbo.LoanTypes");
            DropTable("dbo.Loans");
            DropTable("dbo.BookCopies");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Categories");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
