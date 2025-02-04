using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Translations.Core.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Translations");

            migrationBuilder.CreateTable(
                name: "LocalizedTexts",
                schema: "Translations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TranslationKeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedTexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TranslationKeys",
                schema: "Translations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationKeys", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedTexts_LanguageId_TranslationKeyId",
                schema: "Translations",
                table: "LocalizedTexts",
                columns: new[] { "LanguageId", "TranslationKeyId" });

            migrationBuilder.CreateIndex(
                name: "IX_TranslationKeys_ProjectId",
                schema: "Translations",
                table: "TranslationKeys",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalizedTexts",
                schema: "Translations");

            migrationBuilder.DropTable(
                name: "TranslationKeys",
                schema: "Translations");
        }
    }
}
