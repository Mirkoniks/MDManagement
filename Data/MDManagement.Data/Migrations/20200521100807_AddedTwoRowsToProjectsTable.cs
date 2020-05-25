using Microsoft.EntityFrameworkCore.Migrations;

namespace MDManagement.Web.Data.Migrations
{
    public partial class AddedTwoRowsToProjectsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleated",
                table: "Projects",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleated",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectCode",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
