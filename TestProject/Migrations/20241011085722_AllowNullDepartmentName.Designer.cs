﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestProject.Domain;

#nullable disable

namespace TestProject.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241011085722_AllowNullDepartmentName")]
    partial class AllowNullDepartmentName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TestProject.Base.Domain.ProcessCategory", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("ProcessCategories");
                });

            modelBuilder.Entity("TestProject.Domain.Model.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentID"), 1L, 1);

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("TestProject.Domain.Model.Process", b =>
                {
                    b.Property<int>("ProcessID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProcessID"), 1L, 1);

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int?>("OwnerDepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("ProcessCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProcessName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProcessID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("OwnerDepartmentID");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("TestProject.Domain.Model.Process", b =>
                {
                    b.HasOne("TestProject.Base.Domain.ProcessCategory", "Category")
                        .WithMany("Processes")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestProject.Domain.Model.Department", "Department")
                        .WithMany("Processes")
                        .HasForeignKey("OwnerDepartmentID");

                    b.Navigation("Category");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("TestProject.Base.Domain.ProcessCategory", b =>
                {
                    b.Navigation("Processes");
                });

            modelBuilder.Entity("TestProject.Domain.Model.Department", b =>
                {
                    b.Navigation("Processes");
                });
#pragma warning restore 612, 618
        }
    }
}
