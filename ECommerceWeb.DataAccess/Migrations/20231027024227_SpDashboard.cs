using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWeb.Server.Migrations
{
    /// <inheritdoc />
    public partial class SpDashboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create procedure uspDashBoard
AS
BEGIN

	SELECT 
		SUM(V.Total) AS TotalVenta,
		COUNT(V.ID) AS CantidadVentas,
		(SELECT COUNT(C.ID) FROM Cliente C) CantidadClientes,
		(SELECT COUNT(P.ID) FROM Producto P WHERE P.Estado = 1) CantidadProductos
	FROM Venta V
	WHERE V.Estado = 1
END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROC uspDashBoard");
        }
    }
}
