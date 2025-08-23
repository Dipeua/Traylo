using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Traylo.Migrations
{
    /// <inheritdoc />
    public partial class tr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryPersonTrackings");

            migrationBuilder.CreateTable(
                name: "StockSuivis",
                columns: table => new
                {
                    StockSuiviId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeliveryPersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    SI = table.Column<int>(type: "INTEGER", nullable: false),
                    Entrees = table.Column<int>(type: "INTEGER", nullable: false),
                    Sorties = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockSuivis", x => x.StockSuiviId);
                    table.ForeignKey(
                        name: "FK_StockSuivis_DeliveryPeople_DeliveryPersonId",
                        column: x => x.DeliveryPersonId,
                        principalTable: "DeliveryPeople",
                        principalColumn: "DeliveryPersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockSuivis_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockSuivis_DeliveryPersonId",
                table: "StockSuivis",
                column: "DeliveryPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_StockSuivis_ProductId",
                table: "StockSuivis",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockSuivis");

            migrationBuilder.CreateTable(
                name: "DeliveryPersonTrackings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeliveryPersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Entrees = table.Column<int>(type: "INTEGER", nullable: false),
                    Sorties = table.Column<int>(type: "INTEGER", nullable: false),
                    StockFinal = table.Column<int>(type: "INTEGER", nullable: false),
                    StockInitial = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPersonTrackings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryPersonTrackings_DeliveryPeople_DeliveryPersonId",
                        column: x => x.DeliveryPersonId,
                        principalTable: "DeliveryPeople",
                        principalColumn: "DeliveryPersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryPersonTrackings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPersonTrackings_DeliveryPersonId",
                table: "DeliveryPersonTrackings",
                column: "DeliveryPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPersonTrackings_ProductId",
                table: "DeliveryPersonTrackings",
                column: "ProductId");
        }
    }
}
