using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Model.Migrations.Croco
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Store");

            migrationBuilder.CreateTable(
                name: "DbFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FileData = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                schema: "Store",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RequestId = table.Column<string>(nullable: true),
                    EntityName = table.Column<string>(nullable: true),
                    OperatedAt = table.Column<DateTime>(nullable: false),
                    OperatedBy = table.Column<string>(nullable: true),
                    KeyValues = table.Column<string>(nullable: true),
                    OldValues = table.Column<string>(nullable: true),
                    NewValues = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationMessageLog",
                schema: "Store",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MessageType = table.Column<string>(nullable: true),
                    MessageJson = table.Column<string>(nullable: true),
                    RequestId = table.Column<string>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false)
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
                    ToExecuteOnUtc = table.Column<DateTime>(nullable: false),
                    StartedOnUtc = table.Column<DateTime>(nullable: true),
                    ExecutedOnUtc = table.Column<DateTime>(nullable: true),
                    ExceptionStackTrace = table.Column<string>(nullable: true),
                    DataJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RobotTask", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbFileHistory",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileData = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbFileHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbFileHistory_DbFiles_ParentId",
                        column: x => x.ParentId,
                        principalTable: "DbFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationMessageStatusLog",
                schema: "Store",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    HandlerTypeFullName = table.Column<string>(maxLength: 128, nullable: true),
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
                name: "IX_DbFileHistory_ParentId",
                table: "DbFileHistory",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegrationMessageStatusLog_MessageId",
                schema: "Store",
                table: "IntegrationMessageStatusLog",
                column: "MessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbFileHistory");

            migrationBuilder.DropTable(
                name: "AuditLog",
                schema: "Store");

            migrationBuilder.DropTable(
                name: "IntegrationMessageStatusLog",
                schema: "Store");

            migrationBuilder.DropTable(
                name: "RobotTask",
                schema: "Store");

            migrationBuilder.DropTable(
                name: "DbFiles");

            migrationBuilder.DropTable(
                name: "IntegrationMessageLog",
                schema: "Store");
        }
    }
}
