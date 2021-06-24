using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.LuizaAuth.Migrations
{
    public partial class UpdateNameColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Users",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Senha");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Nome");
        }
    }
}
