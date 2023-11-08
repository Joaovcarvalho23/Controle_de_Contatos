using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Controle_de_Contatos.Migrations
{
    public partial class CriandoVinculoUsuarioNaContato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "TabelaUsuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TabelaUsuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "TabelaUsuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TabelaUsuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "TabelaContatos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TabelaContatos_UsuarioId",
                table: "TabelaContatos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_TabelaContatos_TabelaUsuarios_UsuarioId",
                table: "TabelaContatos",
                column: "UsuarioId",
                principalTable: "TabelaUsuarios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabelaContatos_TabelaUsuarios_UsuarioId",
                table: "TabelaContatos");

            migrationBuilder.DropIndex(
                name: "IX_TabelaContatos_UsuarioId",
                table: "TabelaContatos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "TabelaContatos");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "TabelaUsuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TabelaUsuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "TabelaUsuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TabelaUsuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
