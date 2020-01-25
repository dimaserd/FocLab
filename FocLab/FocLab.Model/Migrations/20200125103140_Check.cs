using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Model.Migrations
{
    public partial class Check : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HandlerId",
                schema: "Store",
                table: "IntegrationMessageStatusLog",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandlerId",
                schema: "Store",
                table: "IntegrationMessageStatusLog");
        }
    }
}