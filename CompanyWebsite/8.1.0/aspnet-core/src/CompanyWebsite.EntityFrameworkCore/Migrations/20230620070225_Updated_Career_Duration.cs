using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyWebsite.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Career_Duration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Career",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Career");
        }
    }
}
