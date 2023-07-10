using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyWebsite.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Applicant_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Resume",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Transcript",
                table: "Applicant");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Applicant",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resume",
                table: "Applicant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Applicant",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Transcript",
                table: "Applicant",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
