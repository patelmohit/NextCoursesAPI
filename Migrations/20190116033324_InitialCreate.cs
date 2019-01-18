using Microsoft.EntityFrameworkCore.Migrations;

namespace NextCourses.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    course_name = table.Column<string>(nullable: false),
                    course_id = table.Column<string>(nullable: true),
                    subject = table.Column<string>(nullable: true),
                    catalog_number = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.course_name);
                });

            migrationBuilder.CreateTable(
                name: "Prereqs",
                columns: table => new
                {
                    prereq_course_name = table.Column<string>(nullable: false),
                    next_course_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prereqs", x => new { x.prereq_course_name, x.next_course_name });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Prereqs");
        }
    }
}
