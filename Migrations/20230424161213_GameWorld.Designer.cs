﻿// <auto-generated />
using System;
using GameWORLD.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameWORLD.Migrations
{
    [DbContext(typeof(GameWORLDContext))]
    [Migration("20230424161213_GameWorld")]
    partial class GameWorld
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GameWORLD.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("GameWORLD.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PublishingCompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SoftwareDeveloperId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PublishingCompanyId");

                    b.HasIndex("SoftwareDeveloperId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("GameWORLD.Models.GameGameGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameGenreId")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameGenreId");

                    b.HasIndex("GameId");

                    b.ToTable("GameGameGenre");
                });

            modelBuilder.Entity("GameWORLD.Models.GameGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameCategory")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameGenre");
                });

            modelBuilder.Entity("GameWORLD.Models.PublishingCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameCompany")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PublishingCompany");
                });

            modelBuilder.Entity("GameWORLD.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Review")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("GameId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("GameWORLD.Models.SoftwareDeveloper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameDeveloper")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SoftwareDeveloper");
                });

            modelBuilder.Entity("GameWORLD.Models.Game", b =>
                {
                    b.HasOne("GameWORLD.Models.PublishingCompany", "PublishingCompany")
                        .WithMany("Game")
                        .HasForeignKey("PublishingCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameWORLD.Models.SoftwareDeveloper", "SoftwareDeveloper")
                        .WithMany("Game")
                        .HasForeignKey("SoftwareDeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PublishingCompany");

                    b.Navigation("SoftwareDeveloper");
                });

            modelBuilder.Entity("GameWORLD.Models.GameGameGenre", b =>
                {
                    b.HasOne("GameWORLD.Models.GameGenre", "GameGenre")
                        .WithMany("GameGameGenre")
                        .HasForeignKey("GameGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameWORLD.Models.Game", "Game")
                        .WithMany("GameGameGenre")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("GameGenre");
                });

            modelBuilder.Entity("GameWORLD.Models.Rating", b =>
                {
                    b.HasOne("GameWORLD.Models.Customer", "Customer")
                        .WithMany("Rating")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameWORLD.Models.Game", "Game")
                        .WithMany("Rating")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("GameWORLD.Models.Customer", b =>
                {
                    b.Navigation("Rating");
                });

            modelBuilder.Entity("GameWORLD.Models.Game", b =>
                {
                    b.Navigation("GameGameGenre");

                    b.Navigation("Rating");
                });

            modelBuilder.Entity("GameWORLD.Models.GameGenre", b =>
                {
                    b.Navigation("GameGameGenre");
                });

            modelBuilder.Entity("GameWORLD.Models.PublishingCompany", b =>
                {
                    b.Navigation("Game");
                });

            modelBuilder.Entity("GameWORLD.Models.SoftwareDeveloper", b =>
                {
                    b.Navigation("Game");
                });
#pragma warning restore 612, 618
        }
    }
}
