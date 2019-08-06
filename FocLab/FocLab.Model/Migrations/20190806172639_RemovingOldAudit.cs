using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Model.Migrations
{
    public partial class RemovingOldAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Snapshot",
                schema: "Store");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "DbFiles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DbFileHistory");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "DbFileHistory");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "DbFileHistory");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "DbFileHistory");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "DbFileHistory");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "DayTasks");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "DayTaskComments");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "ChemistryTasks");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskReagents");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskExperiments");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskExperimentFiles");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskDbFiles");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "ChemistryReagents");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "ChemistryMethodFiles");

            migrationBuilder.AddColumn<int>(
                name: "SeverityType",
                schema: "Store",
                table: "LoggedApplicationAction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    OperatedAt = table.Column<DateTime>(nullable: false),
                    OperatedBy = table.Column<string>(nullable: true),
                    KeyValues = table.Column<string>(nullable: true),
                    OldValues = table.Column<string>(nullable: true),
                    NewValues = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "SeverityType",
                schema: "Store",
                table: "LoggedApplicationAction");

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "DbFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DbFileHistory",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "DbFileHistory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "DbFileHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "DbFileHistory",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "DbFileHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "DayTasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "DayTaskComments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "ChemistryTasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskReagents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskExperiments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskExperimentFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskDbFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "ChemistryReagents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "ChemistryMethodFiles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Snapshot",
                schema: "Store",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    EntityId = table.Column<string>(maxLength: 128, nullable: true),
                    SnapshotJson = table.Column<string>(nullable: true),
                    TypeName = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snapshot", x => x.Id);
                });
        }
    }
}
