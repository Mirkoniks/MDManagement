using Microsoft.EntityFrameworkCore.Migrations;

namespace MDManagement.Web.Data.Migrations
{
    public partial class AddedAddressToEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Addresses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_EmployeeId",
                table: "Addresses",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_EmployeeId",
                table: "Addresses",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_EmployeeId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_EmployeeId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Addresses");
        }
    }
}
