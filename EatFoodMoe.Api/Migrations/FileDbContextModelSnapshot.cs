﻿// <auto-generated />
using System;
using EatFoodMoe.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EatFoodMoe.Api.Migrations
{
    [DbContext(typeof(FileDbContext))]
    partial class FileDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("EatFoodMoe.Api.Entitles.EatFoodFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Embellishment")
                        .HasColumnType("INTEGER");

                    b.Property<long>("FileSize")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("LastModityTIme")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Proofreading")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Translation")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("EatFoodFiles");
                });

            modelBuilder.Entity("EatFoodMoe.Api.Entitles.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("FileCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GameName")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberNames")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4e43a044-2e4d-4a60-862f-606c5cbdf066"),
                            FileCount = 0,
                            GameName = "Unknowed",
                            GroupId = new Guid("de04d32c-5f8b-4e24-95a4-40855d35a663"),
                            MemberNames = "Unknowed",
                            Name = "Unknowed"
                        });
                });

            modelBuilder.Entity("EatFoodMoe.Api.Entitles.SinicizationGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nmae")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SinicizationGroup");

                    b.HasData(
                        new
                        {
                            Id = new Guid("de04d32c-5f8b-4e24-95a4-40855d35a663"),
                            Nmae = "Unknowed"
                        });
                });

            modelBuilder.Entity("EatFoodMoe.Api.Entitles.EatFoodFile", b =>
                {
                    b.HasOne("EatFoodMoe.Api.Entitles.Project", "Project")
                        .WithMany("Files")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("EatFoodMoe.Api.Entitles.Project", b =>
                {
                    b.HasOne("EatFoodMoe.Api.Entitles.SinicizationGroup", "Group")
                        .WithMany("Projects")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("EatFoodMoe.Api.Entitles.Project", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("EatFoodMoe.Api.Entitles.SinicizationGroup", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
