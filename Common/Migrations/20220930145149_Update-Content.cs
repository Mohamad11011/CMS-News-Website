using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.Migrations
{
    public partial class UpdateContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_ContentCategory_ContentCategoryCategoryId",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "ContentCategoryCategoryId",
                table: "Post",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_ContentCategoryCategoryId",
                table: "Post",
                newName: "IX_Post_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_ContentCategory_CategoryId",
                table: "Post",
                column: "CategoryId",
                principalTable: "ContentCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_ContentCategory_CategoryId",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Post",
                newName: "ContentCategoryCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_CategoryId",
                table: "Post",
                newName: "IX_Post_ContentCategoryCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_ContentCategory_ContentCategoryCategoryId",
                table: "Post",
                column: "ContentCategoryCategoryId",
                principalTable: "ContentCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
