using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iSpend.Infra.Data.Migrations
{
    public partial class ExpensesAndSubscriptionsPaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentAt",
                table: "Subscriptions");

            migrationBuilder.AddColumn<int>(
                name: "BillingDay",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExpensesPaid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesPaid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpensesPaid_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionsPaid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionsPaid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionsPaid_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesPaid_ExpenseId",
                table: "ExpensesPaid",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionsPaid_SubscriptionId",
                table: "SubscriptionsPaid",
                column: "SubscriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpensesPaid");

            migrationBuilder.DropTable(
                name: "SubscriptionsPaid");

            migrationBuilder.DropColumn(
                name: "BillingDay",
                table: "Subscriptions");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentAt",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1135), new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1126) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1237), new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1237) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1293), new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1292) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1377), new DateTime(2022, 6, 4, 23, 57, 12, 551, DateTimeKind.Local).AddTicks(1376) });
        }
    }
}
