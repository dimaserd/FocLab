using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Model.Migrations
{
    public partial class DatabaseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ChemistryTasks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ChemistryTasks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "ChemistryTasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ChemistryTasks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "ChemistryTasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ChemistryTaskReagents",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ChemistryTaskReagents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskReagents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ChemistryTaskReagents",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "ChemistryTaskReagents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ChemistryTaskExperiments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ChemistryTaskExperiments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskExperiments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ChemistryTaskExperiments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "ChemistryTaskExperiments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ChemistryTaskExperimentFiles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ChemistryTaskExperimentFiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskExperimentFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ChemistryTaskExperimentFiles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "ChemistryTaskExperimentFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ChemistryTaskDbFiles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ChemistryTaskDbFiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskDbFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ChemistryTaskDbFiles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "ChemistryTaskDbFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ChemistryReagents",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ChemistryReagents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "ChemistryReagents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ChemistryReagents",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "ChemistryReagents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ChemistryMethodFiles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ChemistryMethodFiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CurrentSnapshotId",
                table: "ChemistryMethodFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ChemistryMethodFiles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "ChemistryMethodFiles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DayTasks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CurrentSnapshotId = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    TaskDate = table.Column<DateTime>(type: "date", nullable: false),
                    TaskText = table.Column<string>(nullable: true),
                    TaskTitle = table.Column<string>(nullable: true),
                    FinishDate = table.Column<DateTime>(nullable: true),
                    TaskTarget = table.Column<string>(nullable: true),
                    TaskReview = table.Column<string>(nullable: true),
                    TaskComment = table.Column<string>(nullable: true),
                    EstimationSeconds = table.Column<int>(nullable: false),
                    CompletionSeconds = table.Column<int>(nullable: false),
                    Seconds = table.Column<int>(nullable: false),
                    AuthorId = table.Column<string>(nullable: true),
                    AssigneeUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayTasks_AspNetUsers_AssigneeUserId",
                        column: x => x.AssigneeUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayTasks_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DayTaskComments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CurrentSnapshotId = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    DayTaskId = table.Column<string>(nullable: true),
                    AuthorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayTaskComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayTaskComments_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayTaskComments_DayTasks_DayTaskId",
                        column: x => x.DayTaskId,
                        principalTable: "DayTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayTaskComments_AuthorId",
                table: "DayTaskComments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_DayTaskComments_DayTaskId",
                table: "DayTaskComments",
                column: "DayTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_DayTasks_AssigneeUserId",
                table: "DayTasks",
                column: "AssigneeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DayTasks_AuthorId",
                table: "DayTasks",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayTaskComments");

            migrationBuilder.DropTable(
                name: "DayTasks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ChemistryTasks");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ChemistryTasks");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "ChemistryTasks");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ChemistryTasks");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "ChemistryTasks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ChemistryTaskReagents");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ChemistryTaskReagents");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskReagents");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ChemistryTaskReagents");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "ChemistryTaskReagents");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ChemistryTaskExperiments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ChemistryTaskExperiments");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskExperiments");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ChemistryTaskExperiments");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "ChemistryTaskExperiments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ChemistryTaskExperimentFiles");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ChemistryTaskExperimentFiles");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskExperimentFiles");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ChemistryTaskExperimentFiles");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "ChemistryTaskExperimentFiles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ChemistryTaskDbFiles");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ChemistryTaskDbFiles");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "ChemistryTaskDbFiles");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ChemistryTaskDbFiles");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "ChemistryTaskDbFiles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ChemistryReagents");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ChemistryReagents");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "ChemistryReagents");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ChemistryReagents");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "ChemistryReagents");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ChemistryMethodFiles");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ChemistryMethodFiles");

            migrationBuilder.DropColumn(
                name: "CurrentSnapshotId",
                table: "ChemistryMethodFiles");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ChemistryMethodFiles");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "ChemistryMethodFiles");
        }
    }
}
