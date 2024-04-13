using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnanasMVCWebApp.Migrations
{
    public partial class EditShippingInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ShippingInfos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingInfos",
                table: "ShippingInfos",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingInfos",
                table: "ShippingInfos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShippingInfos");
        }
    }
}
