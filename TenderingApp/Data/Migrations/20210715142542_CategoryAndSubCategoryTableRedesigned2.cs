using Microsoft.EntityFrameworkCore.Migrations;

namespace TenderingApp.Data.Migrations
{
    public partial class CategoryAndSubCategoryTableRedesigned2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Category_CategoryName",
                table: "SubCategory");

            migrationBuilder.DropIndex(
                name: "IX_SubCategory_CategoryName",
                table: "SubCategory");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "SubCategory",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryName1",
                table: "SubCategory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategoryName1",
                table: "SubCategory",
                column: "CategoryName1");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_Category_CategoryName1",
                table: "SubCategory",
                column: "CategoryName1",
                principalTable: "Category",
                principalColumn: "CategoryName",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Category_CategoryName1",
                table: "SubCategory");

            migrationBuilder.DropIndex(
                name: "IX_SubCategory_CategoryName1",
                table: "SubCategory");

            migrationBuilder.DropColumn(
                name: "CategoryName1",
                table: "SubCategory");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "SubCategory",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategoryName",
                table: "SubCategory",
                column: "CategoryName");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_Category_CategoryName",
                table: "SubCategory",
                column: "CategoryName",
                principalTable: "Category",
                principalColumn: "CategoryName",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
