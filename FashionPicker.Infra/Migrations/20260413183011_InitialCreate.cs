using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionPicker.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Outfits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OutfitName = table.Column<string>(type: "text", nullable: false),
                    OutfitDescription = table.Column<string>(type: "text", nullable: false),
                    OutfitImage = table.Column<string>(type: "text", nullable: false),
                    OutfitCreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OutfitSeason = table.Column<string>(type: "text", nullable: false)
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
                    TagName = table.Column<string>(type: "text", nullable: false),
                    TagDescription = table.Column<string>(type: "text", nullable: false),
                    OutfitId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutfitTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutfitTags_Outfits_OutfitId",
                        column: x => x.OutfitId,
                        principalTable: "Outfits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutfitTags_OutfitId",
                table: "OutfitTags",
                column: "OutfitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutfitTags");

            migrationBuilder.DropTable(
                name: "Outfits");
        }
    }
}
