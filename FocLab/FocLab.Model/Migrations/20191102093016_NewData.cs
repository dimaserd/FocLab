using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Model.Migrations
{
    public partial class NewData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileData",
                table: "DbFiles",
                newName: "Data");

            migrationBuilder.RenameColumn(
                name: "FileData",
                table: "DbFileHistory",
                newName: "Data");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "DbFiles",
                newName: "FileData");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "DbFileHistory",
                newName: "FileData");
        }
    }
}
