using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAP._6NETT_GRUPO31.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlteraçãoNomeTabelaDDD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TAB_CONTATOS_DDDRegiao_DDDRegiaoId",
                table: "TAB_CONTATOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DDDRegiao",
                table: "DDDRegiao");

            migrationBuilder.RenameTable(
                name: "DDDRegiao",
                newName: "TAB_DDD_REGIAO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TAB_DDD_REGIAO",
                table: "TAB_DDD_REGIAO",
                column: "IdDDDRegiao");

            migrationBuilder.AddForeignKey(
                name: "FK_TAB_CONTATOS_TAB_DDD_REGIAO_DDDRegiaoId",
                table: "TAB_CONTATOS",
                column: "DDDRegiaoId",
                principalTable: "TAB_DDD_REGIAO",
                principalColumn: "IdDDDRegiao",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TAB_CONTATOS_TAB_DDD_REGIAO_DDDRegiaoId",
                table: "TAB_CONTATOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TAB_DDD_REGIAO",
                table: "TAB_DDD_REGIAO");

            migrationBuilder.RenameTable(
                name: "TAB_DDD_REGIAO",
                newName: "DDDRegiao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DDDRegiao",
                table: "DDDRegiao",
                column: "IdDDDRegiao");

            migrationBuilder.AddForeignKey(
                name: "FK_TAB_CONTATOS_DDDRegiao_DDDRegiaoId",
                table: "TAB_CONTATOS",
                column: "DDDRegiaoId",
                principalTable: "DDDRegiao",
                principalColumn: "IdDDDRegiao",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
