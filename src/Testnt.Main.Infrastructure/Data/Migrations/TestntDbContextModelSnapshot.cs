﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Infrastructure.Data.Migrations
{
    [DbContext(typeof(TestntDbContext))]
    partial class TestntDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Testnt.Main.Domain.Entity.ProjectUser", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("ProjectId", "UserProfileId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("ProjectUser");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TestProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TestProjectId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestFeature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TestProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TestProjectId");

                    b.ToTable("TestFeatures");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestOutline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<int>("TestExecutionResult")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TestOutlines");

                    b.HasDiscriminator<string>("Discriminator").HasValue("TestOutline");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestProject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestSessionEntity.TestCaseSnapshot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TestCaseId")
                        .HasColumnType("uuid");

                    b.Property<string>("TestCaseName")
                        .HasColumnType("text");

                    b.Property<Guid>("TestScenarioSnapshotId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TestScenarioSnapshotId");

                    b.ToTable("TestCaseSnapshot");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestSessionEntity.TestScenarioSnapshot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<int>("TestExecutionResult")
                        .HasColumnType("integer");

                    b.Property<Guid>("TestScenarioId")
                        .HasColumnType("uuid");

                    b.Property<string>("TestScenarioName")
                        .HasColumnType("text");

                    b.Property<Guid?>("TestSessionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TestSessionId");

                    b.ToTable("TestScenarioSnapshot");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestSessionEntity.TestSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Finished")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Started")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TestProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TestProjectId");

                    b.ToTable("TestSessions");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestSessionEntity.TestStepSnapshot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TestCaseSnapshotId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TestCaseSnapshotId");

                    b.ToTable("TestStepSnapshot");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestTag", b =>
                {
                    b.Property<Guid>("TestOutlineId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uuid");

                    b.HasKey("TestOutlineId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("TestTag");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestCase", b =>
                {
                    b.HasBaseType("Testnt.Main.Domain.Entity.TestOutline");

                    b.Property<Guid?>("TestProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TestScenarioId")
                        .HasColumnType("uuid");

                    b.HasIndex("TestProjectId");

                    b.HasIndex("TestScenarioId");

                    b.HasDiscriminator().HasValue("TestCase");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestScenario", b =>
                {
                    b.HasBaseType("Testnt.Main.Domain.Entity.TestOutline");

                    b.Property<Guid?>("TestFeatureId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TestProjectId")
                        .HasColumnName("TestScenario_TestProjectId")
                        .HasColumnType("uuid");

                    b.HasIndex("TestFeatureId");

                    b.HasIndex("TestProjectId");

                    b.HasDiscriminator().HasValue("TestScenario");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.ProjectUser", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.UserProfile", "UserProfile")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testnt.Main.Domain.Entity.TestProject", "TestProject")
                        .WithMany("Members")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Tag", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.TestProject", null)
                        .WithMany("TestTags")
                        .HasForeignKey("TestProjectId");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestFeature", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.TestProject", "TestProject")
                        .WithMany("TestFeatures")
                        .HasForeignKey("TestProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestSessionEntity.TestCaseSnapshot", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.TestSessionEntity.TestScenarioSnapshot", "TestScenarioSnapshot")
                        .WithMany("TestCaseSnapshots")
                        .HasForeignKey("TestScenarioSnapshotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestSessionEntity.TestScenarioSnapshot", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.TestSessionEntity.TestSession", null)
                        .WithMany("TestScenarioSnapshot")
                        .HasForeignKey("TestSessionId");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestSessionEntity.TestSession", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.TestProject", "TestProject")
                        .WithMany("TestSessions")
                        .HasForeignKey("TestProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestSessionEntity.TestStepSnapshot", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.TestSessionEntity.TestCaseSnapshot", null)
                        .WithMany("TestStepSnapshot")
                        .HasForeignKey("TestCaseSnapshotId");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestTag", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.TestOutline", "TestOutline")
                        .WithMany("TestTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testnt.Main.Domain.Entity.Tag", "Tag")
                        .WithMany("TestTags")
                        .HasForeignKey("TestOutlineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestCase", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.TestProject", "TestProject")
                        .WithMany("TestCases")
                        .HasForeignKey("TestProjectId");

                    b.HasOne("Testnt.Main.Domain.Entity.TestScenario", "TestScenario")
                        .WithMany("TestCases")
                        .HasForeignKey("TestScenarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Testnt.Main.Domain.Entity.TestStep", "TestSteps", b1 =>
                        {
                            b1.Property<Guid>("TestCaseId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("Description")
                                .HasColumnType("text");

                            b1.Property<string>("Status")
                                .HasColumnType("text");

                            b1.Property<Guid>("TenantId")
                                .HasColumnType("uuid");

                            b1.HasKey("TestCaseId", "Id");

                            b1.ToTable("TestStep");

                            b1.WithOwner()
                                .HasForeignKey("TestCaseId");
                        });
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestScenario", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.TestFeature", null)
                        .WithMany("TestScenarios")
                        .HasForeignKey("TestFeatureId");

                    b.HasOne("Testnt.Main.Domain.Entity.TestProject", "TestProject")
                        .WithMany("TestScenarios")
                        .HasForeignKey("TestProjectId");
                });
#pragma warning restore 612, 618
        }
    }
}
