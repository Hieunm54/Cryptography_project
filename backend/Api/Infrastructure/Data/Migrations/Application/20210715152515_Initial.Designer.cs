﻿// <auto-generated />
using System;
using APITemplate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Infrastructure.Data.Migrations.Application
{
    [DbContext(typeof(ApplicationDatabaseContext))]
    [Migration("20210715152515_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("APITemplate.Domain.Entities.Block", b =>
                {
                    b.Property<Guid>("BlockID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Hash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Nonce")
                        .HasColumnType("int");

                    b.Property<string>("PreviousHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("BlockID");

                    b.ToTable("Block");
                });

            modelBuilder.Entity("APITemplate.Domain.Entities.BlockData", b =>
                {
                    b.Property<Guid>("BlockDataID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("BlockID")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DataUserID")
                        .HasColumnType("char(36)");

                    b.HasKey("BlockDataID");

                    b.ToTable("BlockData");
                });

            modelBuilder.Entity("APITemplate.Domain.Entities.DataUser", b =>
                {
                    b.Property<Guid>("DataUserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("DataUserID");

                    b.ToTable("DataUser");
                });

            modelBuilder.Entity("APITemplate.Domain.Entities.Exchange", b =>
                {
                    b.Property<Guid>("ExchangeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AddressFrom")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("AddressTo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("BlockID")
                        .HasColumnType("char(36)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("ExchangeID");

                    b.HasIndex("BlockID");

                    b.ToTable("Exchange");
                });

            modelBuilder.Entity("APITemplate.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("APITemplate.Models.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("book");
                });

            modelBuilder.Entity("APITemplate.Domain.Entities.Exchange", b =>
                {
                    b.HasOne("APITemplate.Domain.Entities.Block", null)
                        .WithMany("ListExchange")
                        .HasForeignKey("BlockID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("APITemplate.Domain.Entities.Block", b =>
                {
                    b.Navigation("ListExchange");
                });
#pragma warning restore 612, 618
        }
    }
}
