﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(LeadDb))]
    partial class LeadDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("api.Entities.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "admin",
                            Role = "admin",
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Password = "user",
                            Role = "user",
                            Username = "user"
                        });
                });

            modelBuilder.Entity("api.Entities.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BlogPost");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "This is a test blog post.",
                            ImageUrl = "https://ru.freepik.com/free-vector/hand-drawn-essay-illustration_40350252.htm#query=%D1%82%D0%B5%D1%81%D1%82&position=0&from_view=keyword&track=ais_hybrid&uuid=a0c7331a-7a90-477a-ae49-0e3691a60774",
                            PublishDate = new DateTime(2024, 8, 1, 20, 10, 13, 105, DateTimeKind.Local).AddTicks(6016),
                            Title = "Test Blog Post"
                        });
                });

            modelBuilder.Entity("api.Entities.ContactInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ContactInfos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "123 Test st, Test City, TC 12345",
                            Email = "contact@test.ru",
                            Phone = "123-456-7890"
                        });
                });

            modelBuilder.Entity("api.Entities.Lead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClientName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientQuestion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Contact")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Leads");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClientName = "Test Client",
                            ClientQuestion = "Test Question",
                            Contact = "test@mail.ru",
                            Date = new DateTime(2024, 8, 1, 20, 10, 13, 105, DateTimeKind.Local).AddTicks(5598),
                            Status = 0
                        });
                });

            modelBuilder.Entity("api.Entities.MainContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HeaderContent")
                        .HasColumnType("TEXT");

                    b.Property<string>("MenuButtonText")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MainContents");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            HeaderContent = "Welcom to Our Website",
                            MenuButtonText = "Learn More"
                        });
                });

            modelBuilder.Entity("api.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "This is test project.",
                            ImageUrl = "https://images.app.goo.gl/bE8dX7sMGcDCvv3V6",
                            Title = "Test project"
                        });
                });

            modelBuilder.Entity("api.Entities.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "This is a test service.",
                            Title = "Test Service"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
