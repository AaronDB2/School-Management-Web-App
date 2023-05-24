using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAssignmentData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "AssignmentID", "AssignmentFileName", "CourseId", "Grade", "StudentId" },
                values: new object[] { new Guid("da641ee7-004a-4543-8402-e5e897349ff5"), "TestAssignment.pdf", new Guid("e5376ece-7e42-4604-a3a2-23d69383e8f2"), 0, new Guid("d0a86355-484e-48e0-89e5-68735ce5ec3c") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assignments",
                keyColumn: "AssignmentID",
                keyValue: new Guid("da641ee7-004a-4543-8402-e5e897349ff5"));

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
        }
    }
}
