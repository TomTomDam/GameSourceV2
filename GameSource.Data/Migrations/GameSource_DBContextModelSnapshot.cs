﻿// <auto-generated />
using GameSource.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameSource.Data.Migrations
{
    [DbContext(typeof(GameSource_DBContext))]
    partial class GameSource_DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameSource.Models.Developer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Developer");
                });

            modelBuilder.Entity("GameSource.Models.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DeveloperID")
                        .HasColumnType("int");

                    b.Property<int>("GenreID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlatformID")
                        .HasColumnType("int");

                    b.Property<int>("PublisherID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DeveloperID");

                    b.HasIndex("GenreID");

                    b.HasIndex("PlatformID");

                    b.HasIndex("PublisherID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("GameSource.Models.Genre", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("GameSource.Models.Platform", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlatformTypeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PlatformTypeID");

                    b.ToTable("Platform");
                });

            modelBuilder.Entity("GameSource.Models.PlatformType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PlatformType");
                });

            modelBuilder.Entity("GameSource.Models.Publisher", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("GameSource.Models.Game", b =>
                {
                    b.HasOne("GameSource.Models.Developer", "Developer")
                        .WithMany("Games")
                        .HasForeignKey("DeveloperID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameSource.Models.Genre", "Genre")
                        .WithMany("Games")
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameSource.Models.Platform", "Platform")
                        .WithMany("Games")
                        .HasForeignKey("PlatformID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameSource.Models.Publisher", "Publisher")
                        .WithMany("Games")
                        .HasForeignKey("PublisherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameSource.Models.Platform", b =>
                {
                    b.HasOne("GameSource.Models.PlatformType", "PlatformType")
                        .WithMany("Platforms")
                        .HasForeignKey("PlatformTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
