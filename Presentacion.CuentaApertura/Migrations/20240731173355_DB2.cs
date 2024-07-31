using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presentacion.CuentaApertura.Migrations
{
    /// <inheritdoc />
    public partial class DB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatosDactilares",
                columns: table => new
                {
                    Identificacion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Codigo_Dactilar = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosDactilares", x => x.Identificacion);
                });

            migrationBuilder.CreateTable(
                name: "DireccionMapa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Canton = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parroquia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DireccionMapa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InformacionPersonal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ingresos = table.Column<double>(type: "float", nullable: false),
                    Gastos = table.Column<double>(type: "float", nullable: false),
                    PaisNacimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CiudadNacimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NivelDeInstruccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dependiente = table.Column<bool>(type: "bit", nullable: false),
                    NegocioPropio = table.Column<bool>(type: "bit", nullable: false),
                    Ninguno = table.Column<bool>(type: "bit", nullable: false),
                    VivoConFamiliares = table.Column<bool>(type: "bit", nullable: false),
                    Propia = table.Column<bool>(type: "bit", nullable: false),
                    PropiaHipotecada = table.Column<bool>(type: "bit", nullable: false),
                    ActividadesEnOtroPais = table.Column<bool>(type: "bit", nullable: false),
                    DetallesActividadesEnOtroPais = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacionPersonal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OTP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CombinedData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatosDactilaresIdentificacion = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    DireccionMapaId = table.Column<int>(type: "int", nullable: true),
                    InformacionPersonalId = table.Column<int>(type: "int", nullable: true),
                    OTPId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombinedData", x => x.id);
                    table.ForeignKey(
                        name: "FK_CombinedData_DatosDactilares_DatosDactilaresIdentificacion",
                        column: x => x.DatosDactilaresIdentificacion,
                        principalTable: "DatosDactilares",
                        principalColumn: "Identificacion");
                    table.ForeignKey(
                        name: "FK_CombinedData_DireccionMapa_DireccionMapaId",
                        column: x => x.DireccionMapaId,
                        principalTable: "DireccionMapa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CombinedData_InformacionPersonal_InformacionPersonalId",
                        column: x => x.InformacionPersonalId,
                        principalTable: "InformacionPersonal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CombinedData_OTP_OTPId",
                        column: x => x.OTPId,
                        principalTable: "OTP",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CombinedData_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CombinedData_DatosDactilaresIdentificacion",
                table: "CombinedData",
                column: "DatosDactilaresIdentificacion");

            migrationBuilder.CreateIndex(
                name: "IX_CombinedData_DireccionMapaId",
                table: "CombinedData",
                column: "DireccionMapaId");

            migrationBuilder.CreateIndex(
                name: "IX_CombinedData_InformacionPersonalId",
                table: "CombinedData",
                column: "InformacionPersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_CombinedData_OTPId",
                table: "CombinedData",
                column: "OTPId");

            migrationBuilder.CreateIndex(
                name: "IX_CombinedData_UsuarioId",
                table: "CombinedData",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CombinedData");

            migrationBuilder.DropTable(
                name: "DatosDactilares");

            migrationBuilder.DropTable(
                name: "DireccionMapa");

            migrationBuilder.DropTable(
                name: "InformacionPersonal");

            migrationBuilder.DropTable(
                name: "OTP");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
