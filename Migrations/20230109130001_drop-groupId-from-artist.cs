using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheaterLaakBackend.Migrations
{
    /// <inheritdoc />
    public partial class dropgroupIdfromartist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Groups_GroupId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_GroupId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Artists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Artists",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artists_GroupId",
                table: "Artists",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Groups_GroupId",
                table: "Artists",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
