using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutoCompare_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedMasterServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "master_services",
                columns: new[] { "masterServiceId", "active", "category", "icon", "name", "serviceType" },
                values: new object[,]
                {
                    { 1, false, "", "", "Oil Change", "" },
                    { 2, false, "", "", "Filter Change", "" },
                    { 3, false, "", "", "Fluid Exchange", "" },
                    { 4, false, "", "", "Tire Services", "" },
                    { 5, false, "", "", "Filter Replacement", "" },
                    { 6, false, "", "", "Battery Testing & Replacement", "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "master_services",
                keyColumn: "masterServiceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "master_services",
                keyColumn: "masterServiceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "master_services",
                keyColumn: "masterServiceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "master_services",
                keyColumn: "masterServiceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "master_services",
                keyColumn: "masterServiceId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "master_services",
                keyColumn: "masterServiceId",
                keyValue: 6);
        }
    }
}
