using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTwo.Data.Migrations
{
    public partial class AddPrimaryPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrimaryPhoneId",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PrimaryPhoneId",
                table: "Employees",
                column: "PrimaryPhoneId",
                unique: true,
                filter: "[PrimaryPhoneId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Phones_PrimaryPhoneId",
                table: "Employees",
                column: "PrimaryPhoneId",
                principalTable: "Phones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(@"                 
                UPDATE Employees
                SET Employees.PrimaryPhoneId = (SELECT TOP(1) Id FROM Phones WHERE EmployeeId = Employees.ID)
                WHERE EXISTS(SELECT Id FROM Phones WHERE EmployeeId = Employees.ID)
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Phones_PrimaryPhoneId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PrimaryPhoneId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PrimaryPhoneId",
                table: "Employees");
        }
    }
}
