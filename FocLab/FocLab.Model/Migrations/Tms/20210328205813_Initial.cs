using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Model.Migrations.Tms
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayTasks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TaskDate = table.Column<DateTime>(type: "date", nullable: false),
                    TaskText = table.Column<string>(nullable: true),
                    TaskTitle = table.Column<string>(nullable: true),
                    FinishDate = table.Column<DateTime>(nullable: true),
                    TaskTarget = table.Column<string>(nullable: true),
                    TaskReview = table.Column<string>(nullable: true),
                    TaskComment = table.Column<string>(nullable: true),
                    EstimationSeconds = table.Column<int>(nullable: false),
                    CompletionSeconds = table.Column<int>(nullable: false),
                    AuthorId = table.Column<string>(nullable: true),
                    AssigneeUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayTaskComments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    DayTaskId = table.Column<string>(nullable: true),
                    AuthorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayTaskComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayTaskComments_DayTasks_DayTaskId",
                        column: x => x.DayTaskId,
                        principalTable: "DayTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayTaskComments_DayTaskId",
                table: "DayTaskComments",
                column: "DayTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayTaskComments");

            migrationBuilder.DropTable(
                name: "DayTasks");
        }
    }
}
