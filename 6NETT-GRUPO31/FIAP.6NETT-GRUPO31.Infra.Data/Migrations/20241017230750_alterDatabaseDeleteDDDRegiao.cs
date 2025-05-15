using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAP._6NETT_GRUPO31.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class alterDatabaseDeleteDDDRegiao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TAB_CONTATOS_TAB_DDD_REGIAO_DDDRegiaoId",
                table: "TAB_CONTATOS");

            migrationBuilder.DropTable(
                name: "TAB_DDD_REGIAO");

            migrationBuilder.DropIndex(
                name: "IX_TAB_CONTATOS_DDDRegiaoId",
                table: "TAB_CONTATOS");

            migrationBuilder.DropColumn(
                name: "DDDRegiaoId",
                table: "TAB_CONTATOS");

            migrationBuilder.AddColumn<string>(
                name: "DDD",
                table: "TAB_CONTATOS",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DDD",
                table: "TAB_CONTATOS");

            migrationBuilder.AddColumn<int>(
                name: "DDDRegiaoId",
                table: "TAB_CONTATOS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TAB_DDD_REGIAO",
                columns: table => new
                {
                    IdDDDRegiao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    Ddd = table.Column<string>(type: "varchar(2)", nullable: false),
                    Regiao = table.Column<string>(type: "varchar(100)", nullable: false),
                    UF = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_DDD_REGIAO", x => x.IdDDDRegiao);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TAB_CONTATOS_DDDRegiaoId",
                table: "TAB_CONTATOS",
                column: "DDDRegiaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TAB_CONTATOS_TAB_DDD_REGIAO_DDDRegiaoId",
                table: "TAB_CONTATOS",
                column: "DDDRegiaoId",
                principalTable: "TAB_DDD_REGIAO",
                principalColumn: "IdDDDRegiao",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
