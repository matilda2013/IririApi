using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IririApi.Libs.Migrations
{
    public partial class new5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventPaymentPlans",
                columns: table => new
                {
                    EventPaymentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventPaymentPlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePaid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentPlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    currency = table.Column<string>(type: "varchar(130)", nullable: true),
                    paymentReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gateway_response = table.Column<string>(type: "varchar(130)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    paid_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPaymentPlans", x => x.EventPaymentPlanId);
                });

            migrationBuilder.CreateTable(
                name: "MembershipPlanPayments",
                columns: table => new
                {
                    MemDuePaymentPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentPlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePaid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    currency = table.Column<string>(type: "varchar(130)", nullable: true),
                    paymentReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gateway_response = table.Column<string>(type: "varchar(130)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    paid_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipPlanPayments", x => x.MemDuePaymentPlanId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventPaymentPlans");

            migrationBuilder.DropTable(
                name: "MembershipPlanPayments");
        }
    }
}
