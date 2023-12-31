﻿// <auto-generated />
using System;
using Audit.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Audit.Infrastructure.Persistence.EntityFramework.Migrations
{
    [DbContext(typeof(AuditDbContext))]
    [Migration("20231108230429_AddVehicleAuditRecordMetadata")]
    partial class AddVehicleAuditRecordMetadata
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Audit.Domain.Model.Vehicles.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("FuelLevel")
                        .HasColumnType("integer");

                    b.Property<int>("FuelType")
                        .HasColumnType("integer");

                    b.Property<int>("TankCapacity")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Vehicles", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Audit.Domain.Model.Vehicles.VehicleAuditRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("FuelLevel")
                        .HasColumnType("integer");

                    b.Property<int>("FuelType")
                        .HasColumnType("integer");

                    b.Property<int>("TankCapacity")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleAuditRecords", (string)null);
                });

            modelBuilder.Entity("Audit.Domain.Model.Vehicles.VehicleAuditRecordMetadata", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("OriginalValue")
                        .HasColumnType("text");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UpdatedValue")
                        .HasColumnType("text");

                    b.Property<Guid>("VehicleAuditRecordId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VehicleAuditRecordId");

                    b.ToTable("VehicleAuditRecordMetadata", (string)null);
                });

            modelBuilder.Entity("Audit.Domain.Model.Vehicles.Car", b =>
                {
                    b.HasBaseType("Audit.Domain.Model.Vehicles.Vehicle");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Audit.Domain.Model.Vehicles.Hgv", b =>
                {
                    b.HasBaseType("Audit.Domain.Model.Vehicles.Vehicle");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Audit.Domain.Model.Vehicles.Van", b =>
                {
                    b.HasBaseType("Audit.Domain.Model.Vehicles.Vehicle");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Audit.Domain.Model.Vehicles.VehicleAuditRecord", b =>
                {
                    b.HasOne("Audit.Domain.Model.Vehicles.Vehicle", "Vehicle")
                        .WithMany("AuditRecords")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Audit.Domain.Model.Vehicles.VehicleAuditRecordMetadata", b =>
                {
                    b.HasOne("Audit.Domain.Model.Vehicles.VehicleAuditRecord", null)
                        .WithMany("Metadata")
                        .HasForeignKey("VehicleAuditRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Audit.Domain.Model.Vehicles.Vehicle", b =>
                {
                    b.Navigation("AuditRecords");
                });

            modelBuilder.Entity("Audit.Domain.Model.Vehicles.VehicleAuditRecord", b =>
                {
                    b.Navigation("Metadata");
                });
#pragma warning restore 612, 618
        }
    }
}
