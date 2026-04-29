using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionPicker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OutfitOutfitImageRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutfitImage_Outfits_OutfitId",
                table: "OutfitImage");

            migrationBuilder.DropIndex(
                name: "IX_OutfitImage_OutfitId",
                table: "OutfitImage");

            migrationBuilder.DropColumn(
                name: "OutfitId",
                table: "OutfitImage");

            migrationBuilder.AddForeignKey(
                name: "FK_OutfitImage_Outfits_Id",
                table: "OutfitImage",
                column: "Id",
                principalTable: "Outfits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutfitImage_Outfits_Id",
                table: "OutfitImage");

            migrationBuilder.AddColumn<Guid>(
                name: "OutfitId",
                table: "OutfitImage",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OutfitImage_OutfitId",
                table: "OutfitImage",
                column: "OutfitId");

            migrationBuilder.AddForeignKey(
                name: "FK_OutfitImage_Outfits_OutfitId",
                table: "OutfitImage",
                column: "OutfitId",
                principalTable: "Outfits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
