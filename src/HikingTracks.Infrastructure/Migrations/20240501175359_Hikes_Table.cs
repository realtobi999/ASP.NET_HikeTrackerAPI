using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HikingTracks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Hikes_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hikes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    distance = table.Column<double>(type: "double precision", nullable: false),
                    elevation_gain = table.Column<double>(type: "double precision", nullable: false),
                    elevation_loss = table.Column<double>(type: "double precision", nullable: false),
                    average_speed = table.Column<double>(type: "double precision", nullable: false),
                    max_speed = table.Column<double>(type: "double precision", nullable: false),
                    moving_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    account_id1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hikes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Hikes_Accounts_account_id1",
                        column: x => x.account_id1,
                        principalTable: "Accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hikes_account_id1",
                table: "Hikes",
                column: "account_id1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hikes");
        }
    }
}
