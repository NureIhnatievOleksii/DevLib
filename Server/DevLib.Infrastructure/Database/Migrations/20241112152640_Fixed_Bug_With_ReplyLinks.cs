using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLib.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_Bug_With_ReplyLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Books_BookId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Posts_PostId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_ReplyLink_ReplyId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReplyLink_Comment_CommentId",
                table: "ReplyLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReplyLink",
                table: "ReplyLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "ReplyLink",
                newName: "ReplyLinks");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_ReplyLink_CommentId",
                table: "ReplyLinks",
                newName: "IX_ReplyLinks_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ReplyId",
                table: "Comments",
                newName: "IX_Comments_ReplyId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_PostId",
                table: "Comments",
                newName: "IX_Comments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_BookId",
                table: "Comments",
                newName: "IX_Comments_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReplyLinks",
                table: "ReplyLinks",
                column: "ReplyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Books_BookId",
                table: "Comments",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ReplyLinks_ReplyId",
                table: "Comments",
                column: "ReplyId",
                principalTable: "ReplyLinks",
                principalColumn: "ReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReplyLinks_Comments_CommentId",
                table: "ReplyLinks",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Books_BookId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ReplyLinks_ReplyId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReplyLinks_Comments_CommentId",
                table: "ReplyLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReplyLinks",
                table: "ReplyLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "ReplyLinks",
                newName: "ReplyLink");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_ReplyLinks_CommentId",
                table: "ReplyLink",
                newName: "IX_ReplyLink_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ReplyId",
                table: "Comment",
                newName: "IX_Comment_ReplyId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PostId",
                table: "Comment",
                newName: "IX_Comment_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_BookId",
                table: "Comment",
                newName: "IX_Comment_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReplyLink",
                table: "ReplyLink",
                column: "ReplyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Books_BookId",
                table: "Comment",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Posts_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_ReplyLink_ReplyId",
                table: "Comment",
                column: "ReplyId",
                principalTable: "ReplyLink",
                principalColumn: "ReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReplyLink_Comment_CommentId",
                table: "ReplyLink",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
