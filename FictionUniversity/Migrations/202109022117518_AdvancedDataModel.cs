namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvancedDataModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        Title = c.String(maxLength: 50),
                        Credits = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(maxLength: 60),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        EstablishedDate = c.DateTime(nullable: false),
                        TeacherID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Teacher", t => t.TeacherID)
                .Index(t => t.TeacherID);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 60),
                        FirstName = c.String(nullable: false, maxLength: 60),
                        EmploymentDate = c.DateTime(nullable: false),
                        Address = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Office",
                c => new
                    {
                        TeacherID = c.Int(nullable: false),
                        OfficeLocation = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.TeacherID)
                .ForeignKey("dbo.Teacher", t => t.TeacherID)
                .Index(t => t.TeacherID);
            
            CreateTable(
                "dbo.Registration",
                c => new
                    {
                        RegistrationID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        Grade = c.Int(),
                    })
                .PrimaryKey(t => t.RegistrationID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 60),
                        FirstName = c.String(nullable: false, maxLength: 60),
                        Address = c.String(maxLength: 60),
                        RegistrationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourseTeacher",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        TeacherID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseID, t.TeacherID })
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Teacher", t => t.TeacherID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.TeacherID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseTeacher", "TeacherID", "dbo.Teacher");
            DropForeignKey("dbo.CourseTeacher", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Registration", "StudentID", "dbo.Student");
            DropForeignKey("dbo.Registration", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Department", "TeacherID", "dbo.Teacher");
            DropForeignKey("dbo.Office", "TeacherID", "dbo.Teacher");
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department");
            DropIndex("dbo.CourseTeacher", new[] { "TeacherID" });
            DropIndex("dbo.CourseTeacher", new[] { "CourseID" });
            DropIndex("dbo.Registration", new[] { "StudentID" });
            DropIndex("dbo.Registration", new[] { "CourseID" });
            DropIndex("dbo.Office", new[] { "TeacherID" });
            DropIndex("dbo.Department", new[] { "TeacherID" });
            DropIndex("dbo.Course", new[] { "DepartmentID" });
            DropTable("dbo.CourseTeacher");
            DropTable("dbo.Student");
            DropTable("dbo.Registration");
            DropTable("dbo.Office");
            DropTable("dbo.Teacher");
            DropTable("dbo.Department");
            DropTable("dbo.Course");
        }
    }
}
