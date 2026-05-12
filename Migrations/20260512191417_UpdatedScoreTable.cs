using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATS.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedScoreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ApplicationScores",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ApplicationScores",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ApplicationScores");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ApplicationScores");
        }
    }
}
