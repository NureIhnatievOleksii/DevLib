using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLib.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemovedIsArticleFieldFromPostsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArticle",
                table: "Posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArticle",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
