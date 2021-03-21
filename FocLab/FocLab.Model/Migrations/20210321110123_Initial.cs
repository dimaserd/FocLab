using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocLab.Model.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Store");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChemistryReagents",
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
                    table.PrimaryKey("PK_ChemistryReagents", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    AvatarFileId = table.Column<int>(nullable: true),
                    UnConfirmedEmail = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    Sex = table.Column<bool>(nullable: true),
                    Balance = table.Column<decimal>(nullable: false),
                    DeActivated = table.Column<bool>(nullable: false),
                    ObjectJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_DbFiles_AvatarFileId",
                        column: x => x.AvatarFileId,
                        principalTable: "DbFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChemistryMethodFiles",
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
                    table.PrimaryKey("PK_ChemistryMethodFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChemistryMethodFiles_DbFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "DbFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DbFileHistory",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileData = table.Column<byte[]>(nullable: true),
                    FileMimeType = table.Column<string>(nullable: true)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChemistryDayTasks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    TaskDate = table.Column<DateTime>(type: "date", nullable: false),
                    TaskText = table.Column<string>(nullable: true),
                    TaskTitle = table.Column<string>(nullable: true),
                    FinishDate = table.Column<DateTime>(nullable: true),
                    TaskTargetHtml = table.Column<string>(nullable: true),
                    TaskReviewHtml = table.Column<string>(nullable: true),
                    TaskCommentHtml = table.Column<string>(nullable: true),
                    AdminId = table.Column<string>(nullable: true),
                    AssigneeUserId = table.Column<string>(nullable: true)
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
                name: "ChemistryTasks",
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
                    table.PrimaryKey("PK_ChemistryTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChemistryTasks_AspNetUsers_AdminUserId",
                        column: x => x.AdminUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChemistryTasks_ChemistryMethodFiles_MethodFileId",
                        column: x => x.MethodFileId,
                        principalTable: "ChemistryMethodFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChemistryTasks_AspNetUsers_PerformerUserId",
                        column: x => x.PerformerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "ChemistryTaskDbFiles",
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
                    table.PrimaryKey("PK_ChemistryTaskDbFiles", x => new { x.ChemistryTaskId, x.FileId });
                    table.ForeignKey(
                        name: "FK_ChemistryTaskDbFiles_ChemistryTasks_ChemistryTaskId",
                        column: x => x.ChemistryTaskId,
                        principalTable: "ChemistryTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChemistryTaskDbFiles_DbFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "DbFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChemistryTaskExperiments",
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
                    table.PrimaryKey("PK_ChemistryTaskExperiments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChemistryTaskExperiments_ChemistryTasks_ChemistryTaskId",
                        column: x => x.ChemistryTaskId,
                        principalTable: "ChemistryTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChemistryTaskExperiments_AspNetUsers_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChemistryTaskReagents",
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
                    table.PrimaryKey("PK_ChemistryTaskReagents", x => new { x.TaskId, x.ReagentId });
                    table.ForeignKey(
                        name: "FK_ChemistryTaskReagents_ChemistryReagents_ReagentId",
                        column: x => x.ReagentId,
                        principalTable: "ChemistryReagents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChemistryTaskReagents_ChemistryTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "ChemistryTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChemistryTaskExperimentFiles",
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
                    table.PrimaryKey("PK_ChemistryTaskExperimentFiles", x => new { x.ChemistryTaskExperimentId, x.FileId });
                    table.ForeignKey(
                        name: "FK_ChemistryTaskExperimentFiles_ChemistryTaskExperiments_ChemistryTaskExperimentId",
                        column: x => x.ChemistryTaskExperimentId,
                        principalTable: "ChemistryTaskExperiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChemistryTaskExperimentFiles_DbFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "DbFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvatarFileId",
                table: "AspNetUsers",
                column: "AvatarFileId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistryDayTasks_AdminId",
                table: "ChemistryDayTasks",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistryDayTasks_AssigneeUserId",
                table: "ChemistryDayTasks",
                column: "AssigneeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistryMethodFiles_FileId",
                table: "ChemistryMethodFiles",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistryTaskDbFiles_FileId",
                table: "ChemistryTaskDbFiles",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistryTaskExperimentFiles_FileId",
                table: "ChemistryTaskExperimentFiles",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistryTaskExperiments_ChemistryTaskId",
                table: "ChemistryTaskExperiments",
                column: "ChemistryTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistryTaskExperiments_PerformerId",
                table: "ChemistryTaskExperiments",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistryTaskReagents_ReagentId",
                table: "ChemistryTaskReagents",
                column: "ReagentId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistryTasks_AdminUserId",
                table: "ChemistryTasks",
                column: "AdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistryTasks_MethodFileId",
                table: "ChemistryTasks",
                column: "MethodFileId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistryTasks_PerformerUserId",
                table: "ChemistryTasks",
                column: "PerformerUserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_DbFileHistory_ParentId",
                table: "DbFileHistory",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ChemistryDayTasks");

            migrationBuilder.DropTable(
                name: "ChemistryTaskDbFiles");

            migrationBuilder.DropTable(
                name: "ChemistryTaskExperimentFiles");

            migrationBuilder.DropTable(
                name: "ChemistryTaskReagents");

            migrationBuilder.DropTable(
                name: "DayTaskComments");

            migrationBuilder.DropTable(
                name: "DbFileHistory");

            migrationBuilder.DropTable(
                name: "AuditLog",
                schema: "Store");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ChemistryTaskExperiments");

            migrationBuilder.DropTable(
                name: "ChemistryReagents");

            migrationBuilder.DropTable(
                name: "DayTasks");

            migrationBuilder.DropTable(
                name: "ChemistryTasks");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ChemistryMethodFiles");

            migrationBuilder.DropTable(
                name: "DbFiles");
        }
    }
}
