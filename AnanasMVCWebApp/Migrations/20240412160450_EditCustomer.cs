using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnanasMVCWebApp.Migrations
{
    public partial class EditCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ward",
                table: "AspNetUsers",
                newName: "WardName");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "AspNetUsers",
                newName: "ProvinceName");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "AspNetUsers",
                newName: "DistrictName");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WardCode",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WardCode",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "WardName",
                table: "AspNetUsers",
                newName: "Ward");

            migrationBuilder.RenameColumn(
                name: "ProvinceName",
                table: "AspNetUsers",
                newName: "District");

            migrationBuilder.RenameColumn(
                name: "DistrictName",
                table: "AspNetUsers",
                newName: "City");
        }
    }
}
