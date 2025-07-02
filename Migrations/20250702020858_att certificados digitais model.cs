using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CertiNet.Migrations
{
    public partial class attcertificadosdigitaismodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgendamentoId",
                table: "CertificadoDigital",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoDigital_AgendamentoId",
                table: "CertificadoDigital",
                column: "AgendamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoDigital_Agendamento_AgendamentoId",
                table: "CertificadoDigital",
                column: "AgendamentoId",
                principalTable: "Agendamento",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoDigital_Agendamento_AgendamentoId",
                table: "CertificadoDigital");

            migrationBuilder.DropIndex(
                name: "IX_CertificadoDigital_AgendamentoId",
                table: "CertificadoDigital");

            migrationBuilder.DropColumn(
                name: "AgendamentoId",
                table: "CertificadoDigital");
        }
    }
}
