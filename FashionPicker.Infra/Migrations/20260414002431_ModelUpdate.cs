using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionPicker.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagDescription",
                table: "OutfitTags");

            migrationBuilder.DropColumn(
                name: "TagName",
                table: "OutfitTags");

            migrationBuilder.DropColumn(
                name: "OutfitDescription",
                table: "Outfits");

            migrationBuilder.DropColumn(
                name: "OutfitImage",
                table: "Outfits");

            migrationBuilder.DropColumn(
                name: "OutfitName",
                table: "Outfits");

            migrationBuilder.DropColumn(
                name: "OutfitSeason",
                table: "Outfits");

            migrationBuilder.RenameColumn(
                name: "OutfitCreationDate",
                table: "Outfits",
                newName: "CreationDate");

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

            migrationBuilder.AddColumn<List<string>>(
                name: "Colors",
                table: "Outfits",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Outfits",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Outfits",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Outfits",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Season",
                table: "Outfits",
                type: "character varying(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutfitTags_Name",
                table: "OutfitTags",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Colors",
                table: "Outfits");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Outfits");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Outfits");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Outfits");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "Outfits");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Outfits",
                newName: "OutfitCreationDate");

            migrationBuilder.AddColumn<string>(
                name: "TagDescription",
                table: "OutfitTags",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TagName",
                table: "OutfitTags",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OutfitDescription",
                table: "Outfits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OutfitImage",
                table: "Outfits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OutfitName",
                table: "Outfits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OutfitSeason",
                table: "Outfits",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
