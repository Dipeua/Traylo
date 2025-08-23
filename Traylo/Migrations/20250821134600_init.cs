using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Traylo.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryPeople",
                columns: table => new
                {
                    DeliveryPersonId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPeople", x => x.DeliveryPersonId);
                    table.ForeignKey(
                        name: "FK_DeliveryPeople_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    InitQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityDelivery = table.Column<int>(type: "INTEGER", nullable: true),
                    QuantityReste = table.Column<int>(type: "INTEGER", nullable: true),
                    DeliveryPersonId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_DeliveryPeople_DeliveryPersonId",
                        column: x => x.DeliveryPersonId,
                        principalTable: "DeliveryPeople",
                        principalColumn: "DeliveryPersonId");
                });

            migrationBuilder.CreateTable(
                name: "StockHistories",
                columns: table => new
                {
                    StockHistoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: true),
                    DeliveryPersonId = table.Column<int>(type: "INTEGER", nullable: true),
                    QuantityChange = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockHistories", x => x.StockHistoryId);
                    table.ForeignKey(
                        name: "FK_StockHistories_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_StockHistories_DeliveryPeople_DeliveryPersonId",
                        column: x => x.DeliveryPersonId,
                        principalTable: "DeliveryPeople",
                        principalColumn: "DeliveryPersonId");
                    table.ForeignKey(
                        name: "FK_StockHistories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "DeliveryPersonId", "InitQuantity", "Name", "QuantityDelivery", "QuantityReste" },
                values: new object[,]
                {
                    { 1, null, 0, "CARDIHIT", null, null },
                    { 2, null, 0, "ST HEART", null, null },
                    { 3, null, 0, "DIABETIC", null, null },
                    { 4, null, 0, "XENOPROST ACTIVE", null, null },
                    { 5, null, 0, "SLIMUX", null, null },
                    { 6, null, 0, "PROSTUROX", null, null },
                    { 7, null, 0, "FAST ACTIVE", null, null },
                    { 8, null, 0, "GLUCOZEIN", null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CityId", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, null, "admin", 0, "Paulin" },
                    { 2, null, "manager", 1, "Stephanie" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPeople_CityId",
                table: "DeliveryPeople",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DeliveryPersonId",
                table: "Products",
                column: "DeliveryPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_CityId",
                table: "StockHistories",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_DeliveryPersonId",
                table: "StockHistories",
                column: "DeliveryPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_ProductId",
                table: "StockHistories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockHistories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "DeliveryPeople");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
