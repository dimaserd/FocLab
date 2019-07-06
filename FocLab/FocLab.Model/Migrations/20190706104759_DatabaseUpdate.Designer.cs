﻿// <auto-generated />
using System;
using FocLab.Model.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FocLab.Model.Migrations
{
    [DbContext(typeof(ChemistryDbContext))]
    [Migration("20190706104759_DatabaseUpdate")]
    partial class DatabaseUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Croco.Core.Model.Entities.Store.LoggedApplicationAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ActionDate");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<string>("GroupName");

                    b.Property<bool>("IsException");

                    b.Property<bool>("IsInternal");

                    b.Property<string>("Message");

                    b.Property<string>("StackTrace");

                    b.HasKey("Id");

                    b.ToTable("LoggedApplicationAction","Store");
                });

            modelBuilder.Entity("Croco.Core.Model.Entities.Store.LoggedUserInterfaceAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<string>("GroupName");

                    b.Property<bool>("IsException");

                    b.Property<DateTime>("LogDate");

                    b.Property<string>("Message");

                    b.Property<string>("Uri")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("LoggedUserInterfaceAction","Store");
                });

            modelBuilder.Entity("Croco.Core.Model.Entities.Store.Snapshot", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("EntityId")
                        .HasMaxLength(128);

                    b.Property<string>("SnapshotJson");

                    b.Property<string>("TypeName")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("Snapshot","Store");
                });

            modelBuilder.Entity("FocLab.Model.Entities.ApplicationDbFileHistory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CurrentSnapshotId")
                        .IsConcurrencyToken();

                    b.Property<byte[]>("FileData");

                    b.Property<string>("FileMimeType");

                    b.Property<string>("FileName");

                    b.Property<string>("FilePath");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime?>("LastModifiedOn");

                    b.Property<int>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("DbFileHistory");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryDayTask", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdminId");

                    b.Property<string>("AssigneeUserId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime?>("FinishDate");

                    b.Property<string>("TaskCommentHtml");

                    b.Property<DateTime>("TaskDate")
                        .HasColumnType("date");

                    b.Property<string>("TaskReviewHtml");

                    b.Property<string>("TaskTargetHtml");

                    b.Property<string>("TaskText");

                    b.Property<string>("TaskTitle");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("AssigneeUserId");

                    b.ToTable("ChemistryDayTasks");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryMethodFile", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CurrentSnapshotId")
                        .IsConcurrencyToken();

                    b.Property<int>("FileId");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime?>("LastModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.ToTable("ChemistryMethodFiles");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryReagent", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CurrentSnapshotId")
                        .IsConcurrencyToken();

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime?>("LastModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ChemistryReagents");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryTask", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdminQuality");

                    b.Property<string>("AdminQuantity");

                    b.Property<string>("AdminUserId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CurrentSnapshotId")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DeadLineDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime?>("LastModifiedOn");

                    b.Property<string>("MethodFileId");

                    b.Property<DateTime?>("PerformedDate");

                    b.Property<string>("PerformerQuality");

                    b.Property<string>("PerformerQuantity");

                    b.Property<string>("PerformerText");

                    b.Property<string>("PerformerUserId");

                    b.Property<string>("SubstanceCounterJson");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("AdminUserId");

                    b.HasIndex("MethodFileId");

                    b.HasIndex("PerformerUserId");

                    b.ToTable("ChemistryTasks");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryTaskDbFile", b =>
                {
                    b.Property<string>("ChemistryTaskId");

                    b.Property<int>("FileId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CurrentSnapshotId")
                        .IsConcurrencyToken();

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime?>("LastModifiedOn");

                    b.Property<int>("Type");

                    b.HasKey("ChemistryTaskId", "FileId");

                    b.HasIndex("FileId");

                    b.ToTable("ChemistryTaskDbFiles");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryTaskExperiment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChemistryTaskId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CurrentSnapshotId")
                        .IsConcurrencyToken();

                    b.Property<bool>("Deleted");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime?>("LastModifiedOn");

                    b.Property<DateTime?>("PerformedDate");

                    b.Property<string>("PerformerId");

                    b.Property<string>("PerformerText");

                    b.Property<string>("SubstanceCounterJson");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ChemistryTaskId");

                    b.HasIndex("PerformerId");

                    b.ToTable("ChemistryTaskExperiments");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryTaskExperimentFile", b =>
                {
                    b.Property<string>("ChemistryTaskExperimentId");

                    b.Property<int>("FileId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CurrentSnapshotId")
                        .IsConcurrencyToken();

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime?>("LastModifiedOn");

                    b.Property<int>("Type");

                    b.HasKey("ChemistryTaskExperimentId", "FileId");

                    b.HasIndex("FileId");

                    b.ToTable("ChemistryTaskExperimentFiles");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryTaskReagent", b =>
                {
                    b.Property<string>("TaskId");

                    b.Property<string>("ReagentId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CurrentSnapshotId")
                        .IsConcurrencyToken();

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime?>("LastModifiedOn");

                    b.Property<decimal>("ReturnedQuantity");

                    b.Property<decimal>("TakenQuantity");

                    b.HasKey("TaskId", "ReagentId");

                    b.HasIndex("ReagentId");

                    b.ToTable("ChemistryTaskReagents");
                });

            modelBuilder.Entity("FocLab.Model.Entities.DbFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CurrentSnapshotId")
                        .IsConcurrencyToken();

                    b.Property<byte[]>("FileData");

                    b.Property<string>("FileName");

                    b.Property<string>("FilePath");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime?>("LastModifiedOn");

                    b.HasKey("Id");

                    b.ToTable("DbFiles");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Tasker.ApplicationDayTask", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssigneeUserId");

                    b.Property<string>("AuthorId");

                    b.Property<int>("CompletionSeconds");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CurrentSnapshotId")
                        .IsConcurrencyToken();

                    b.Property<int>("EstimationSeconds");

                    b.Property<DateTime?>("FinishDate");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime?>("LastModifiedOn");

                    b.Property<int>("Seconds");

                    b.Property<string>("TaskComment");

                    b.Property<DateTime>("TaskDate")
                        .HasColumnType("date");

                    b.Property<string>("TaskReview");

                    b.Property<string>("TaskTarget");

                    b.Property<string>("TaskText");

                    b.Property<string>("TaskTitle");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeUserId");

                    b.HasIndex("AuthorId");

                    b.ToTable("DayTasks");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Tasker.ApplicationDayTaskComment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId");

                    b.Property<string>("Comment");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CurrentSnapshotId")
                        .IsConcurrencyToken();

                    b.Property<string>("DayTaskId");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime?>("LastModifiedOn");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("DayTaskId");

                    b.ToTable("DayTaskComments");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Users.Default.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Users.Default.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("AvatarFileId");

                    b.Property<decimal>("Balance");

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CurrentSnapshotId")
                        .IsConcurrencyToken();

                    b.Property<bool>("DeActivated");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime?>("LastModifiedOn");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("ObjectJson");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Patronymic");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool?>("Sex");

                    b.Property<string>("Surname");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UnConfirmedEmail");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("AvatarFileId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Users.Default.ApplicationUserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FocLab.Model.Entities.ApplicationDbFileHistory", b =>
                {
                    b.HasOne("FocLab.Model.Entities.DbFile", "ParentFile")
                        .WithMany("History")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryDayTask", b =>
                {
                    b.HasOne("FocLab.Model.Entities.Users.Default.ApplicationUser", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId");

                    b.HasOne("FocLab.Model.Entities.Users.Default.ApplicationUser", "AssigneeUser")
                        .WithMany()
                        .HasForeignKey("AssigneeUserId");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryMethodFile", b =>
                {
                    b.HasOne("FocLab.Model.Entities.DbFile", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryTask", b =>
                {
                    b.HasOne("FocLab.Model.Entities.Users.Default.ApplicationUser", "AdminUser")
                        .WithMany()
                        .HasForeignKey("AdminUserId");

                    b.HasOne("FocLab.Model.Entities.Chemistry.ChemistryMethodFile", "ChemistryMethodFile")
                        .WithMany()
                        .HasForeignKey("MethodFileId");

                    b.HasOne("FocLab.Model.Entities.Users.Default.ApplicationUser", "PerformerUser")
                        .WithMany()
                        .HasForeignKey("PerformerUserId");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryTaskDbFile", b =>
                {
                    b.HasOne("FocLab.Model.Entities.Chemistry.ChemistryTask", "ChemistryTask")
                        .WithMany("Files")
                        .HasForeignKey("ChemistryTaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FocLab.Model.Entities.DbFile", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryTaskExperiment", b =>
                {
                    b.HasOne("FocLab.Model.Entities.Chemistry.ChemistryTask", "ChemistryTask")
                        .WithMany("Experiments")
                        .HasForeignKey("ChemistryTaskId");

                    b.HasOne("FocLab.Model.Entities.Users.Default.ApplicationUser", "Performer")
                        .WithMany()
                        .HasForeignKey("PerformerId");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryTaskExperimentFile", b =>
                {
                    b.HasOne("FocLab.Model.Entities.Chemistry.ChemistryTaskExperiment", "ChemistryTaskExperiment")
                        .WithMany("Files")
                        .HasForeignKey("ChemistryTaskExperimentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FocLab.Model.Entities.DbFile", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FocLab.Model.Entities.Chemistry.ChemistryTaskReagent", b =>
                {
                    b.HasOne("FocLab.Model.Entities.Chemistry.ChemistryReagent", "Reagent")
                        .WithMany("Tasks")
                        .HasForeignKey("ReagentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FocLab.Model.Entities.Chemistry.ChemistryTask", "Task")
                        .WithMany("Reagents")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FocLab.Model.Entities.Tasker.ApplicationDayTask", b =>
                {
                    b.HasOne("FocLab.Model.Entities.Users.Default.ApplicationUser", "AssigneeUser")
                        .WithMany()
                        .HasForeignKey("AssigneeUserId");

                    b.HasOne("FocLab.Model.Entities.Users.Default.ApplicationUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Tasker.ApplicationDayTaskComment", b =>
                {
                    b.HasOne("FocLab.Model.Entities.Users.Default.ApplicationUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("FocLab.Model.Entities.Tasker.ApplicationDayTask", "DayTask")
                        .WithMany("Comments")
                        .HasForeignKey("DayTaskId");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Users.Default.ApplicationUser", b =>
                {
                    b.HasOne("FocLab.Model.Entities.DbFile", "AvatarFile")
                        .WithMany()
                        .HasForeignKey("AvatarFileId");
                });

            modelBuilder.Entity("FocLab.Model.Entities.Users.Default.ApplicationUserRole", b =>
                {
                    b.HasOne("FocLab.Model.Entities.Users.Default.ApplicationRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FocLab.Model.Entities.Users.Default.ApplicationUser", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("FocLab.Model.Entities.Users.Default.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FocLab.Model.Entities.Users.Default.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FocLab.Model.Entities.Users.Default.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FocLab.Model.Entities.Users.Default.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
