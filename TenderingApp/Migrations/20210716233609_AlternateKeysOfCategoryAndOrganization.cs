using Microsoft.EntityFrameworkCore.Migrations;

namespace TenderingApp.Migrations
{
    public partial class AlternateKeysOfCategoryAndOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_OrganizationId",
                table: "Organization",
                column: "OrganizationId");

            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_CategoryId",
                table: "Category",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_OrganizationId",
                table: "Organization",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryId",
                table: "Category",
                column: "CategoryId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_OrganizationId",
                table: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Organization_OrganizationId",
                table: "Organization");

            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_CategoryId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_CategoryId",
                table: "Category");
        }
    }
}
