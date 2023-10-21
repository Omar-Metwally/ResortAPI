using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class AddingBills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Expenses_ApartmentId_ExpenseId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Apartments_ApartmentId",
                table: "Expenses");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Expenses_ApartmentId_Id",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Bills_ApartmentId_ExpenseId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Expenses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ApartmentId",
                table: "Bills",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ExpenseId",
                table: "Bills",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Apartments_ApartmentId",
                table: "Bills",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Expenses_ExpenseId",
                table: "Bills",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Apartments_ApartmentId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Expenses_ExpenseId",
                table: "Bills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Bills_ApartmentId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_ExpenseId",
                table: "Bills");

            migrationBuilder.AddColumn<Guid>(
                name: "ApartmentId",
                table: "Expenses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Expenses_ApartmentId_Id",
                table: "Expenses",
                columns: new[] { "ApartmentId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses",
                columns: new[] { "Id", "ApartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ApartmentId_ExpenseId",
                table: "Bills",
                columns: new[] { "ApartmentId", "ExpenseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Expenses_ApartmentId_ExpenseId",
                table: "Bills",
                columns: new[] { "ApartmentId", "ExpenseId" },
                principalTable: "Expenses",
                principalColumns: new[] { "ApartmentId", "Id" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Apartments_ApartmentId",
                table: "Expenses",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
