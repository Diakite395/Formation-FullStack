using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_TaskFlow_DotNet.Migrations
{
    /// <inheritdoc />
    public partial class Initial01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("3f9bdfca-75b2-488a-bb36-9290b13bc93d"), new DateTime(2025, 11, 5, 15, 41, 32, 132, DateTimeKind.Utc).AddTicks(776), "admin@example.com", "adminpasswordhash", "Admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("3f9bdfca-75b2-488a-bb36-9290b13bc93d"));
        }
    }
}
