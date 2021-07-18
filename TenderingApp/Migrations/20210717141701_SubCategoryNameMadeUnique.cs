using Microsoft.EntityFrameworkCore.Migrations;

namespace TenderingApp.Migrations
{
    public partial class SubCategoryNameMadeUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_SubCategoryName",
                table: "SubCategory",
                column: "SubCategoryName",
                unique: true,
                filter: "[SubCategoryName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubCategory_SubCategoryName",
                table: "SubCategory");
        }
    }
}
