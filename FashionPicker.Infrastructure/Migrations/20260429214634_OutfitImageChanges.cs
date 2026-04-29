using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionPicker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OutfitImageChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "OutfitImage",
                newName: "SmallSizeUrl");

            migrationBuilder.AddColumn<string>(
                name: "BigSizeUrl",
                table: "OutfitImage",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediumSizeUrl",
                table: "OutfitImage",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MimeType",
                table: "OutfitImage",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OriginalSizeUrl",
                table: "OutfitImage",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BigSizeUrl",
                table: "OutfitImage");

            migrationBuilder.DropColumn(
                name: "MediumSizeUrl",
                table: "OutfitImage");

            migrationBuilder.DropColumn(
                name: "MimeType",
                table: "OutfitImage");

            migrationBuilder.DropColumn(
                name: "OriginalSizeUrl",
                table: "OutfitImage");

            migrationBuilder.RenameColumn(
                name: "SmallSizeUrl",
                table: "OutfitImage",
                newName: "Url");
        }
    }
}
