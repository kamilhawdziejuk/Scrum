﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScrumBlazor.Data;

namespace ScrumBlazor.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("ScrumBlazor.Data.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("StoryPoint")
                        .HasColumnType("REAL");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("ScrumBlazor.Data.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("TaskName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("ScrumBlazor.Data.Member", b =>
                {
                    b.HasOne("ScrumBlazor.Data.Team", null)
                        .WithMany("Members")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
