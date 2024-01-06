using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coddinggurrus.Api.Migrations
{
    /// <inheritdoc />
    public partial class DateRegistrationadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateRegistration",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRegistration",
                table: "AspNetUsers");
        }
    }
}
