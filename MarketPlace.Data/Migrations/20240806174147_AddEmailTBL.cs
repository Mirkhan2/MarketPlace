using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketPlace.Data.Migrations
{
    public partial class AddEmailTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnableSSL = table.Column<bool>(type: "bit", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Port = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EmailSettings",
                columns: new[] { "Id", "CreateDate", "DisplayName", "EnableSSL", "From", "IsDefault", "IsDelete", "LastUpdateDate", "Password", "Port", "SMTP" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Junko", true, "nymasteam@gmail.com", true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "fuqijttnofjradmh", 587, "smtp.gmail.com" });

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "AboutUs", "Address", "CopyRight", "CreateDate", "Email", "FooterText", "IsDefault", "IsDelete", "LastUpdateDate", "MapScript", "Mobile", "Phone" },
                values: new object[] { 1L, null, "Iran", "mirkhan shams", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nymasteam@gmail.com", "nymasteam@gmail.com", true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "null", "09146788445", "04442248066" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailSettings");

            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
