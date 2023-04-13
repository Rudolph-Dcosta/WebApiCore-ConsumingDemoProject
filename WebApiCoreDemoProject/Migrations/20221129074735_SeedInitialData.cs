using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiCoreDemoProject.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Area", "AreaName", "Code", "Latitude", "Longitude", "Population" },
                values: new object[,]
                {
                    { 1, 13789.0, "Northland Region", "NRTHL", -35.370830400000003, 172.57178250000001, 194600L },
                    { 2, 4894.0, "Auckland Region", "AUCK", -36.525320700000002, 173.77857040000001, 1718982L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
