using Microsoft.EntityFrameworkCore.Migrations;

namespace TC.Usuarios.Infra.Migrations
{
    public partial class IndiceEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UsuarioEmail",
                table: "Usuario",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_UsuarioEmail",
                table: "Usuario");
        }
    }
}
