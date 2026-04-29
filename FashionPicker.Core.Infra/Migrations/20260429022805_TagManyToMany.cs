using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionPicker.Core.Infra.Migrations
{
    /// <inheritdoc />
    public partial class TagManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutfitTags_Outfits_OutfitId",
                table: "OutfitTags");

            migrationBuilder.DropIndex(
                name: "IX_OutfitTags_OutfitId",
                table: "OutfitTags");

            migrationBuilder.DropColumn(
                name: "OutfitId",
                table: "OutfitTags");

            migrationBuilder.AlterColumn<List<string>>(
                name: "Images",
                table: "Clothing",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.CreateTable(
                name: "OutfitOutfitTag",
                columns: table => new
                {
                    OutfitsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutfitOutfitTag", x => new { x.OutfitsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_OutfitOutfitTag_OutfitTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "OutfitTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutfitOutfitTag_Outfits_OutfitsId",
                        column: x => x.OutfitsId,
                        principalTable: "Outfits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutfitOutfitTag_TagsId",
                table: "OutfitOutfitTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutfitOutfitTag");

            migrationBuilder.AddColumn<Guid>(
                name: "OutfitId",
                table: "OutfitTags",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<List<string>>(
                name: "Images",
                table: "Clothing",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutfitTags_OutfitId",
                table: "OutfitTags",
                column: "OutfitId");

            migrationBuilder.AddForeignKey(
                name: "FK_OutfitTags_Outfits_OutfitId",
                table: "OutfitTags",
                column: "OutfitId",
                principalTable: "Outfits",
                principalColumn: "Id");
        }
    }
}
