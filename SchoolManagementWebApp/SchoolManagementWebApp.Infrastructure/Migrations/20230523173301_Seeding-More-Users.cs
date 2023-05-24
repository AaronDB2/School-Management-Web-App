using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolManagementWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingMoreUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8c66808c-5a90-47fd-8d25-bbe3f5ac1985"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1fc7d5af-f603-407f-aa41-915794ce250a", "AQAAAAIAAYagAAAAEC2MvucQ/UWGMeDCWFuWhSjfIh3TvfMmysJK5tLa7uwKcX5ijJ7GDdXRvC5B3JtG7A==" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("6e0cbcd5-3807-4813-95d1-930b9a220f27"), 0, "521ef9eb-ec19-453a-aa4e-b3dfa3a04f58", "teacher@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEFbXUaBGcY8apt6Sf4v9Bsq6J1cdqPlq17DgJF8RiL/dpZpTQSW4CK+NSRQSy8QHsA==", null, false, null, false, "Teacher" },
                    { new Guid("d0a86355-484e-48e0-89e5-68735ce5ec3c"), 0, "f839d8a6-ba24-4e92-967a-8a2c49e576ba", "student@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEJtBKtf31H8ifNRHWuzikv2H5DB5Zs6fawB8/g2ELVEqbHKR+ID/YSdVLx7wcwCPJQ==", null, false, null, false, "Student" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("6c4cb79b-4863-4d1b-bfbd-a2c9b78543e7"), new Guid("6e0cbcd5-3807-4813-95d1-930b9a220f27") },
                    { new Guid("09a8113e-55dd-422e-bee0-8cef1af547e2"), new Guid("d0a86355-484e-48e0-89e5-68735ce5ec3c") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6c4cb79b-4863-4d1b-bfbd-a2c9b78543e7"), new Guid("6e0cbcd5-3807-4813-95d1-930b9a220f27") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("09a8113e-55dd-422e-bee0-8cef1af547e2"), new Guid("d0a86355-484e-48e0-89e5-68735ce5ec3c") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6e0cbcd5-3807-4813-95d1-930b9a220f27"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d0a86355-484e-48e0-89e5-68735ce5ec3c"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8c66808c-5a90-47fd-8d25-bbe3f5ac1985"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3fb81fec-3ea8-4a77-8468-7e5ee72aa993", "AQAAAAIAAYagAAAAEAasQpbogMC8t8o3JbOssBdxlggsySvZxV4k92kkLyxE3rvXLlQb0jHpkAGA9mKvpQ==" });
        }
    }
}
