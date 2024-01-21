using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Audit.Infrastructure.Persistence.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddForecourtAsAggregateRoot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forecourts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecourts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lanes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ForecourtId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lanes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lanes_Forecourts_ForecourtId",
                        column: x => x.ForecourtId,
                        principalTable: "Forecourts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pumps_LaneId",
                table: "Pumps",
                column: "LaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Lanes_ForecourtId",
                table: "Lanes",
                column: "ForecourtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pumps_Lanes_LaneId",
                table: "Pumps",
                column: "LaneId",
                principalTable: "Lanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pumps_Lanes_LaneId",
                table: "Pumps");

            migrationBuilder.DropTable(
                name: "Lanes");

            migrationBuilder.DropTable(
                name: "Forecourts");

            migrationBuilder.DropIndex(
                name: "IX_Pumps_LaneId",
                table: "Pumps");
        }
    }
}
