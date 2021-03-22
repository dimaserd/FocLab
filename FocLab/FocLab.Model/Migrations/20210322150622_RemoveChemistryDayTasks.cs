using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Model.Migrations
{
    public partial class RemoveChemistryDayTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChemistryDayTasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChemistryDayTasks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AssigneeUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaskCommentHtml = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskDate = table.Column<DateTime>(type: "date", nullable: false),
                    TaskReviewHtml = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskTargetHtml = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChemistryDayTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChemistryDayTasks_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChemistryDayTasks_AspNetUsers_AssigneeUserId",
                        column: x => x.AssigneeUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChemistryDayTasks_AdminId",
                table: "ChemistryDayTasks",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistryDayTasks_AssigneeUserId",
                table: "ChemistryDayTasks",
                column: "AssigneeUserId");
        }
    }
}
