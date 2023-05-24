using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCoursedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6e0cbcd5-3807-4813-95d1-930b9a220f27"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b40455ad-b54d-42b2-96ba-165096c0a147", "AQAAAAIAAYagAAAAEHmCkzXrNTVx1nnPVR1YuYTPXEBxkRibcmtPM0Ju+Etsbria0/X9Wg6ZX0056vxR5Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8c66808c-5a90-47fd-8d25-bbe3f5ac1985"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e992d2c3-2844-4599-bbcd-ed70ec482d5c", "AQAAAAIAAYagAAAAEJFEM0YwDXMzbyYIEW174z2iPZlecJWbZKTCnOJ1wfx14zt/XZ6+FgDSCjxHBX/pJw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d0a86355-484e-48e0-89e5-68735ce5ec3c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "725130c8-a4c0-4c05-a68e-151f95090351", "AQAAAAIAAYagAAAAEGm24y3RYNHEwNxzwUPti5o2q2Z2sl/+//fA1bFWzFsmq+gCMSwTYAmb7H1RgEUiLw==" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseFileName", "CourseName", "CourseText", "Message", "TeacherId" },
                values: new object[] { new Guid("e5376ece-7e42-4604-a3a2-23d69383e8f2"), "Test.pdf", "Test Course", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", "\"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\"", new Guid("6e0cbcd5-3807-4813-95d1-930b9a220f27") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("e5376ece-7e42-4604-a3a2-23d69383e8f2"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6e0cbcd5-3807-4813-95d1-930b9a220f27"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "521ef9eb-ec19-453a-aa4e-b3dfa3a04f58", "AQAAAAIAAYagAAAAEFbXUaBGcY8apt6Sf4v9Bsq6J1cdqPlq17DgJF8RiL/dpZpTQSW4CK+NSRQSy8QHsA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8c66808c-5a90-47fd-8d25-bbe3f5ac1985"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1fc7d5af-f603-407f-aa41-915794ce250a", "AQAAAAIAAYagAAAAEC2MvucQ/UWGMeDCWFuWhSjfIh3TvfMmysJK5tLa7uwKcX5ijJ7GDdXRvC5B3JtG7A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d0a86355-484e-48e0-89e5-68735ce5ec3c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f839d8a6-ba24-4e92-967a-8a2c49e576ba", "AQAAAAIAAYagAAAAEJtBKtf31H8ifNRHWuzikv2H5DB5Zs6fawB8/g2ELVEqbHKR+ID/YSdVLx7wcwCPJQ==" });
        }
    }
}
