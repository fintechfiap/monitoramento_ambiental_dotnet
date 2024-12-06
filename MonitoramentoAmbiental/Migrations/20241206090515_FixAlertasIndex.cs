using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoramentoAmbiental.Migrations
{
    /// <inheritdoc />
    public partial class FixAlertasIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_ALERTAS_CLIMATICOS",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TIPO = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    DESCRICAO = table.Column<string>(type: "VARCHAR2(1024)", maxLength: 1024, nullable: false),
                    LOCALIDADE = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    CATEGORIA = table.Column<string>(type: "VARCHAR2(127)", maxLength: 127, nullable: false),
                    DATA_INICIO = table.Column<DateTime>(type: "DATE", nullable: false),
                    PREVISAO_TERMINO = table.Column<DateTime>(type: "DATE", nullable: false),
                    CRIADO_EM = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    ALTERADO_EM = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    DELETADO_EM = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ALERTAS_CLIMATICOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_USUARIOS",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    EMAIL = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    SENHA_HASH = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    CRIADO_EM = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    ALTERADO_EM = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    DELETADO_EM = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_USUARIOS", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_USUARIOS_EMAIL",
                table: "TBL_USUARIOS",
                column: "EMAIL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_ALERTAS_CLIMATICOS");

            migrationBuilder.DropTable(
                name: "TBL_USUARIOS");
        }
    }
}
