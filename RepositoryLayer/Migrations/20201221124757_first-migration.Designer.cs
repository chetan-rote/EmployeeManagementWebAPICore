﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepositoryLayer.EmployeeDbContext;

namespace RepositoryLayer.Migrations
{
    [DbContext(typeof(EmployeeContext))]
    [Migration("20201221124757_first-migration")]
    partial class firstmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RepositoryLayer.ContextModel.CompanyEmployee", b =>
                {
                    b.Property<long>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDateTime");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime?>("ModifiedDateTime");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Role");

                    b.HasKey("EmployeeId");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
