namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        CourseNumber = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        CourseID = c.Guid(nullable: false),
                        GradeNumber = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.CourseID)
                .Index(t => t.CourseID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Grades", "CourseID", "dbo.Courses");
            DropIndex("dbo.Grades", new[] { "CourseID" });
            DropTable("dbo.Grades");
            DropTable("dbo.Courses");
        }
    }
}
