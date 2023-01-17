using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheaterLaakBackend.Migrations
{
    /// <inheritdoc />
    public partial class removedidsodotnetidisusedwhichisstringsolotofchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountID",
                table: "Verificaties",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountID",
                table: "Verificaties",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
