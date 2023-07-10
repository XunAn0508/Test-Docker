using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyWebsite.Migrations
{
    /// <inheritdoc />
    public partial class Updated_emailtablename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailQueues",
                table: "EmailQueues");

            migrationBuilder.RenameTable(
                name: "EmailQueues",
                newName: "AppEmailQueues");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppEmailQueues",
                table: "AppEmailQueues",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppEmailQueues",
                table: "AppEmailQueues");

            migrationBuilder.RenameTable(
                name: "AppEmailQueues",
                newName: "EmailQueues");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailQueues",
                table: "EmailQueues",
                column: "Id");
        }
    }
}
