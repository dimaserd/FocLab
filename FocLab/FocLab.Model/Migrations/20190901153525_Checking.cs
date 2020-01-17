using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Model.Migrations
{
    public partial class Checking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                schema: "Store",
                table: "LoggedApplicationAction",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DbFiles",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DayTasks",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DayTaskComments",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ChemistryTasks",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ChemistryTaskReagents",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ChemistryTaskExperiments",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ChemistryTaskExperimentFiles",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ChemistryTaskDbFiles",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ChemistryReagents",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ChemistryMethodFiles",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "AspNetUsers",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionId",
                schema: "Store",
                table: "LoggedApplicationAction");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DbFiles");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DayTasks");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DayTaskComments");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ChemistryTasks");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ChemistryTaskReagents");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ChemistryTaskExperiments");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ChemistryTaskExperimentFiles");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ChemistryTaskDbFiles");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ChemistryReagents");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ChemistryMethodFiles");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
