using Microsoft.EntityFrameworkCore.Migrations;

namespace TenderingApp.Data.Migrations
{
    public partial class OrganizationTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationName = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    WebUrl = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Vdc = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    WardNo = table.Column<int>(nullable: false),
                    ToleName = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Landmark = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    MapUrl = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    AboutOrg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryName = table.Column<string>(nullable: true),
                    SubCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_Organization_Category_CategoryName",
                        column: x => x.CategoryName,
                        principalTable: "Category",
                        principalColumn: "CategoryName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organization_SubCategory_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organization_CategoryName",
                table: "Organization",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_OrganizationId",
                table: "Organization",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organization_SubCategoryId",
                table: "Organization",
                column: "SubCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Organization");
        }
    }
}
