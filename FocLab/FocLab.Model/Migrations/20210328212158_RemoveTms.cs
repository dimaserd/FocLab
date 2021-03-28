using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Model.Migrations
{
    public partial class RemoveTms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayTaskComments");

            migrationBuilder.DropTable(
                name: "DayTasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayTasks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssigneeUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompletionSeconds = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimationSeconds = table.Column<int>(type: "int", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Seconds = table.Column<int>(type: "int", nullable: false),
                    TaskComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskDate = table.Column<DateTime>(type: "date", nullable: false),
                    TaskReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskTarget = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayTaskId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
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
    }
}
