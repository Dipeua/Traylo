using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Traylo.Migrations
{
    /// <inheritdoc />
    public partial class addNewUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "PasswordHash", "Role", "Username" },
                values: new object[] { "admin", 0, "Dipeua" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CityId", "PasswordHash", "Role", "Username" },
                values: new object[] { 3, null, "manager", 1, "Stephanie" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "PasswordHash", "Role", "Username" },
                values: new object[] { "manager", 1, "Stephanie" });
        }
    }
}
