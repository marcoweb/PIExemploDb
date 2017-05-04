using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PIExemploDb.Models;

namespace PIExemploDb.Migrations
{
    [DbContext(typeof(LivrosDbContext))]
    partial class LivrosDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("PIExemploDb.Models.Genero", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("ID");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("PIExemploDb.Models.Livro", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GeneroId");

                    b.Property<string>("Titulo");

                    b.HasKey("ID");

                    b.HasIndex("GeneroId");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("PIExemploDb.Models.Livro", b =>
                {
                    b.HasOne("PIExemploDb.Models.Genero", "Genero")
                        .WithMany("Livros")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
