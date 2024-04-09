using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEstacionamento.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrecoHora",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataPrecoCadastrado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrecoHora", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VeiculosEntrada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlacaVeiculo = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataSaida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PagamentoEfetuado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculosEntrada", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrecoHora");

            migrationBuilder.DropTable(
                name: "VeiculosEntrada");
        }
    }
}
