﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TC.Usuarios.Infra.Data;

namespace TC.Usuarios.Infra.Migrations
{
    [DbContext(typeof(UsuariosContext))]
    [Migration("20220527034039_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TC.Usuarios.Domain.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataNascimento")
                        .IsRequired()
                        .HasColumnType("datetime")
                        .HasColumnName("DataNascimento");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Email");

                    b.Property<int>("Escolaridade")
                        .HasColumnType("int")
                        .HasColumnName("Escolaridade");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("TC.Usuarios.Domain.Usuario", b =>
                {
                    b.OwnsOne("TC.Core.DomainObjects.Nome", "Nome", b1 =>
                        {
                            b1.Property<int>("UsuarioId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("PrimeiroNome")
                                .IsRequired()
                                .HasColumnType("varchar(50)")
                                .HasColumnName("PrimeiroNome");

                            b1.Property<string>("UltimoNome")
                                .IsRequired()
                                .HasColumnType("varchar(50)")
                                .HasColumnName("UltimoNome");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuario");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.Navigation("Nome");
                });
#pragma warning restore 612, 618
        }
    }
}
