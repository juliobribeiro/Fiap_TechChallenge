using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAP._6NETT_GRUPO31.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DDDRegiao",
                columns: table => new
                {
                    IdDDDRegiao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ddd = table.Column<string>(type: "varchar(2)", nullable: false),
                    Regiao = table.Column<string>(type: "varchar(100)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    UF = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DDDRegiao", x => x.IdDDDRegiao);
                });

            migrationBuilder.CreateTable(
                name: "TAB_CONTATOS",
                columns: table => new
                {
                    IdContato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(500)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(100)", nullable: false),
                    DDDRegiaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_CONTATOS", x => x.IdContato);
                    table.ForeignKey(
                        name: "FK_TAB_CONTATOS_DDDRegiao_DDDRegiaoId",
                        column: x => x.DDDRegiaoId,
                        principalTable: "DDDRegiao",
                        principalColumn: "IdDDDRegiao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TAB_CONTATOS_DDDRegiaoId",
                table: "TAB_CONTATOS",
                column: "DDDRegiaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TAB_CONTATOS_Email",
                table: "TAB_CONTATOS",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TAB_CONTATOS");

            migrationBuilder.DropTable(
                name: "DDDRegiao");
        }
    }
}
