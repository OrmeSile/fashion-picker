using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionPicker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OutfitImageAddUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "ClothingImage",
                newName: "MimeType");

            migrationBuilder.AddColumn<string>(
                name: "BigSizeUrl",
                table: "ClothingImage",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediumSizeUrl",
                table: "ClothingImage",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalSizeUrl",
                table: "ClothingImage",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SmallSizeUrl",
                table: "ClothingImage",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ClothingOutfit",
                columns: table => new
                {
                    ClothingId = table.Column<Guid>(type: "uuid", nullable: false),
                    OutfitsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingOutfit", x => new { x.ClothingId, x.OutfitsId });
                    table.ForeignKey(
                        name: "FK_ClothingOutfit_Clothing_ClothingId",
                        column: x => x.ClothingId,
                        principalTable: "Clothing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothingOutfit_Outfits_OutfitsId",
                        column: x => x.OutfitsId,
                        principalTable: "Outfits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothingOutfit_OutfitsId",
                table: "ClothingOutfit",
                column: "OutfitsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothingOutfit");

            migrationBuilder.DropColumn(
                name: "BigSizeUrl",
                table: "ClothingImage");

            migrationBuilder.DropColumn(
                name: "MediumSizeUrl",
                table: "ClothingImage");

            migrationBuilder.DropColumn(
                name: "OriginalSizeUrl",
                table: "ClothingImage");

            migrationBuilder.DropColumn(
                name: "SmallSizeUrl",
                table: "ClothingImage");

            migrationBuilder.RenameColumn(
                name: "MimeType",
                table: "ClothingImage",
                newName: "Url");
        }
    }
}
