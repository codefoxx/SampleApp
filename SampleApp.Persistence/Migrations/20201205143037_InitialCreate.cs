using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleApp.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Authors",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Authors", x => x.Id); });

            migrationBuilder.CreateTable(
                "Tags",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Tags", x => x.Id); });

            migrationBuilder.CreateTable(
                "Courses",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: true),
                    Description = table.Column<string>("nvarchar(max)", nullable: true),
                    Level = table.Column<int>("int", nullable: false),
                    FullPrice = table.Column<float>("real", nullable: false),
                    AuthorId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        "FK_Courses_Authors_AuthorId",
                        x => x.AuthorId,
                        "Authors",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "CourseTag",
                table => new
                {
                    CoursesId = table.Column<int>("int", nullable: false),
                    TagsId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTag", x => new {x.CoursesId, x.TagsId});
                    table.ForeignKey(
                        "FK_CourseTag_Courses_CoursesId",
                        x => x.CoursesId,
                        "Courses",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_CourseTag_Tags_TagsId",
                        x => x.TagsId,
                        "Tags",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Courses_AuthorId",
                "Courses",
                "AuthorId");

            migrationBuilder.CreateIndex(
                "IX_CourseTag_TagsId",
                "CourseTag",
                "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "CourseTag");

            migrationBuilder.DropTable(
                "Courses");

            migrationBuilder.DropTable(
                "Tags");

            migrationBuilder.DropTable(
                "Authors");
        }
    }
}