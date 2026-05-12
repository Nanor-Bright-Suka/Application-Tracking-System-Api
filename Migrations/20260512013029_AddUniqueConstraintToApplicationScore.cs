using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATS.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToApplicationScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationScores_ApplicationId",
                table: "ApplicationScores");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationScores_ApplicationId_Type",
                table: "ApplicationScores",
                columns: new[] { "ApplicationId", "Type" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationScores_ApplicationId_Type",
                table: "ApplicationScores");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationScores_ApplicationId",
                table: "ApplicationScores",
                column: "ApplicationId");
        }
    }
}
