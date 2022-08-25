using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iSpend.Infra.Data.Migrations
{
    public partial class modelNodesForRecurrentsTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpensesPaid");

            migrationBuilder.DropTable(
                name: "SubscriptionsPaid");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Subscriptions");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRecurrences",
                table: "Incomes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Incomes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Incomes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRecurrences",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Expenses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Expense_Node",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseId = table.Column<int>(type: "int", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense_Node", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expense_Node_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Income_Node",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncomeId = table.Column<int>(type: "int", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Income_Node", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Income_Node_Incomes_IncomeId",
                        column: x => x.IncomeId,
                        principalTable: "Incomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscription_Node",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription_Node", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_Node_Subscriptions_SubscriptionId",
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
                values: new object[] { new DateTime(2022, 8, 25, 16, 24, 16, 923, DateTimeKind.Local).AddTicks(7959), new DateTime(2022, 8, 25, 16, 24, 16, 923, DateTimeKind.Local).AddTicks(7949) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 8, 25, 16, 24, 16, 923, DateTimeKind.Local).AddTicks(8024), new DateTime(2022, 8, 25, 16, 24, 16, 923, DateTimeKind.Local).AddTicks(8023) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 8, 25, 16, 24, 16, 923, DateTimeKind.Local).AddTicks(8100), new DateTime(2022, 8, 25, 16, 24, 16, 923, DateTimeKind.Local).AddTicks(8099) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ModifiedAt", "RegisteredAt" },
                values: new object[] { new DateTime(2022, 8, 25, 16, 24, 16, 923, DateTimeKind.Local).AddTicks(8143), new DateTime(2022, 8, 25, 16, 24, 16, 923, DateTimeKind.Local).AddTicks(8142) });

            migrationBuilder.CreateIndex(
                name: "IX_Expense_Node_ExpenseId",
                table: "Expense_Node",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Income_Node_IncomeId",
                table: "Income_Node",
                column: "IncomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_Node_SubscriptionId",
                table: "Subscription_Node",
                column: "SubscriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expense_Node");

            migrationBuilder.DropTable(
                name: "Income_Node");

            migrationBuilder.DropTable(
                name: "Subscription_Node");

            migrationBuilder.DropColumn(
                name: "NumberOfRecurrences",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "NumberOfRecurrences",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Expenses");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesPaid_ExpenseId",
                table: "ExpensesPaid",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionsPaid_SubscriptionId",
                table: "SubscriptionsPaid",
                column: "SubscriptionId");
        }
    }
}
