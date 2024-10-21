using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLib.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Created_TagsConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TagConnections",
                columns: table => new
                {
                    TagConnectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DirectoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagConnections", x => x.TagConnectionId);
                    table.ForeignKey(
                        name: "FK_TagConnections_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagConnections_Directories_DirectoryId",
                        column: x => x.DirectoryId,
                        principalTable: "Directories",
                        principalColumn: "DirectoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagConnections_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagConnections_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagConnections_BookId",
                table: "TagConnections",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_TagConnections_DirectoryId",
                table: "TagConnections",
                column: "DirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TagConnections_PostId",
                table: "TagConnections",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_TagConnections_TagId",
                table: "TagConnections",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagConnections");
        }
    }
}
