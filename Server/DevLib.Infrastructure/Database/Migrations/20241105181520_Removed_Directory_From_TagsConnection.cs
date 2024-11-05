using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLib.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Removed_Directory_From_TagsConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagConnections_Directories_DirectoryId",
                table: "TagConnections");

            migrationBuilder.DropIndex(
                name: "IX_TagConnections_DirectoryId",
                table: "TagConnections");

            migrationBuilder.DropColumn(
                name: "DirectoryId",
                table: "TagConnections");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DirectoryId",
                table: "TagConnections",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagConnections_DirectoryId",
                table: "TagConnections",
                column: "DirectoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagConnections_Directories_DirectoryId",
                table: "TagConnections",
                column: "DirectoryId",
                principalTable: "Directories",
                principalColumn: "DirectoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
