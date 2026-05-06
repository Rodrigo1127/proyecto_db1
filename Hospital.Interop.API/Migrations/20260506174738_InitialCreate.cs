using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hospital.Interop.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacienteId = table.Column<int>(type: "integer", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Hora = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Departamento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    DepartamentoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Ubicacion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.DepartamentoId);
                });

            migrationBuilder.CreateTable(
                name: "Examenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Resultado = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examenes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Dosis = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    PacienteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Documento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Direccion = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Genero = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.PacienteId);
                });

            migrationBuilder.CreateTable(
                name: "Tecnicos",
                columns: table => new
                {
                    TecnicoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Documento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DepartamentoId = table.Column<int>(type: "integer", nullable: false),
                    Cargo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaContratacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnicos", x => x.TecnicoId);
                    table.ForeignKey(
                        name: "FK_Tecnicos_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiposPrueba",
                columns: table => new
                {
                    TipoPruebaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DepartamentoId = table.Column<int>(type: "integer", nullable: false),
                    CostoReferencia = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPrueba", x => x.TipoPruebaId);
                    table.ForeignKey(
                        name: "FK_TiposPrueba_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    FacturaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacienteId = table.Column<int>(type: "integer", nullable: false),
                    Monto = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    FechaFactura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Concepto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.FacturaId);
                    table.ForeignKey(
                        name: "FK_Facturas_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesPrueba",
                columns: table => new
                {
                    SolicitudPruebaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacienteId = table.Column<int>(type: "integer", nullable: false),
                    TipoPruebaId = table.Column<int>(type: "integer", nullable: false),
                    TecnicoId = table.Column<int>(type: "integer", nullable: true),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaRealizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CostoFinal = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesPrueba", x => x.SolicitudPruebaId);
                    table.ForeignKey(
                        name: "FK_SolicitudesPrueba_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesPrueba_Tecnicos_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "Tecnicos",
                        principalColumn: "TecnicoId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SolicitudesPrueba_TiposPrueba_TipoPruebaId",
                        column: x => x.TipoPruebaId,
                        principalTable: "TiposPrueba",
                        principalColumn: "TipoPruebaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResultadosPrueba",
                columns: table => new
                {
                    ResultadoPruebaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SolicitudPruebaId = table.Column<int>(type: "integer", nullable: false),
                    ValorResultado = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ValorMinimo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ValorMaximo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UnidadMedida = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FechaResultado = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AnalystaNombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadosPrueba", x => x.ResultadoPruebaId);
                    table.ForeignKey(
                        name: "FK_ResultadosPrueba_SolicitudesPrueba_SolicitudPruebaId",
                        column: x => x.SolicitudPruebaId,
                        principalTable: "SolicitudesPrueba",
                        principalColumn: "SolicitudPruebaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_PacienteId",
                table: "Facturas",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosPrueba_SolicitudPruebaId",
                table: "ResultadosPrueba",
                column: "SolicitudPruebaId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesPrueba_PacienteId",
                table: "SolicitudesPrueba",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesPrueba_TecnicoId",
                table: "SolicitudesPrueba",
                column: "TecnicoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesPrueba_TipoPruebaId",
                table: "SolicitudesPrueba",
                column: "TipoPruebaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_DepartamentoId",
                table: "Tecnicos",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposPrueba_DepartamentoId",
                table: "TiposPrueba",
                column: "DepartamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Examenes");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Medicamentos");

            migrationBuilder.DropTable(
                name: "ResultadosPrueba");

            migrationBuilder.DropTable(
                name: "SolicitudesPrueba");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Tecnicos");

            migrationBuilder.DropTable(
                name: "TiposPrueba");

            migrationBuilder.DropTable(
                name: "Departamentos");
        }
    }
}
