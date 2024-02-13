using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutorizacionDePagos.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutorizacionesAprobadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoOperacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<int>(type: "int", nullable: false),
                    TipoAutorizacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorizacionesAprobadas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposAutorizacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposAutorizacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Autorizaciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoOperacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<int>(type: "int", nullable: true),
                    MontoDenegado = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TipoAutorizacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstadoAutorizacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autorizaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Autorizaciones_Estados_EstadoAutorizacionId",
                        column: x => x.EstadoAutorizacionId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Autorizaciones_TiposAutorizacion_TipoAutorizacionId",
                        column: x => x.TipoAutorizacionId,
                        principalTable: "TiposAutorizacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Eliminado", "FechaCreacion", "FechaModificacion", "Nombre", "TipoCliente" },
                values: new object[,]
                {
                    { new Guid("094532e6-be77-4d5a-93c8-3d54131d4337"), false, new DateTime(2024, 2, 13, 6, 26, 11, 625, DateTimeKind.Local).AddTicks(2154), null, "Jose", "PRIMERO" },
                    { new Guid("585ac58c-35fe-4f19-9b0d-99fad357940a"), false, new DateTime(2024, 2, 13, 6, 26, 11, 625, DateTimeKind.Local).AddTicks(2173), null, "Jhon", "SEGUNDO" }
                });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { new Guid("1c65dd15-2466-4e44-8c7f-28ebbc94a7da"), "DENEGADO" },
                    { new Guid("281e3474-a9fa-4a88-a0d2-654b0304b343"), "APROBADO" },
                    { new Guid("6c9c0a81-6c4f-43df-9527-ce2f7e63f6c4"), "NO_CONFIRMADO" },
                    { new Guid("d781bc0f-093d-4e2c-b30c-3fdd0117faa5"), "CONFIRMAR" }
                });

            migrationBuilder.InsertData(
                table: "TiposAutorizacion",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { new Guid("51574221-1158-4ef6-b5fa-4f69d14fadf4"), "COBRO" },
                    { new Guid("c0cb46d6-c7ba-4299-b1ab-1789231df7f6"), "DEVOLUCION" },
                    { new Guid("ee7e9e0f-7759-46ce-88a2-888d406b7251"), "REVERSA" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autorizaciones_EstadoAutorizacionId",
                table: "Autorizaciones",
                column: "EstadoAutorizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Autorizaciones_TipoAutorizacionId",
                table: "Autorizaciones",
                column: "TipoAutorizacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Autorizaciones");

            migrationBuilder.DropTable(
                name: "AutorizacionesAprobadas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "TiposAutorizacion");
        }
    }
}
