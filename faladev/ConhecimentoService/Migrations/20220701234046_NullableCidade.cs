using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConhecimentoService.Migrations
{
    public partial class NullableCidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CidadeDaPublicacao",
                table: "PublicacoesRefinadas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PublicacoesRefinadas",
                keyColumn: "CidadeDaPublicacao",
                keyValue: null,
                column: "CidadeDaPublicacao",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CidadeDaPublicacao",
                table: "PublicacoesRefinadas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
