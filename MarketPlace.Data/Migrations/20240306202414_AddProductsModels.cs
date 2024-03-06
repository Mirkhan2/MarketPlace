using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketPlace.Data.Migrations
{
    public partial class AddProductsModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Products_ProductId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductAcceptanseState",
                table: "Products",
                newName: "ProductAcceptanceState");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Products",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UrlName",
                table: "ProductCatagories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProductCatagories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "ProductCatagories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductSelectedCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSelectedCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSelectedCategory_ProductCatagories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCatagories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSelectedCategory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerId",
                table: "Products",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatagories_ParentId",
                table: "ProductCatagories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedCategory_ProductCategoryId",
                table: "ProductSelectedCategory",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedCategory_ProductId",
                table: "ProductSelectedCategory",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatagories_ProductCatagories_ParentId",
                table: "ProductCatagories",
                column: "ParentId",
                principalTable: "ProductCatagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sellers_SellerId",
                table: "Products",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatagories_ProductCatagories_ParentId",
                table: "ProductCatagories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sellers_SellerId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductSelectedCategory");

            migrationBuilder.DropIndex(
                name: "IX_Products_SellerId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductCatagories_ParentId",
                table: "ProductCatagories");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ProductCatagories");

            migrationBuilder.RenameColumn(
                name: "ProductAcceptanceState",
                table: "Products",
                newName: "ProductAcceptanseState");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "ProductCategoryId",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UrlName",
                table: "ProductCatagories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProductCatagories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Products_ProductId",
                table: "Products",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
