using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.Migrations
{
    public partial class Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentCategoryCategoryId",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VideoLink",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContentCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_ContentCategoryCategoryId",
                table: "Post",
                column: "ContentCategoryCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_ContentCategory_ContentCategoryCategoryId",
                table: "Post",
                column: "ContentCategoryCategoryId",
                principalTable: "ContentCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_ContentCategory_ContentCategoryCategoryId",
                table: "Post");

            migrationBuilder.DropTable(
                name: "ContentCategory");

            migrationBuilder.DropIndex(
                name: "IX_Post_ContentCategoryCategoryId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "ContentCategoryCategoryId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "VideoLink",
                table: "Post");
        }
    }
}
