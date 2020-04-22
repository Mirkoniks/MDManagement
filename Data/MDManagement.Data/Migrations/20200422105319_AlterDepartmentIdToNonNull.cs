using Microsoft.EntityFrameworkCore.Migrations;

namespace MDManagement.Web.Data.Migrations
{
    public partial class AlterDepartmentIdToNonNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                table: "AspNetUsers",
                type: "decimal (18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal (18,4)");

            migrationBuilder.AlterColumn<int>(
                name: "JobTitleId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                table: "AspNetUsers",
                type: "decimal (18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal (18,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JobTitleId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
