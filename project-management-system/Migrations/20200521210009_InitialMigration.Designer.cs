﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using project_management_system.Context;

namespace project_management_system.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20200521210009_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("project_management_system.Context.Assignment", b =>
                {
                    b.Property<int>("AssignmentID")
                        .HasColumnType("int");

                    b.Property<string>("AssignmentDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssignmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("AssignmentID");

                    b.HasIndex("ProjectID");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("project_management_system.Context.AssignmentUser", b =>
                {
                    b.Property<int>("AssignmentID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("AssignmentID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("AssignmentUser");
                });

            modelBuilder.Entity("project_management_system.Context.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .HasColumnType("int");

                    b.Property<int>("AssignmentID")
                        .HasColumnType("int");

                    b.Property<string>("CommentContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.HasKey("CommentID");

                    b.HasIndex("AssignmentID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("project_management_system.Context.Project", b =>
                {
                    b.Property<int>("ProjectID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProjectDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ProjectID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("project_management_system.Context.User", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("ProjectID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("project_management_system.Context.Assignment", b =>
                {
                    b.HasOne("project_management_system.Context.Project", "Project")
                        .WithMany("Assignments")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("project_management_system.Context.AssignmentUser", b =>
                {
                    b.HasOne("project_management_system.Context.Assignment", "Assignment")
                        .WithMany("AssignmentUsers")
                        .HasForeignKey("AssignmentID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("project_management_system.Context.User", "User")
                        .WithMany("AssignmentUsers")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("project_management_system.Context.Comment", b =>
                {
                    b.HasOne("project_management_system.Context.Assignment", "Assignment")
                        .WithMany("Comments")
                        .HasForeignKey("AssignmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("project_management_system.Context.User", b =>
                {
                    b.HasOne("project_management_system.Context.Project", "Project")
                        .WithMany("Users")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
