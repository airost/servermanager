using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManager.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ReservationModel_HotelID",
                table: "ReservationModel",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationModel_UserID",
                table: "ReservationModel",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationModel_AspNetUsers_UserID",
                table: "ReservationModel",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationModel_HotelModel_HotelID",
                table: "ReservationModel",
                column: "HotelID",
                principalTable: "HotelModel",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationModel_AspNetUsers_UserID",
                table: "ReservationModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationModel_HotelModel_HotelID",
                table: "ReservationModel");

            migrationBuilder.DropIndex(
                name: "IX_ReservationModel_HotelID",
                table: "ReservationModel");

            migrationBuilder.DropIndex(
                name: "IX_ReservationModel_UserID",
                table: "ReservationModel");
        }
    }
}
