using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class DBRestaurante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormaEntrega",
                columns: table => new
                {
                    FormaEntregaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaEntrega", x => x.FormaEntregaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoMercaderia",
                columns: table => new
                {
                    TipoMercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMercaderia", x => x.TipoMercaderiaId);
                });

            migrationBuilder.CreateTable(
                name: "Comanda",
                columns: table => new
                {
                    ComandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormaEntregaId = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comanda", x => x.ComandaId);
                    table.ForeignKey(
                        name: "FK_Comanda_FormaEntrega_FormaEntregaId",
                        column: x => x.FormaEntregaId,
                        principalTable: "FormaEntrega",
                        principalColumn: "FormaEntregaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mercaderia",
                columns: table => new
                {
                    MercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoMercaderiaId = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    Ingredientes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Preparacion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mercaderia", x => x.MercaderiaId);
                    table.ForeignKey(
                        name: "FK_Mercaderia_TipoMercaderia_TipoMercaderiaId",
                        column: x => x.TipoMercaderiaId,
                        principalTable: "TipoMercaderia",
                        principalColumn: "TipoMercaderiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComandaMercaderia",
                columns: table => new
                {
                    ComandaMercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MercaderiaId = table.Column<int>(type: "int", nullable: false),
                    ComandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComandaMercaderia", x => x.ComandaMercaderiaId);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderia_Comanda_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comanda",
                        principalColumn: "ComandaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderia_Mercaderia_MercaderiaId",
                        column: x => x.MercaderiaId,
                        principalTable: "Mercaderia",
                        principalColumn: "MercaderiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FormaEntrega",
                columns: new[] { "FormaEntregaId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Salón" },
                    { 2, "Delivery" },
                    { 3, "Pedidos Ya" }
                });

            migrationBuilder.InsertData(
                table: "TipoMercaderia",
                columns: new[] { "TipoMercaderiaId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Entrada" },
                    { 2, "Minutas" },
                    { 3, "Pastas" },
                    { 4, "Parrilla" },
                    { 5, "Pizzas" },
                    { 6, "Sandwich" },
                    { 7, "Ensaladas" },
                    { 8, "Bebidas" },
                    { 9, "Cerveza Artesanal" },
                    { 10, "Postres" }
                });

            migrationBuilder.InsertData(
                table: "Mercaderia",
                columns: new[] { "MercaderiaId", "Imagen", "Ingredientes", "Nombre", "Precio", "Preparacion", "TipoMercaderiaId" },
                values: new object[,]
                {
                    { 1, "https://www.recetin.com/wp-content/uploads/2013/10/palitos_mozzarella.jpg", "Pan rallado, huevo, muzarella", "Bastones de Muzzarella", 800, "Cortar la muzzarella en bastones, pasar por huevo y pan rallado. Freir en aceite caliente hasta dorar. Servir caliente", 1 },
                    { 2, "https://statics.diariomendoza.com.ar/2021/05/61a5306da6e50.jpg", "Masa, carne picada, cebolla, huevo", "Empanadas fritas", 250, "Preparar la masa con harina, sal, agua y grasa. Rellenar con carne, cebolla, huevo. Freir en aceite caliente hasta dorar", 1 },
                    { 3, "https://vinomanos.com/wp-content/uploads/2019/02/milanesas-receta.jpg", "carne, sal, ajo, perejil, pan rallado, huevo", "Milanesa", 1500, "Condimentar filetes de carne con sal, ajo y perejil. Pasar por huevo batido y pan rallado. Freir en aceite caliente hasta dorar.", 2 },
                    { 4, "https://previews.123rf.com/images/nblxer/nblxer1502/nblxer150200201/37174399-hamburguesa-casera-cerca-con-lechuga-fresca-verde-tomate-y-cebolla-roja-sobre-fondo-r%C3%BAstico.jpg", "carne picada, pan, tomate, lechuga, cebolla, aderezo", "Hamburguesa", 1200, "Formar carne picada en disco. Cocinar a la parrilla o sartén. Armar hamburguesa en pan con lechuga, tomate, cebolla y aderezo a gusto.", 2 },
                    { 5, "https://chefs4estaciones.com/wp-content/uploads/2020/09/ravioles-con-salsa-de-3-quesos.jpg", "Masa para ravioles, ricotta, espinacas, huevo, sal y pimienta, leche, harina, manteca", "Ravioles con salsa blanca", 1800, "Cocinar ravioles rellenos de ricotta y espinacas en agua con sal. Para la salsa blanca: derretir manteca, agregar harina, leche y condimentos.", 3 },
                    { 6, "https://imag.bonviveur.com/tallarines-a-la-bolonesa.jpg", "Pasta para tallarines, agua, carne picada, cebolla, ajo, tomate, aceite, sal y pimienta", "Tallarines con bolognesa", 1700, "Cocinar tallarines. En sartén, dorar carne picada en aceite. Agregar cebolla, ajo, tomate, sal y pimienta. Servir sobre los tallarines", 3 },
                    { 7, "https://billiken.lat/wp-content/uploads/2022/06/asado-carne-cocinada-a-la-parrilla-quimica-SITIO.jpg", "vacío, asado, entraña, sal gruesa, carbón, chimichurri o salsa criolla.", "Asado", 2500, "Encender fuego con carbón en la parrilla. Salar la carne. Cocinar a fuego medio/alto hasta lograr el término deseado. Cortar en tiras.", 4 },
                    { 8, "https://lasrecetasdelchef.net/wp-content/uploads/2016/11/matambre-a-la-pizza.jpg", "matambre, queso mozzarella, salsa de tomate, orégano, jamón cocido", "Matambre a la pizza", 2300, "Limpiar matambre, estirar con mazo y salpimentar. Cocinarlo en la parilla. Colocar por encima jamón, queso, salsa de tomate y oregano. Cortar y servir", 4 },
                    { 9, "https://www.clarin.com/img/2022/10/05/utIOlIIyB_720x0__1.jpg", "Masa para pizza, salsa de tomate, muzarella, aceitunas", "Pizza de Muzarella", 1900, "Estirar masa para pizza. Cubrir con salsa de tomate, queso muzarella y orégano. Cocinar en horno precalentado a 200°C por 10-15 min.", 5 },
                    { 10, "https://as2.ftcdn.net/v2/jpg/02/96/96/71/1000_F_296967135_DAjY06XgRvqzYr3WYzHvmQp4C66QxM00.jpg", "Masa para pizza, salsa de tomate, muzarella, calabresa", "Pizza de Calabresa", 2000, "Estirar masa para pizza. Cubrir con salsa de tomate, queso muzarella, rodajas de calabresa. Cocinar en horno precalentado a 200°C por 10-15 min", 5 },
                    { 11, "https://super22deoctubre.com.ar/wp-content/uploads/2020/12/IMG_1046.jpg", "Pan de miga, jamon, queso, huevo duro, mayonesa", "Sanguche de miga", 500, "Untar mayonesa en pan de miga. Rellenar con jamón, queso y huevo duro. ", 6 },
                    { 12, "https://www.deliciosi.com/images/2200/2227/sandwich-de-pollo-desmenuzado.jpg", "Pan, pollo cocido y desmenuzado, lechuga, tomate, cebolla, mayonesa, sal y pimienta.", "Sanguche de pollo", 850, "Mezclar pollo, mayonesa, sal y pimienta. Untar pan con la mezcla. Agregar lechuga, tomate y cebolla. Servir frío o tostado.", 6 },
                    { 13, "https://www.cocinacaserayfacil.net/wp-content/uploads/2018/06/Ensalada-cesar.jpg", "Lechuga, pollo,crotones, parmesano, ajo, mayonesa, jugo de limón, aceite, salsa inglesa, mostaza de Dijon", "Ensalada Caesar", 750, "Cortar lechuga y colocar en ensaladera. Agregar pollo, crotones y parmesano. Mezclar ajo, mayonesa, limón, aceite, salsa inglesa y mostaza para preparar la salsa. Agregar a la ensalada", 7 },
                    { 14, "https://cuk-it.com/wp-content/uploads/2020/11/thumb06-1-e1635893831894.jpg", "Papas, zanahorias, arvejas, mayonesa", "Ensalada Rusa", 750, "Cocinar papas, zanahorias y huevo. Cortar en cubos y mezclar con mayonesa y sal. Enfriar antes de servir.", 7 },
                    { 15, "https://previews.123rf.com/images/aryutkin/aryutkin2204/aryutkin220400017/185343105-la-gran-cantidad-de-botellas-de-vidrio-de-coca-cola-en-el-mostrador-del-supermercado-regi%C3%B3n-de.jpg", " Botella de Coca-Cola x500ml", "Coca-Cola", 350, "-", 8 },
                    { 16, "https://www.golomax.com.ar/uploads/centum/articles/original/13492_1.jpg", "Botella de Agua x500ml", "Agua Villavicencio", 250, "-", 8 },
                    { 17, "https://thumbs.dreamstime.com/b/pinta-de-honey-brown-beer-51858208.jpg", "Chopp de cerveza Honey", "Cerveza Honey", 600, "-", 9 },
                    { 18, "https://www.clarin.com/img/2020/08/18/PY22-FZSu_1256x620__2.jpg#1628113269253", "Chopp de cerveza IPA", "Cerveza IPA", 600, "-", 9 },
                    { 19, "https://www.lactaid.com/sites/lactaid_us/files/recipe-images/easy_flan2.jpg", "Leche, huevos, azúcar y caramelo.", "Flan", 700, "Mezclar 1L de leche, 6 huevos, 150g de azúcar. Verter en molde acaramelado y hornear a baño de María a 180°C por 1 hora. Enfriar y servir.", 10 },
                    { 20, "https://www.bancodealimentoschicago.org/wp-content/uploads/2022/04/Fruit-Salad.jpg", "Naranja, manzana, uva, kiwi, durazno, anana, jugo de naranja", "Copa Ensalada de frutas", 700, "Picar las frutas en cubos. Mezclar con jugo de naranja. Refrigerador por 30 minutos. Servir frío.", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_FormaEntregaId",
                table: "Comanda",
                column: "FormaEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderia_ComandaId",
                table: "ComandaMercaderia",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderia_MercaderiaId",
                table: "ComandaMercaderia",
                column: "MercaderiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mercaderia_TipoMercaderiaId",
                table: "Mercaderia",
                column: "TipoMercaderiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComandaMercaderia");

            migrationBuilder.DropTable(
                name: "Comanda");

            migrationBuilder.DropTable(
                name: "Mercaderia");

            migrationBuilder.DropTable(
                name: "FormaEntrega");

            migrationBuilder.DropTable(
                name: "TipoMercaderia");
        }
    }
}
