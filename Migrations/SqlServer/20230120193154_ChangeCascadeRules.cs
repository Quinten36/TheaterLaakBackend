using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheaterLaakBackend.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class ChangeCascadeRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Programs_ProgramId",
                table: "Shows");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Programs_ProgramId",
                table: "Shows",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Programs_ProgramId",
                table: "Shows");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Programs_ProgramId",
                table: "Shows",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
