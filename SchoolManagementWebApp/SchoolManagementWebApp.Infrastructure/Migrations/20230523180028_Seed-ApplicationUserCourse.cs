using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedApplicationUserCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationUserCourse",
                columns: new[] { "CoursesCourseId", "StudentsId" },
                values: new object[] { new Guid("e5376ece-7e42-4604-a3a2-23d69383e8f2"), new Guid("d0a86355-484e-48e0-89e5-68735ce5ec3c") });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6e0cbcd5-3807-4813-95d1-930b9a220f27"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "84ed1099-5b54-47f0-919c-f8da647da615", "AQAAAAIAAYagAAAAEM2wd8R6PZqAQeFy4IWQ9BAY1OxAJ9tvVfPFn6O1bzrdsUNSyVu88LMF6h0aODZAvg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8c66808c-5a90-47fd-8d25-bbe3f5ac1985"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "eae8d7c4-8e2d-4dce-a078-341e84f40806", "AQAAAAIAAYagAAAAEPmCshGKbLC116Yo2jDmH2EQit6+xQg9vJa9KmzGE8HrqIyzuwE6JESHv+9MVnlDmA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d0a86355-484e-48e0-89e5-68735ce5ec3c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6b55e008-7348-4cff-bb4c-0709ccc713d6", "AQAAAAIAAYagAAAAEMWtABvG6xd9whY5FH0P8tUToMyWURVjZxPRFqYzHapAZgXAXJRDzf1KQbZVrOX1ag==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationUserCourse",
                keyColumns: new[] { "CoursesCourseId", "StudentsId" },
                keyValues: new object[] { new Guid("e5376ece-7e42-4604-a3a2-23d69383e8f2"), new Guid("d0a86355-484e-48e0-89e5-68735ce5ec3c") });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6e0cbcd5-3807-4813-95d1-930b9a220f27"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1140e516-61fa-41dc-8709-0ecbbf321e9b", "AQAAAAIAAYagAAAAEM3XFitObb6UaoX8yqGHtn+gxmIheSqTfaMTIV5NSiRdWPnqmGeu+iHqN4myb4He2Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8c66808c-5a90-47fd-8d25-bbe3f5ac1985"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9bc4c9c2-3369-4854-bc05-6ee07f80dbcc", "AQAAAAIAAYagAAAAEMC6jioJSjUm5w5MTzjqkaJDPP2PAuh6xXjBHqdSxomH1Wpb1aHZlUrHWsYsxD3XwA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d0a86355-484e-48e0-89e5-68735ce5ec3c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d8a9f8fd-038a-47fb-8553-b50d82619285", "AQAAAAIAAYagAAAAEGHR/7vb4GWwGKLTL43rHQPji3iIn03u5Olf4wW9i4zmn76STeP2SQ6gmfQmgTUt+A==" });
        }
    }
}
