﻿// <auto-generated />
using System;
using CiaAerea.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CiaAerea.Migrations
{
    [DbContext(typeof(CiaAereaContext))]
    partial class CiaAereaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CiaAerea.Entities.Aeronave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fabricante")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Aeronaves");
                });

            modelBuilder.Entity("CiaAerea.Entities.Cancelamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DataHoraNotificacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VooId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VooId")
                        .IsUnique();

                    b.ToTable("Cancelamentos");
                });

            modelBuilder.Entity("CiaAerea.Entities.Manutencao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AeronaveId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observacoes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AeronaveId");

                    b.ToTable("Manutencoes");
                });

            modelBuilder.Entity("CiaAerea.Entities.Piloto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pilotos");
                });

            modelBuilder.Entity("CiaAerea.Entities.Voo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AeronaveId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHoraChegada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataHoraPartida")
                        .HasColumnType("datetime2");

                    b.Property<string>("Destino")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Origem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PilotoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AeronaveId");

                    b.HasIndex("PilotoId");

                    b.ToTable("Voos");
                });

            modelBuilder.Entity("CiaAerea.Entities.Cancelamento", b =>
                {
                    b.HasOne("CiaAerea.Entities.Voo", "Voo")
                        .WithOne("Cancelamento")
                        .HasForeignKey("CiaAerea.Entities.Cancelamento", "VooId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Voo");
                });

            modelBuilder.Entity("CiaAerea.Entities.Manutencao", b =>
                {
                    b.HasOne("CiaAerea.Entities.Aeronave", "Aeronave")
                        .WithMany("Manutencoes")
                        .HasForeignKey("AeronaveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aeronave");
                });

            modelBuilder.Entity("CiaAerea.Entities.Voo", b =>
                {
                    b.HasOne("CiaAerea.Entities.Aeronave", "Aeronave")
                        .WithMany("Voos")
                        .HasForeignKey("AeronaveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CiaAerea.Entities.Piloto", "Piloto")
                        .WithMany("Voos")
                        .HasForeignKey("PilotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aeronave");

                    b.Navigation("Piloto");
                });

            modelBuilder.Entity("CiaAerea.Entities.Aeronave", b =>
                {
                    b.Navigation("Manutencoes");

                    b.Navigation("Voos");
                });

            modelBuilder.Entity("CiaAerea.Entities.Piloto", b =>
                {
                    b.Navigation("Voos");
                });

            modelBuilder.Entity("CiaAerea.Entities.Voo", b =>
                {
                    b.Navigation("Cancelamento");
                });
#pragma warning restore 612, 618
        }
    }
}
