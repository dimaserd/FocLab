using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Model.Migrations
{
    public partial class AddCoreEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebAppRequestContextLogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    RequestId = table.Column<string>(nullable: true),
                    ParentRequestId = table.Column<string>(nullable: true),
                    Uri = table.Column<string>(nullable: true),
                    StartedOn = table.Column<DateTime>(nullable: false),
                    FinishedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebAppRequestContextLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationMessageLog",
                schema: "Store",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MessageType = table.Column<string>(nullable: true),
                    MessageJson = table.Column<string>(nullable: true),
                    RequestId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationMessageLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RobotTask",
                schema: "Store",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Script = table.Column<string>(nullable: true),
                    Result = table.Column<int>(nullable: false),
                    IsExecutionDelayed = table.Column<bool>(nullable: false),
                    ToExecuteOn = table.Column<DateTime>(nullable: false),
                    StartedOn = table.Column<DateTime>(nullable: true),
                    ExecutedOn = table.Column<DateTime>(nullable: true),
                    ExceptionStackTrace = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RobotTask", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationMessageStatusLog",
                schema: "Store",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MessageId = table.Column<string>(nullable: true),
                    StartedOn = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationMessageStatusLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegrationMessageStatusLog_IntegrationMessageLog_MessageId",
                        column: x => x.MessageId,
                        principalSchema: "Store",
                        principalTable: "IntegrationMessageLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoggedApplicationAction_EventId",
                schema: "Store",
                table: "LoggedApplicationAction",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_LoggedApplicationAction_TransactionId",
                schema: "Store",
                table: "LoggedApplicationAction",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_WebAppRequestContextLogs_RequestId",
                table: "WebAppRequestContextLogs",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_WebAppRequestContextLogs_StartedOn",
                table: "WebAppRequestContextLogs",
                column: "StartedOn");

            migrationBuilder.CreateIndex(
                name: "IX_WebAppRequestContextLogs_Uri",
                table: "WebAppRequestContextLogs",
                column: "Uri");

            migrationBuilder.CreateIndex(
                name: "IX_WebAppRequestContextLogs_UserId",
                table: "WebAppRequestContextLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegrationMessageStatusLog_MessageId",
                schema: "Store",
                table: "IntegrationMessageStatusLog",
                column: "MessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebAppRequestContextLogs");

            migrationBuilder.DropTable(
                name: "IntegrationMessageStatusLog",
                schema: "Store");

            migrationBuilder.DropTable(
                name: "RobotTask",
                schema: "Store");

            migrationBuilder.DropTable(
                name: "IntegrationMessageLog",
                schema: "Store");

            migrationBuilder.DropIndex(
                name: "IX_LoggedApplicationAction_EventId",
                schema: "Store",
                table: "LoggedApplicationAction");

            migrationBuilder.DropIndex(
                name: "IX_LoggedApplicationAction_TransactionId",
                schema: "Store",
                table: "LoggedApplicationAction");
        }
    }
}
