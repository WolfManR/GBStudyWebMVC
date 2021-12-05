﻿// <auto-generated />
using DataBase.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MigrationsLauncher.Migrations
{
    [DbContext(typeof(KittensContext))]
    [Migration("20210716194251_Clinic")]
    partial class Clinic
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ClinicKitten", b =>
                {
                    b.Property<int>("ClinicsId")
                        .HasColumnType("integer");

                    b.Property<int>("KittensId")
                        .HasColumnType("integer");

                    b.HasKey("ClinicsId", "KittensId");

                    b.HasIndex("KittensId");

                    b.ToTable("ClinicKitten");
                });

            modelBuilder.Entity("DataBase.Abstractions.Entities.Clinic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("DataBase.Abstractions.Entities.Kitten", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<string>("Feed")
                        .HasColumnType("text");

                    b.Property<bool>("HasCertificate")
                        .HasColumnType("boolean");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Kittens");
                });

            modelBuilder.Entity("ClinicKitten", b =>
                {
                    b.HasOne("DataBase.Abstractions.Entities.Clinic", null)
                        .WithMany()
                        .HasForeignKey("ClinicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataBase.Abstractions.Entities.Kitten", null)
                        .WithMany()
                        .HasForeignKey("KittensId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
