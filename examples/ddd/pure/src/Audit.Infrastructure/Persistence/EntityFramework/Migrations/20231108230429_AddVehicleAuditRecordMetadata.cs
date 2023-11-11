using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Audit.Infrastructure.Persistence.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleAuditRecordMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehiclesAudit");

            migrationBuilder.CreateTable(
                name: "VehicleAuditRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    FuelType = table.Column<int>(type: "integer", nullable: false),
                    FuelLevel = table.Column<int>(type: "integer", nullable: false),
                    TankCapacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleAuditRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleAuditRecords_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleAuditRecordMetadata",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleAuditRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyName = table.Column<string>(type: "text", nullable: false),
                    OriginalValue = table.Column<string>(type: "text", nullable: true),
                    UpdatedValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleAuditRecordMetadata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleAuditRecordMetadata_VehicleAuditRecords_VehicleAudit~",
                        column: x => x.VehicleAuditRecordId,
                        principalTable: "VehicleAuditRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAuditRecordMetadata_VehicleAuditRecordId",
                table: "VehicleAuditRecordMetadata",
                column: "VehicleAuditRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAuditRecords_VehicleId",
                table: "VehicleAuditRecords",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleAuditRecordMetadata");

            migrationBuilder.DropTable(
                name: "VehicleAuditRecords");

            migrationBuilder.CreateTable(
                name: "VehiclesAudit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    FuelLevel = table.Column<int>(type: "integer", nullable: false),
                    FuelType = table.Column<int>(type: "integer", nullable: false),
                    TankCapacity = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclesAudit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehiclesAudit_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehiclesAudit_VehicleId",
                table: "VehiclesAudit",
                column: "VehicleId");
        }
    }
}
