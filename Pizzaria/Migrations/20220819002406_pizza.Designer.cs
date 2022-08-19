﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pizzaria_G11.Data;

namespace Pizzaria.Migrations
{
    [DbContext(typeof(PizzariaDbContext))]
    [Migration("20220819002406_pizza")]
    partial class pizza
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pizzaria.Models.Pizza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagemURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("TamanhoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TamanhoId");

                    b.ToTable("Pizzas");
                });

            modelBuilder.Entity("Pizzaria.Models.PizzasSabores", b =>
                {
                    b.Property<int>("PizzaId")
                        .HasColumnType("int");

                    b.Property<int>("SaborId")
                        .HasColumnType("int");

                    b.Property<int?>("TamanhoId")
                        .HasColumnType("int");

                    b.HasKey("PizzaId", "SaborId");

                    b.HasIndex("SaborId");

                    b.HasIndex("TamanhoId");

                    b.ToTable("PizzasSabores");
                });

            modelBuilder.Entity("Pizzaria.Models.Sabor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagemURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sabores");
                });

            modelBuilder.Entity("Pizzaria.Models.Tamanho", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tamanhos");
                });

            modelBuilder.Entity("Pizzaria.Models.Pizza", b =>
                {
                    b.HasOne("Pizzaria.Models.Tamanho", "Tamanho")
                        .WithMany("Pizzas")
                        .HasForeignKey("TamanhoId");

                    b.Navigation("Tamanho");
                });

            modelBuilder.Entity("Pizzaria.Models.PizzasSabores", b =>
                {
                    b.HasOne("Pizzaria.Models.Pizza", "Pizza")
                        .WithMany("PizzasSabores")
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pizzaria.Models.Sabor", null)
                        .WithMany("PizzasSabores")
                        .HasForeignKey("SaborId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pizzaria.Models.Tamanho", "Tamanho")
                        .WithMany()
                        .HasForeignKey("TamanhoId");

                    b.Navigation("Pizza");

                    b.Navigation("Tamanho");
                });

            modelBuilder.Entity("Pizzaria.Models.Pizza", b =>
                {
                    b.Navigation("PizzasSabores");
                });

            modelBuilder.Entity("Pizzaria.Models.Sabor", b =>
                {
                    b.Navigation("PizzasSabores");
                });

            modelBuilder.Entity("Pizzaria.Models.Tamanho", b =>
                {
                    b.Navigation("Pizzas");
                });
#pragma warning restore 612, 618
        }
    }
}
