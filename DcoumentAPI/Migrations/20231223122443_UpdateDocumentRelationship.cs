using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DcoumentAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDocumentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UploadedBy",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UploadedBy",
                table: "Documents",
                column: "UploadedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AspNetUsers_UploadedBy",
                table: "Documents",
                column: "UploadedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AspNetUsers_UploadedBy",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_UploadedBy",
                table: "Documents");

            migrationBuilder.AlterColumn<string>(
                name: "UploadedBy",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
