using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketPlace.Data.Migrations
{
    public partial class AddAllTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_Users_UserId",
                table: "ContactUs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatagories_ProductCatagories_ParentId",
                table: "ProductCatagories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSelectedCategory_ProductCatagories_ProductCategoryId",
                table: "ProductSelectedCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSelectedCategory_Products_ProductId",
                table: "ProductSelectedCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Users_OwnerId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketMessages_Ticket_TicketId",
                table: "TicketMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSelectedCategory",
                table: "ProductSelectedCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCatagories",
                table: "ProductCatagories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactUs",
                table: "ContactUs");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "Tickets");

            migrationBuilder.RenameTable(
                name: "ProductSelectedCategory",
                newName: "ProductSelectedCategories");

            migrationBuilder.RenameTable(
                name: "ProductCatagories",
                newName: "ProductCategories");

            migrationBuilder.RenameTable(
                name: "ContactUs",
                newName: "ContactUses");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_OwnerId",
                table: "Tickets",
                newName: "IX_Tickets_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSelectedCategory_ProductId",
                table: "ProductSelectedCategories",
                newName: "IX_ProductSelectedCategories_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSelectedCategory_ProductCategoryId",
                table: "ProductSelectedCategories",
                newName: "IX_ProductSelectedCategories_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatagories_ParentId",
                table: "ProductCategories",
                newName: "IX_ProductCategories_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactUs_UserId",
                table: "ContactUses",
                newName: "IX_ContactUses_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSelectedCategories",
                table: "ProductSelectedCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactUses",
                table: "ContactUses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductDiscounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscountNumber = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDiscounts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductDiscountUses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductDiscountId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDiscountUses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDiscountUses_ProductDiscounts_ProductDiscountId",
                        column: x => x.ProductDiscountId,
                        principalTable: "ProductDiscounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductDiscountUses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscounts_ProductId",
                table: "ProductDiscounts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscountUses_ProductDiscountId",
                table: "ProductDiscountUses",
                column: "ProductDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscountUses_UserId",
                table: "ProductDiscountUses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUses_Users_UserId",
                table: "ContactUses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_ProductCategories_ParentId",
                table: "ProductCategories",
                column: "ParentId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSelectedCategories_ProductCategories_ProductCategoryId",
                table: "ProductSelectedCategories",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSelectedCategories_Products_ProductId",
                table: "ProductSelectedCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketMessages_Tickets_TicketId",
                table: "TicketMessages",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_OwnerId",
                table: "Tickets",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUses_Users_UserId",
                table: "ContactUses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_ProductCategories_ParentId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSelectedCategories_ProductCategories_ProductCategoryId",
                table: "ProductSelectedCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSelectedCategories_Products_ProductId",
                table: "ProductSelectedCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketMessages_Tickets_TicketId",
                table: "TicketMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_OwnerId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "ProductDiscountUses");

            migrationBuilder.DropTable(
                name: "ProductDiscounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSelectedCategories",
                table: "ProductSelectedCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactUses",
                table: "ContactUses");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Ticket");

            migrationBuilder.RenameTable(
                name: "ProductSelectedCategories",
                newName: "ProductSelectedCategory");

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                newName: "ProductCatagories");

            migrationBuilder.RenameTable(
                name: "ContactUses",
                newName: "ContactUs");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_OwnerId",
                table: "Ticket",
                newName: "IX_Ticket_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSelectedCategories_ProductId",
                table: "ProductSelectedCategory",
                newName: "IX_ProductSelectedCategory_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSelectedCategories_ProductCategoryId",
                table: "ProductSelectedCategory",
                newName: "IX_ProductSelectedCategory_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategories_ParentId",
                table: "ProductCatagories",
                newName: "IX_ProductCatagories_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactUses_UserId",
                table: "ContactUs",
                newName: "IX_ContactUs_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSelectedCategory",
                table: "ProductSelectedCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCatagories",
                table: "ProductCatagories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactUs",
                table: "ContactUs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_Users_UserId",
                table: "ContactUs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatagories_ProductCatagories_ParentId",
                table: "ProductCatagories",
                column: "ParentId",
                principalTable: "ProductCatagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSelectedCategory_ProductCatagories_ProductCategoryId",
                table: "ProductSelectedCategory",
                column: "ProductCategoryId",
                principalTable: "ProductCatagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSelectedCategory_Products_ProductId",
                table: "ProductSelectedCategory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Users_OwnerId",
                table: "Ticket",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketMessages_Ticket_TicketId",
                table: "TicketMessages",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
