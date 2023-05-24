using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("09a8113e-55dd-422e-bee0-8cef1af547e2"),
                column: "NormalizedName",
                value: "STUDENT");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6c4cb79b-4863-4d1b-bfbd-a2c9b78543e7"),
                column: "NormalizedName",
                value: "TEACHER");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("df2b31ef-1940-40b1-976d-9a251b84512d"),
                column: "NormalizedName",
                value: "ADMIN");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("df2b31ef-1940-40b1-976d-9a251b84512d"), new Guid("8c66808c-5a90-47fd-8d25-bbe3f5ac1985") });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8c66808c-5a90-47fd-8d25-bbe3f5ac1985"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3fb81fec-3ea8-4a77-8468-7e5ee72aa993", "AQAAAAIAAYagAAAAEAasQpbogMC8t8o3JbOssBdxlggsySvZxV4k92kkLyxE3rvXLlQb0jHpkAGA9mKvpQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("df2b31ef-1940-40b1-976d-9a251b84512d"), new Guid("8c66808c-5a90-47fd-8d25-bbe3f5ac1985") });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("09a8113e-55dd-422e-bee0-8cef1af547e2"),
                column: "NormalizedName",
                value: "Student");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6c4cb79b-4863-4d1b-bfbd-a2c9b78543e7"),
                column: "NormalizedName",
                value: "Teacher");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("df2b31ef-1940-40b1-976d-9a251b84512d"),
                column: "NormalizedName",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8c66808c-5a90-47fd-8d25-bbe3f5ac1985"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "84ed1787-79d9-41ec-996c-09f2238cf7e5", null });
        }
    }
}
