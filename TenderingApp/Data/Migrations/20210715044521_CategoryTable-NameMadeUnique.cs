using Microsoft.EntityFrameworkCore.Migrations;

namespace TenderingApp.Data.Migrations
{
    public partial class CategoryTableNameMadeUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryName",
                table: "Category",
                column: "CategoryName",
                unique: true,
                filter: "[CategoryName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Category_CategoryName",
                table: "Category");
        }
    }
}
