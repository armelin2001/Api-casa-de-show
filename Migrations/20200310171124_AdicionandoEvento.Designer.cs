﻿// <auto-generated />
using System;
using Api_casa_de_show.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api_casa_de_show.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200310171124_AdicionandoEvento")]
    partial class AdicionandoEvento
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Api_casa_de_show.Models.CasaDeShow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Endereco")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NomeCasaDeShow")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("CasasDeShows");
                });

            modelBuilder.Entity("Api_casa_de_show.Models.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Capacidade")
                        .HasColumnType("int");

                    b.Property<int?>("CasaDeShowsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataEvento")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("GeneroDoEventoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("HorarioEvento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NomeDoEvento")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float>("PrecoIngresso")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CasaDeShowsId");

                    b.HasIndex("GeneroDoEventoId");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("Api_casa_de_show.Models.GeneroEvento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NomeGenero")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("Api_casa_de_show.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UltimoNome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Api_casa_de_show.Models.Venda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.Property<int>("QtdIngresso")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<float>("ValorCompra")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.HasIndex("UserId");

                    b.ToTable("Vendas");
                });

            modelBuilder.Entity("Api_casa_de_show.Models.Evento", b =>
                {
                    b.HasOne("Api_casa_de_show.Models.CasaDeShow", "CasaDeShow")
                        .WithMany()
                        .HasForeignKey("CasaDeShowsId");

                    b.HasOne("Api_casa_de_show.Models.GeneroEvento", "GeneroEvento")
                        .WithMany()
                        .HasForeignKey("GeneroDoEventoId");
                });

            modelBuilder.Entity("Api_casa_de_show.Models.Venda", b =>
                {
                    b.HasOne("Api_casa_de_show.Models.Evento", "Evento")
                        .WithMany()
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api_casa_de_show.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
