using Microsoft.EntityFrameworkCore.Migrations;

namespace IririApi.Libs.Migrations
{
    public partial class new4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentPlanName",
                table: "PaymentGatewayStores",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentPlanName",
                table: "PaymentGatewayStores");
        }
    }
}
