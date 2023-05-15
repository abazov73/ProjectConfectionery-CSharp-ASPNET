﻿// <auto-generated />
using System;
using ConfectioneryDataBaseImplement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConfectioneryDataBaseImplement.Migrations
{
    [DbContext(typeof(ConfectioneryDatabase))]
    partial class ConfectioneryDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ConfectioneryDataBaseImplement.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<string>("IngredientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("ConfectioneryDataBaseImplement.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateImplement")
                        .HasColumnType("datetime2");

                    b.Property<int>("PastryId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("Sum")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("PastryId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ConfectioneryDataBaseImplement.Models.Pastry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PastryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Pastrys");
                });

            modelBuilder.Entity("ConfectioneryDataBaseImplement.Models.PastryIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<int>("PastryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("PastryId");

                    b.ToTable("PastryIngredients");
                });

            modelBuilder.Entity("ConfectioneryDataBaseImplement.Models.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("OpeningDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PastryCapacity")
                        .HasColumnType("int");

                    b.Property<string>("ShopAdress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShopName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("ConfectioneryDataBaseImplement.Models.ShopPastry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("PastryId")
                        .HasColumnType("int");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PastryId");

                    b.HasIndex("ShopId");

                    b.ToTable("ShopPastries");
                });

            modelBuilder.Entity("ConfectioneryDataBaseImplement.Models.Order", b =>
                {
                    b.HasOne("ConfectioneryDataBaseImplement.Models.Pastry", null)
                        .WithMany("Orders")
                        .HasForeignKey("PastryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConfectioneryDataBaseImplement.Models.PastryIngredient", b =>
                {
                    b.HasOne("ConfectioneryDataBaseImplement.Models.Ingredient", "Ingredient")
                        .WithMany("PastryIgredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConfectioneryDataBaseImplement.Models.Pastry", "Pastry")
                        .WithMany("Ingredients")
                        .HasForeignKey("PastryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Pastry");
                });

            modelBuilder.Entity("ConfectioneryDataBaseImplement.Models.ShopPastry", b =>
                {
                    b.HasOne("ConfectioneryDataBaseImplement.Models.Pastry", "Pastry")
                        .WithMany()
                        .HasForeignKey("PastryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConfectioneryDataBaseImplement.Models.Shop", "Shop")
                        .WithMany("Pastries")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pastry");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("ConfectioneryDataBaseImplement.Models.Ingredient", b =>
                {
                    b.Navigation("PastryIgredients");
                });

            modelBuilder.Entity("ConfectioneryDataBaseImplement.Models.Pastry", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ConfectioneryDataBaseImplement.Models.Shop", b =>
                {
                    b.Navigation("Pastries");
                });
#pragma warning restore 612, 618
        }
    }
}
