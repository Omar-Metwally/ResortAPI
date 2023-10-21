using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class addingPKToBill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bills",
                table: "Bills");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bills",
                table: "Bills",
                columns: new[] { "Id", "ApartmentId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bills",
                table: "Bills");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bills",
                table: "Bills",
                columns: new[] { "Id", "ExpenseId" });
        }
    }
}
