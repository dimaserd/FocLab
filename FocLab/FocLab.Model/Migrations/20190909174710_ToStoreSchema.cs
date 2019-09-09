using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Model.Migrations
{
    public partial class ToStoreSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuditLogs",
                table: "AuditLogs");

            migrationBuilder.RenameTable(
                name: "AuditLogs",
                newName: "AuditLog",
                newSchema: "Store");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuditLog",
                schema: "Store",
                table: "AuditLog",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuditLog",
                schema: "Store",
                table: "AuditLog");

            migrationBuilder.RenameTable(
                name: "AuditLog",
                schema: "Store",
                newName: "AuditLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuditLogs",
                table: "AuditLogs",
                column: "Id");
        }
    }
}
