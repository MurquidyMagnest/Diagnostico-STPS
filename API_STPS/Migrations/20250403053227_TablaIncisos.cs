using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_STPS.Migrations
{
    /// <inheritdoc />
    public partial class TablaIncisos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incisos_normas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_noms = table.Column<int>(type: "int", nullable: true),
                    inciso_noms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    comprobacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    criterio_acepton = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    observacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incisos_normas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incisos_normas");
        }
    }
}
