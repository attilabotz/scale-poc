using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroService.Consumer.DataAccess.Migrations
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
                type: "nvarchar(max)",
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
