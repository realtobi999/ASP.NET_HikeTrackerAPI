using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HikingTracks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Account_Table_UniqueEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Accounts_email",
                table: "Accounts",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_email",
                table: "Accounts");
        }
    }
}
