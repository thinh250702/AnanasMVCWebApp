using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnanasMVCWebApp.Migrations
{
    public partial class EditOrderDetailModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ProductSKUSizeId_ProductSKUProductVariantId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductSKUId",
                table: "OrderDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                columns: new[] { "ProductSKUSizeId", "ProductSKUProductVariantId", "OrderId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "ProductSKUId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                columns: new[] { "ProductSKUId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductSKUSizeId_ProductSKUProductVariantId",
                table: "OrderDetails",
                columns: new[] { "ProductSKUSizeId", "ProductSKUProductVariantId" });
        }
    }
}
