﻿// <auto-generated />
using EcommerceWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcommerceWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241019193519_AddProductTableToDb")]
    partial class AddProductTableToDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EcommerceWebApp.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Mobiles"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "TVs"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Laptop"
                        });
                });

            modelBuilder.Entity("EcommerceWebApp.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Samsung Galaxy S24, 8GB RAM, 256GB ROM, 48MP Camera",
                            Name = "Samsung Galaxy S24",
                            Price = 80000.0
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "Samsung Galaxy S23, 8GB RAM, 128GB ROM, 32MP Camera",
                            Name = "Samsung Galaxy S23",
                            Price = 70000.0
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Apple iPhone 15, 6GB RAM, 128GB ROM, 32MP Camera",
                            Name = "Apple iPhone 15",
                            Price = 100000.0
                        });
                });

            modelBuilder.Entity("EcommerceWebApp.Models.Product", b =>
                {
                    b.HasOne("EcommerceWebApp.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EcommerceWebApp.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
