using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManager.Migrations
{
    /// <inheritdoc />
    public partial class updatehotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomID",
                table: "ReservationModel",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationModel_RoomID",
                table: "ReservationModel",
                column: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationModel_RoomModel_RoomID",
                table: "ReservationModel",
                column: "RoomID",
                principalTable: "RoomModel",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationModel_RoomModel_RoomID",
                table: "ReservationModel");

            migrationBuilder.DropIndex(
                name: "IX_ReservationModel_RoomID",
                table: "ReservationModel");

            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "ReservationModel");
        }
    }
}
