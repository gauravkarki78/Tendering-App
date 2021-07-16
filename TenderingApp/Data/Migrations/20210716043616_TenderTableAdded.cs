using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TenderingApp.Data.Migrations
{
    public partial class TenderTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tender",
                columns: table => new
                {
                    TenderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefNo = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    NoticeDate = table.Column<DateTime>(nullable: false),
                    TenderDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenderDoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    OrganizationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tender", x => x.TenderId);
                    table.ForeignKey(
                        name: "FK_Tender_Organization_OrganizationName",
                        column: x => x.OrganizationName,
                        principalTable: "Organization",
                        principalColumn: "OrganizationName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tender_OrganizationName",
                table: "Tender",
                column: "OrganizationName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tender");
        }
    }
}
