using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iSpend.Infra.Data.Migrations
{
    public partial class RefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 6, 26, 17, 0, 56, 780, DateTimeKind.Local).AddTicks(441), new DateTime(2022, 6, 26, 17, 0, 56, 780, DateTimeKind.Local).AddTicks(431) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 6, 26, 17, 0, 56, 780, DateTimeKind.Local).AddTicks(514), new DateTime(2022, 6, 26, 17, 0, 56, 780, DateTimeKind.Local).AddTicks(513) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 6, 26, 17, 0, 56, 780, DateTimeKind.Local).AddTicks(616), new DateTime(2022, 6, 26, 17, 0, 56, 780, DateTimeKind.Local).AddTicks(615) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 6, 26, 17, 0, 56, 780, DateTimeKind.Local).AddTicks(663), new DateTime(2022, 6, 26, 17, 0, 56, 780, DateTimeKind.Local).AddTicks(663) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 6, 20, 2, 10, 50, 916, DateTimeKind.Local).AddTicks(9719), new DateTime(2022, 6, 20, 2, 10, 50, 916, DateTimeKind.Local).AddTicks(9706) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 6, 20, 2, 10, 50, 916, DateTimeKind.Local).AddTicks(9798), new DateTime(2022, 6, 20, 2, 10, 50, 916, DateTimeKind.Local).AddTicks(9798) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 6, 20, 2, 10, 50, 916, DateTimeKind.Local).AddTicks(9881), new DateTime(2022, 6, 20, 2, 10, 50, 916, DateTimeKind.Local).AddTicks(9881) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 6, 20, 2, 10, 50, 916, DateTimeKind.Local).AddTicks(9931), new DateTime(2022, 6, 20, 2, 10, 50, 916, DateTimeKind.Local).AddTicks(9930) });
        }
    }
}
