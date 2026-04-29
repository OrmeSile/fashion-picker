using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionPicker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Reset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clothing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clothing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Outfits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Mood = table.Column<string>(type: "text", nullable: false),
                    Sport = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outfits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutfitTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutfitTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClothingImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    ClothingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothingImage_Clothing_ClothingId",
                        column: x => x.ClothingId,
                        principalTable: "Clothing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutfitColor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    OutfitId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutfitColor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutfitColor_Outfits_OutfitId",
                        column: x => x.OutfitId,
                        principalTable: "Outfits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OutfitImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OutfitId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutfitImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutfitImage_Outfits_OutfitId",
                        column: x => x.OutfitId,
                        principalTable: "Outfits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "OutfitSeason",
                columns: table => new
                {
                    OutfitsId = table.Column<Guid>(type: "uuid", nullable: false),
                    SeasonsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutfitSeason", x => new { x.OutfitsId, x.SeasonsId });
                    table.ForeignKey(
                        name: "FK_OutfitSeason_Outfits_OutfitsId",
                        column: x => x.OutfitsId,
                        principalTable: "Outfits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutfitSeason_Season_SeasonsId",
                        column: x => x.SeasonsId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothingImage_ClothingId",
                table: "ClothingImage",
                column: "ClothingId");

            migrationBuilder.CreateIndex(
                name: "IX_OutfitColor_OutfitId",
                table: "OutfitColor",
                column: "OutfitId");

            migrationBuilder.CreateIndex(
                name: "IX_OutfitImage_OutfitId",
                table: "OutfitImage",
                column: "OutfitId");

            migrationBuilder.CreateIndex(
                name: "IX_OutfitOutfitTag_TagsId",
                table: "OutfitOutfitTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_OutfitSeason_SeasonsId",
                table: "OutfitSeason",
                column: "SeasonsId");

            migrationBuilder.CreateIndex(
                name: "IX_OutfitTags_Value",
                table: "OutfitTags",
                column: "Value",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothingImage");

            migrationBuilder.DropTable(
                name: "OutfitColor");

            migrationBuilder.DropTable(
                name: "OutfitImage");

            migrationBuilder.DropTable(
                name: "OutfitOutfitTag");

            migrationBuilder.DropTable(
                name: "OutfitSeason");

            migrationBuilder.DropTable(
                name: "Clothing");

            migrationBuilder.DropTable(
                name: "OutfitTags");

            migrationBuilder.DropTable(
                name: "Outfits");

            migrationBuilder.DropTable(
                name: "Season");
        }
    }
}
