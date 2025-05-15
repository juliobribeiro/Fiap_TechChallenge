using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAP._6NETT_GRUPO31.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class alterColumnDDDToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DDD",
                table: "TAB_CONTATOS",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DDD",
                table: "TAB_CONTATOS",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
