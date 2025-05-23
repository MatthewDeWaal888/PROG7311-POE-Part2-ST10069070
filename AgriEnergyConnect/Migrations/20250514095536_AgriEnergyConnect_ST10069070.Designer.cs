﻿// <auto-generated />
using System;
using AgriEnergyConnect.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgriEnergyConnect.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250514095536_AgriEnergyConnect_ST10069070")]
    partial class AgriEnergyConnect_ST10069070
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.5");

            modelBuilder.Entity("AgriEnergyConnect.Models.Tables.Discussion", b =>
                {
                    b.Property<int>("DiscussionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.HasKey("DiscussionID");

                    b.ToTable("Discussion");
                });

            modelBuilder.Entity("AgriEnergyConnect.Models.Tables.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CellphoneNumber")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasMaxLength(1)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.HasKey("EmployeeID");

                    b.ToTable("Employee");

                    b.HasData(
                        new
                        {
                            EmployeeID = 1,
                            CellphoneNumber = "123456789",
                            EmailAddress = "john@example.com",
                            FirstName = "John",
                            Gender = "M",
                            LastName = "Lucas",
                            Password = "abcd12!@",
                            UserName = "John"
                        },
                        new
                        {
                            EmployeeID = 2,
                            CellphoneNumber = "123456781",
                            EmailAddress = "oliver@example.com",
                            FirstName = "Oliver",
                            Gender = "M",
                            LastName = "Lucas",
                            Password = "abcd12!@",
                            UserName = "Oliver"
                        },
                        new
                        {
                            EmployeeID = 3,
                            CellphoneNumber = "123456782",
                            EmailAddress = "amanda@example.com",
                            FirstName = "Amanda",
                            Gender = "F",
                            LastName = "Nicole",
                            Password = "abcd12!@",
                            UserName = "Amanda"
                        });
                });

            modelBuilder.Entity("AgriEnergyConnect.Models.Tables.Farmer", b =>
                {
                    b.Property<int>("FarmerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CellphoneNumber")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasMaxLength(1)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.HasKey("FarmerID");

                    b.ToTable("Farmer");

                    b.HasData(
                        new
                        {
                            FarmerID = 1,
                            CellphoneNumber = "123456783",
                            EmailAddress = "marcus@example.com",
                            FirstName = "Marcus",
                            Gender = "M",
                            LastName = "John",
                            Password = "abcd12!@",
                            UserName = "Marcus"
                        },
                        new
                        {
                            FarmerID = 2,
                            CellphoneNumber = "123456784",
                            EmailAddress = "wyatt@example.com",
                            FirstName = "Wyatt",
                            Gender = "M",
                            LastName = "John",
                            Password = "abcd12!@",
                            UserName = "Wyatt"
                        });
                });

            modelBuilder.Entity("AgriEnergyConnect.Models.Tables.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<int>("FarmerID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductType")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ProductionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ProductID");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            Category = "Dairy",
                            FarmerID = 1,
                            Name = "Milk",
                            ProductType = "Dairy Product",
                            ProductionDate = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ProductID = 2,
                            Category = "Meat",
                            FarmerID = 1,
                            Name = "Chicken",
                            ProductType = "Meat Product",
                            ProductionDate = new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ProductID = 3,
                            Category = "Oil",
                            FarmerID = 1,
                            Name = "Oil",
                            ProductType = "Oil Product",
                            ProductionDate = new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ProductID = 4,
                            Category = "Wheat",
                            FarmerID = 2,
                            Name = "Bread",
                            ProductType = "Wheat Product",
                            ProductionDate = new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ProductID = 5,
                            Category = "Meat",
                            FarmerID = 2,
                            Name = "Eggs",
                            ProductType = "Meat Product",
                            ProductionDate = new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ProductID = 6,
                            Category = "Meat",
                            FarmerID = 2,
                            Name = "Lamb",
                            ProductType = "Meat Product",
                            ProductionDate = new DateTime(2025, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
