using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroService.Producer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NoteId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NoteId",
                table: "Note",
                type: "char(26)",
                maxLength: 26,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "Note");
        }
    }
}
