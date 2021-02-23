using Microsoft.EntityFrameworkCore.Migrations;

namespace Hahn.ApplicatonProcess.December2020.Data.Migrations
{
    public partial class Initialagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmailAdress",
                table: "Applicant",
                type: "nvarchar(75)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CountryOfOrigin",
                table: "Applicant",
                type: "nvarchar(75)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmailAdress",
                table: "Applicant",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)");

            migrationBuilder.AlterColumn<string>(
                name: "CountryOfOrigin",
                table: "Applicant",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)");
        }
    }
}
