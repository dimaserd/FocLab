using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Model.Migrations
{
    public partial class UpdateCore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInternal",
                schema: "Store",
                table: "LoggedApplicationAction");

            migrationBuilder.RenameColumn(
                name: "GroupName",
                schema: "Store",
                table: "LoggedUserInterfaceAction",
                newName: "ParametersJson");

            migrationBuilder.RenameColumn(
                name: "StackTrace",
                schema: "Store",
                table: "LoggedApplicationAction",
                newName: "ParametersJson");

            migrationBuilder.AddColumn<string>(
                name: "EventId",
                schema: "Store",
                table: "LoggedUserInterfaceAction",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                schema: "Store",
                table: "LoggedApplicationAction",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventId",
                schema: "Store",
                table: "LoggedApplicationAction",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExceptionStackTrace",
                schema: "Store",
                table: "LoggedApplicationAction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "Store",
                table: "LoggedUserInterfaceAction");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "Store",
                table: "LoggedApplicationAction");

            migrationBuilder.DropColumn(
                name: "ExceptionStackTrace",
                schema: "Store",
                table: "LoggedApplicationAction");

            migrationBuilder.RenameColumn(
                name: "ParametersJson",
                schema: "Store",
                table: "LoggedUserInterfaceAction",
                newName: "GroupName");

            migrationBuilder.RenameColumn(
                name: "ParametersJson",
                schema: "Store",
                table: "LoggedApplicationAction",
                newName: "StackTrace");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                schema: "Store",
                table: "LoggedApplicationAction",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInternal",
                schema: "Store",
                table: "LoggedApplicationAction",
                nullable: false,
                defaultValue: false);
        }
    }
}
