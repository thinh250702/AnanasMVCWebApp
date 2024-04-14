using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnanasMVCWebApp.Migrations
{
    public partial class EditCoupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LimitAmount",
                table: "Coupons",
                newName: "MinimumAmount");

            migrationBuilder.AddColumn<int>(
                name: "Limit",
                table: "Coupons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Limit",
                table: "Coupons");

            migrationBuilder.RenameColumn(
                name: "MinimumAmount",
                table: "Coupons",
                newName: "LimitAmount");
        }
    }
}
