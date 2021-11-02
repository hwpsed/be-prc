using Microsoft.EntityFrameworkCore.Migrations;

namespace LaundryApp.Migrations
{
    public partial class UptOrderDe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalAmount",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "UnitCost",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "OrderDetails");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "OrderDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "OrderDetails");

            migrationBuilder.AddColumn<float>(
                name: "FinalAmount",
                table: "OrderDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "UnitCost",
                table: "OrderDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "UnitPrice",
                table: "OrderDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
