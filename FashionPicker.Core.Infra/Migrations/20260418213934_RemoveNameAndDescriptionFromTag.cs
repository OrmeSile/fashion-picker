using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionPicker.Core.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNameAndDescriptionFromTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutfitTags_Name",
                table: "OutfitTags");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "OutfitTags");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OutfitTags");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "OutfitTags",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OutfitTags_Value",
                table: "OutfitTags",
                column: "Value",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutfitTags_Value",
                table: "OutfitTags");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "OutfitTags");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OutfitTags",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OutfitTags",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OutfitTags_Name",
                table: "OutfitTags",
                column: "Name",
                unique: true);
        }
    }
}
