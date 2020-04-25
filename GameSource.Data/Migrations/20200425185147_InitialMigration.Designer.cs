﻿// <auto-generated />
using System;
using GameSource.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameSource.Data.Migrations
{
    [DbContext(typeof(GameSource_DBContext))]
    [Migration("20200425185147_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameSource.Models.Developer", b =>
                {
                    b.Property<int>("Developer_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Developer_ID");

                    b.ToTable("Developer");
                });

            modelBuilder.Entity("GameSource.Models.Game", b =>
                {
                    b.Property<int>("Game_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Developer_ID")
                        .HasColumnType("int");

                    b.Property<int?>("Genre_ID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlatformsPlatform_ID")
                        .HasColumnType("int");

                    b.Property<int?>("Publisher_ID")
                        .HasColumnType("int");

                    b.HasKey("Game_ID");

                    b.HasIndex("Developer_ID");

                    b.HasIndex("Genre_ID");

                    b.HasIndex("PlatformsPlatform_ID");

                    b.HasIndex("Publisher_ID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("GameSource.Models.Genre", b =>
                {
                    b.Property<int>("Genre_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Genre_ID");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("GameSource.Models.Platform", b =>
                {
                    b.Property<int>("Platform_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Platform_ID");

                    b.ToTable("Platform");
                });

            modelBuilder.Entity("GameSource.Models.Publisher", b =>
                {
                    b.Property<int>("Publisher_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Publisher_ID");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("GameSource.Models.Game", b =>
                {
                    b.HasOne("GameSource.Models.Developer", "Developer")
                        .WithMany()
                        .HasForeignKey("Developer_ID");

                    b.HasOne("GameSource.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("Genre_ID");

                    b.HasOne("GameSource.Models.Platform", "Platforms")
                        .WithMany()
                        .HasForeignKey("PlatformsPlatform_ID");

                    b.HasOne("GameSource.Models.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("Publisher_ID");
                });
#pragma warning restore 612, 618
        }
    }
}
