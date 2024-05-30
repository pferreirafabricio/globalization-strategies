using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalizationApiSql.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalMessages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalMessages_LanguageId",
                table: "TechnicalMessages",
                column: "LanguageId");

            migrationBuilder.InsertData(
                table: "Languages",
                columns: ["Code", "Name"],
                values: ["en", "English"]);

            migrationBuilder.InsertData(
                table: "Languages",
                columns: ["Code", "Name"],
                values: ["pt", "Portuguese"]);

            migrationBuilder.InsertData(
                table: "Languages",
                columns: ["Code", "Name"],
                values: ["pt-BR", "Portuguese (Brazil)"]);

            migrationBuilder.InsertData(
                table: "Languages",
                columns: ["Code", "Name"],
                values: ["pt-PT", "Portuguese (Portugal)"]);

            #region English

            migrationBuilder.InsertData(
                table: "TechnicalMessages",
                columns: ["Code", "LanguageId", "Message"],
                values: ["FAT001", 1, "Government's services are not available at the moment"]);

            migrationBuilder.InsertData(
                table: "TechnicalMessages",
                columns: ["Code", "LanguageId", "Message"],
                values: ["ERR001", 1, "Invalid CNPJ format"]);

            migrationBuilder.InsertData(
                table: "TechnicalMessages",
                columns: ["Code", "LanguageId", "Message"],
                values: ["WARN001", 1, "CNPJ without digital certificate"]);

            migrationBuilder.InsertData(
                table: "TechnicalMessages",
                columns: ["Code", "LanguageId", "Message"],
                values: ["INFO001", 1, "CNPJ without mask"]);

            #endregion

            #region Portuguese

            migrationBuilder.InsertData(
                table: "TechnicalMessages",
                columns: ["Code", "LanguageId", "Message"],
                values: ["FAT001", 2, "Os serviços do governo não estão disponíveis no momento"]);

            migrationBuilder.InsertData(
                table: "TechnicalMessages",
                columns: ["Code", "LanguageId", "Message"],
                values: ["ERR001", 2, "Formato de CNPJ inválido"]);

            migrationBuilder.InsertData(
                table: "TechnicalMessages",
                columns: ["Code", "LanguageId", "Message"],
                values: ["WARN001", 2, "CNPJ sem certificado digital"]);

            #endregion

            #region Portuguese (Brazil)

            migrationBuilder.InsertData(
                table: "TechnicalMessages",
                columns: ["Code", "LanguageId", "Message"],
                values: ["FAT001", 3, "Os serviços do governo brasileiro não estão disponíveis no momento"]);

            migrationBuilder.InsertData(
                table: "TechnicalMessages",
                columns: ["Code", "LanguageId", "Message"],
                values: ["ERR001", 3, "Formato de CNPJ inválido"]);

            #endregion

            #region Portuguese (Portugal)

            migrationBuilder.InsertData(
                table: "TechnicalMessages",
                columns: ["Code", "LanguageId", "Message"],
                values: ["FAT001", 4, "Os serviços do governo português não estão disponíveis no momento"]);

            #endregion
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicalMessages");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
