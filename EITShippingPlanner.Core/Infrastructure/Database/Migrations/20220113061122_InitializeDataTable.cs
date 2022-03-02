using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EITShippingPlanner.Core.Infrastructure.Database.Migrations
{
    public partial class InitializeDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CargoCenterLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoCenterLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExtraCharges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParcelType = table.Column<int>(nullable: false),
                    Percentage = table.Column<float>(nullable: false),
                    IsSupported = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraCharges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParcelPrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    LowerWeight = table.Column<float>(nullable: false),
                    UpperWeight = table.Column<float>(nullable: false),
                    Price = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstLocationId = table.Column<int>(nullable: true),
                    SecondLocationId = table.Column<int>(nullable: true),
                    TransportationType = table.Column<int>(nullable: false),
                    NumberOfSegment = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_CargoCenterLocations_FirstLocationId",
                        column: x => x.FirstLocationId,
                        principalTable: "CargoCenterLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Routes_CargoCenterLocations_SecondLocationId",
                        column: x => x.SecondLocationId,
                        principalTable: "CargoCenterLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargoCenterLocations_Code",
                table: "CargoCenterLocations",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_CargoCenterLocations_Id",
                table: "CargoCenterLocations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraCharges_Id",
                table: "ExtraCharges",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraCharges_ParcelType",
                table: "ExtraCharges",
                column: "ParcelType");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelPrices_Id",
                table: "ParcelPrices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_FirstLocationId",
                table: "Routes",
                column: "FirstLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_Id",
                table: "Routes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_SecondLocationId",
                table: "Routes",
                column: "SecondLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_TransportationType",
                table: "Routes",
                column: "TransportationType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraCharges");

            migrationBuilder.DropTable(
                name: "ParcelPrices");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "CargoCenterLocations");
        }
    }
}
