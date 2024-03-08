using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapaDatos.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nombres = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    Apellidos = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    Direccion = table.Column<string>(type: "VARCHAR2(100)", nullable: true),
                    Telefono = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    NroMesa = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nombre = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    Reservada = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Puestos = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesas", x => x.NroMesa);
                });

            migrationBuilder.CreateTable(
                name: "Meseros",
                columns: table => new
                {
                    IdMesero = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nombres = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    Apellidos = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    Edad = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Antiguedad = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meseros", x => x.IdMesero);
                });

            migrationBuilder.CreateTable(
                name: "Supervisores",
                columns: table => new
                {
                    IdSupervisor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nombres = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    Apellidos = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    Edad = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Antiguedad = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisores", x => x.IdSupervisor);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    NroFactura = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdCliente = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NroMesa = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdMesero = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.NroFactura);
                    table.ForeignKey(
                        name: "FK_Facturas_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facturas_Mesas_NroMesa",
                        column: x => x.NroMesa,
                        principalTable: "Mesas",
                        principalColumn: "NroMesa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facturas_Meseros_IdMesero",
                        column: x => x.IdMesero,
                        principalTable: "Meseros",
                        principalColumn: "IdMesero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleXFacturas",
                columns: table => new
                {
                    IdDetalleXFactura = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NroFactura = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdSupervisor = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Plato = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Valor = table.Column<double>(type: "BINARY_DOUBLE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleXFacturas", x => x.IdDetalleXFactura);
                    table.ForeignKey(
                        name: "FK_DetalleXFacturas_Facturas_NroFactura",
                        column: x => x.NroFactura,
                        principalTable: "Facturas",
                        principalColumn: "NroFactura",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleXFacturas_Supervisores_IdSupervisor",
                        column: x => x.IdSupervisor,
                        principalTable: "Supervisores",
                        principalColumn: "IdSupervisor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleXFacturas_IdSupervisor",
                table: "DetalleXFacturas",
                column: "IdSupervisor");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleXFacturas_NroFactura",
                table: "DetalleXFacturas",
                column: "NroFactura");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_IdCliente",
                table: "Facturas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_IdMesero",
                table: "Facturas",
                column: "IdMesero");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_NroMesa",
                table: "Facturas",
                column: "NroMesa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleXFacturas");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Supervisores");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Mesas");

            migrationBuilder.DropTable(
                name: "Meseros");
        }
    }
}
