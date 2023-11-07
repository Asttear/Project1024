using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1024.Server.Migrations
{
    /// <inheritdoc />
    public partial class Add_Likes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "likes",
                table: "video",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "likes",
                table: "video");
        }
    }
}
