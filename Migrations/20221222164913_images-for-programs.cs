using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheaterLaakBackend.Migrations
{
    /// <inheritdoc />
    public partial class imagesforprograms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Halls_Hallid",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "Hallid",
                table: "Reservations",
                newName: "HallId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_Hallid",
                table: "Reservations",
                newName: "IX_Reservations_HallId");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Programs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Halls_HallId",
                table: "Reservations",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Halls_HallId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Programs");

            migrationBuilder.RenameColumn(
                name: "HallId",
                table: "Reservations",
                newName: "Hallid");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_HallId",
                table: "Reservations",
                newName: "IX_Reservations_Hallid");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Halls_Hallid",
                table: "Reservations",
                column: "Hallid",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
