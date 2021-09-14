using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IririApi.Libs.Migrations
{
    public partial class new3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentPlans",
                columns: table => new
                {
                    PaymentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentPlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cost = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPlans", x => x.PaymentPlanId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentPlans");
        }
    }
}
