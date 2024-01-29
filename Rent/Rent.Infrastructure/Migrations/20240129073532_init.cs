using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemplateDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Path = table.Column<string>(type: "character varying(72)", maxLength: 72, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateDescriptions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateDescriptions");
        }
    }
}
