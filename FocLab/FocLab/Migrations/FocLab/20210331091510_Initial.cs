using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Migrations.FocLab
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reagents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reagents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MethodFiles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    FileId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MethodFiles_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Title = table.Column<string>(nullable: true),
                    DeadLineDate = table.Column<DateTime>(nullable: false),
                    PerformedDate = table.Column<DateTime>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    AdminUserId = table.Column<string>(nullable: true),
                    PerformerUserId = table.Column<string>(nullable: true),
                    MethodFileId = table.Column<string>(nullable: true),
                    AdminQuantity = table.Column<string>(nullable: true),
                    AdminQuality = table.Column<string>(nullable: true),
                    PerformerQuantity = table.Column<string>(nullable: true),
                    PerformerQuality = table.Column<string>(nullable: true),
                    PerformerText = table.Column<string>(nullable: true),
                    SubstanceCounterJson = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_AdminUserId",
                        column: x => x.AdminUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_MethodFiles_MethodFileId",
                        column: x => x.MethodFileId,
                        principalTable: "MethodFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_PerformerUserId",
                        column: x => x.PerformerUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskDbFiles",
                columns: table => new
                {
                    ChemistryTaskId = table.Column<string>(nullable: false),
                    FileId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDbFiles", x => new { x.ChemistryTaskId, x.FileId });
                    table.ForeignKey(
                        name: "FK_TaskDbFiles_Tasks_ChemistryTaskId",
                        column: x => x.ChemistryTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskDbFiles_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskExperiments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ChemistryTaskId = table.Column<string>(nullable: true),
                    PerformerId = table.Column<string>(nullable: true),
                    PerformedDate = table.Column<DateTime>(nullable: true),
                    PerformerText = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    SubstanceCounterJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskExperiments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskExperiments_Tasks_ChemistryTaskId",
                        column: x => x.ChemistryTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskExperiments_Users_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskReagents",
                columns: table => new
                {
                    TaskId = table.Column<string>(nullable: false),
                    ReagentId = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TakenQuantity = table.Column<decimal>(nullable: false),
                    ReturnedQuantity = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskReagents", x => new { x.TaskId, x.ReagentId });
                    table.ForeignKey(
                        name: "FK_TaskReagents_Reagents_ReagentId",
                        column: x => x.ReagentId,
                        principalTable: "Reagents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskReagents_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskExperimentFiles",
                columns: table => new
                {
                    ChemistryTaskExperimentId = table.Column<string>(nullable: false),
                    FileId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskExperimentFiles", x => new { x.ChemistryTaskExperimentId, x.FileId });
                    table.ForeignKey(
                        name: "FK_TaskExperimentFiles_TaskExperiments_ChemistryTaskExperimentId",
                        column: x => x.ChemistryTaskExperimentId,
                        principalTable: "TaskExperiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskExperimentFiles_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MethodFiles_FileId",
                table: "MethodFiles",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDbFiles_FileId",
                table: "TaskDbFiles",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskExperimentFiles_FileId",
                table: "TaskExperimentFiles",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskExperiments_ChemistryTaskId",
                table: "TaskExperiments",
                column: "ChemistryTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskExperiments_PerformerId",
                table: "TaskExperiments",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReagents_ReagentId",
                table: "TaskReagents",
                column: "ReagentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AdminUserId",
                table: "Tasks",
                column: "AdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_MethodFileId",
                table: "Tasks",
                column: "MethodFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PerformerUserId",
                table: "Tasks",
                column: "PerformerUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskDbFiles");

            migrationBuilder.DropTable(
                name: "TaskExperimentFiles");

            migrationBuilder.DropTable(
                name: "TaskReagents");

            migrationBuilder.DropTable(
                name: "TaskExperiments");

            migrationBuilder.DropTable(
                name: "Reagents");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MethodFiles");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
