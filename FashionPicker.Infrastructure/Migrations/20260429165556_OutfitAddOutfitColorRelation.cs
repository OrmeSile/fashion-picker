using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionPicker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OutfitAddOutfitColorRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutfitColor_Outfits_OutfitId",
                table: "OutfitColor");

            migrationBuilder.DropIndex(
                name: "IX_OutfitColor_OutfitId",
                table: "OutfitColor");

            migrationBuilder.DropColumn(
                name: "OutfitId",
                table: "OutfitColor");

            migrationBuilder.CreateTable(
                name: "OutfitOutfitColor",
                columns: table => new
                {
                    ColorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    OutfitsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutfitOutfitColor", x => new { x.ColorsId, x.OutfitsId });
                    table.ForeignKey(
                        name: "FK_OutfitOutfitColor_OutfitColor_ColorsId",
                        column: x => x.ColorsId,
                        principalTable: "OutfitColor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutfitOutfitColor_Outfits_OutfitsId",
                        column: x => x.OutfitsId,
                        principalTable: "Outfits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutfitOutfitColor_OutfitsId",
                table: "OutfitOutfitColor",
                column: "OutfitsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutfitOutfitColor");

            migrationBuilder.AddColumn<Guid>(
                name: "OutfitId",
                table: "OutfitColor",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutfitColor_OutfitId",
                table: "OutfitColor",
                column: "OutfitId");

            migrationBuilder.AddForeignKey(
                name: "FK_OutfitColor_Outfits_OutfitId",
                table: "OutfitColor",
                column: "OutfitId",
                principalTable: "Outfits",
                principalColumn: "Id");
        }
    }
}
