using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileRepository.Migrations
{
    /// <inheritdoc />
    public partial class Modifiedfileinformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "RepositoryFileInformations");

            migrationBuilder.DropColumn(
                name: "FileNameWithExtension",
                table: "RepositoryFileInformations");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "RepositoryFileInformations");

            migrationBuilder.DropColumn(
                name: "TotalFiles",
                table: "DataConsistencyCheckEpisodes");

            migrationBuilder.AlterColumn<string>(
                name: "MimeType",
                table: "RepositoryFileInformations",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Extension",
                table: "RepositoryFileInformations",
                type: "character varying(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "LogicalFileName",
                table: "RepositoryFileInformations",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathBig",
                table: "RepositoryFileInformations",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathMedium",
                table: "RepositoryFileInformations",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathOriginal",
                table: "RepositoryFileInformations",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PathSmall",
                table: "RepositoryFileInformations",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhysicalFileName",
                table: "RepositoryFileInformations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogicalFileName",
                table: "RepositoryFileInformations");

            migrationBuilder.DropColumn(
                name: "PathBig",
                table: "RepositoryFileInformations");

            migrationBuilder.DropColumn(
                name: "PathMedium",
                table: "RepositoryFileInformations");

            migrationBuilder.DropColumn(
                name: "PathOriginal",
                table: "RepositoryFileInformations");

            migrationBuilder.DropColumn(
                name: "PathSmall",
                table: "RepositoryFileInformations");

            migrationBuilder.DropColumn(
                name: "PhysicalFileName",
                table: "RepositoryFileInformations");

            migrationBuilder.AlterColumn<string>(
                name: "MimeType",
                table: "RepositoryFileInformations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Extension",
                table: "RepositoryFileInformations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(6)",
                oldMaxLength: 6);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "RepositoryFileInformations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileNameWithExtension",
                table: "RepositoryFileInformations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "RepositoryFileInformations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalFiles",
                table: "DataConsistencyCheckEpisodes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
