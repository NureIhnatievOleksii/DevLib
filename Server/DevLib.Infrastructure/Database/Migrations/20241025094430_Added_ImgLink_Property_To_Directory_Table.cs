using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLib.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Added_ImgLink_Property_To_Directory_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgLink",
                table: "Directories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgLink",
                table: "Directories");
        }
    }
}
