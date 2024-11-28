﻿// <auto-generated />
using System;
using JuanJacoboSummerGames.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JuanJacoboSummerGames.Data.SGMigrations
{
    [DbContext(typeof(SummerGamesContext))]
    partial class SummerGamesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.15");

            modelBuilder.Entity("JuanJacoboSummerGames.Models.Athlete", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Affiliation")
                        .HasColumnType("TEXT");

                    b.Property<string>("AthleteCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("ContingentID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<int>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("MediaInfo")
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<int>("SportID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("ID");

                    b.HasIndex("AthleteCode")
                        .IsUnique();

                    b.HasIndex("ContingentID");

                    b.HasIndex("SportID");

                    b.ToTable("Athletes");
                });

            modelBuilder.Entity("JuanJacoboSummerGames.Models.Contingent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Contingents");
                });

            modelBuilder.Entity("JuanJacoboSummerGames.Models.Sport", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("JuanJacoboSummerGames.Models.Athlete", b =>
                {
                    b.HasOne("JuanJacoboSummerGames.Models.Contingent", "Contingent")
                        .WithMany("Athletes")
                        .HasForeignKey("ContingentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JuanJacoboSummerGames.Models.Sport", "Sport")
                        .WithMany("Athletes")
                        .HasForeignKey("SportID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Contingent");

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("JuanJacoboSummerGames.Models.Contingent", b =>
                {
                    b.Navigation("Athletes");
                });

            modelBuilder.Entity("JuanJacoboSummerGames.Models.Sport", b =>
                {
                    b.Navigation("Athletes");
                });
#pragma warning restore 612, 618
        }
    }
}
