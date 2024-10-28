using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLib.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Added_Column_PublicationDateTime_For_Books_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationDateTime",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicationDateTime",
                table: "Books");
        }
    }
}
