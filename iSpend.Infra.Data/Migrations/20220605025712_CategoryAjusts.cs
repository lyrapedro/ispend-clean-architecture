using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iSpend.Infra.Data.Migrations
{
    public partial class CategoryAjusts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Categories",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "ModifiedAt", "Name", "RegisteredAt", "UserId" },
                values: new object[,]
                {
                    { 1, "#c0eb34", new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1135), "Lazer", new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1126), null },
                    { 2, "#eb9334", new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1237), "Vestuário", new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1237), null },
                    { 3, "#ebdc34", new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1293), "Mercado", new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1292), null },
                    { 4, "#349ceb", new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1377), "Saúde", new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1376), null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(7)",
                oldMaxLength: 7);
        }
    }
}
