using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1024.Server.Migrations
{
    /// <inheritdoc />
    public partial class Delete_Plays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "plays",
                table: "video");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "plays",
                table: "video",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
