using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Full.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreacionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    apellido = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    numero_documento = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    correo_electronico = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    telefono = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    fecha_nacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clientes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cuentas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cliente_id = table.Column<Guid>(type: "uuid", nullable: false),
                    numero_cuenta = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    tipo_cuenta = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    moneda = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    saldo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    limite_sobregiro = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_actualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cuentas", x => x.id);
                    table.ForeignKey(
                        name: "fk_cuentas_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_clientes_correo_electronico",
                table: "clientes",
                column: "correo_electronico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_clientes_numero_documento",
                table: "clientes",
                column: "numero_documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cuentas_cliente_id",
                table: "cuentas",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "ix_cuentas_numero_cuenta",
                table: "cuentas",
                column: "numero_cuenta",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cuentas");

            migrationBuilder.DropTable(
                name: "clientes");
        }
    }
}
