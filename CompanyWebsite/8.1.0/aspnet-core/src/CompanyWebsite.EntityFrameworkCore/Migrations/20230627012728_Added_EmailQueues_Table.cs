using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyWebsite.Migrations
{
    /// <inheritdoc />
    public partial class Added_EmailQueues_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailQueues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultFromDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultFromAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientEmailAddresses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmtpHost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmtpUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmtpPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmtpPort = table.Column<int>(type: "int", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentPaths = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailQueues", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailQueues");
        }
    }
}
