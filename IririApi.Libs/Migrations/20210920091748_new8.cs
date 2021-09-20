using Microsoft.EntityFrameworkCore.Migrations;

namespace IririApi.Libs.Migrations
{
    public partial class new8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isTeaser",
                table: "EventModels",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isTeaser",
                table: "EventModels");
        }
    }
}
