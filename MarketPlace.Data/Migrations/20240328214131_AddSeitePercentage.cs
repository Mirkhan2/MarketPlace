using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketPlace.Data.Migrations
{
    public partial class AddSeitePercentage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerWallets_SellerWallets_SellerWalletId",
                table: "SellerWallets");

            migrationBuilder.DropIndex(
                name: "IX_SellerWallets_SellerWalletId",
                table: "SellerWallets");

            migrationBuilder.DropColumn(
                name: "SellerWalletId",
                table: "SellerWallets");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SellerWallets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteProfit",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SellerWallets");

            migrationBuilder.DropColumn(
                name: "SiteProfit",
                table: "Products");

            migrationBuilder.AddColumn<long>(
                name: "SellerWalletId",
                table: "SellerWallets",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SellerWallets_SellerWalletId",
                table: "SellerWallets",
                column: "SellerWalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerWallets_SellerWallets_SellerWalletId",
                table: "SellerWallets",
                column: "SellerWalletId",
                principalTable: "SellerWallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
