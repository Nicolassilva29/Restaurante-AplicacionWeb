using Microsoft.EntityFrameworkCore;
using Domain;

namespace Infraestructura
{
    public class RestauranteContext : DbContext
    {
        public RestauranteContext(DbContextOptions<RestauranteContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<FormaEntrega>().HasData(
                new FormaEntrega {FormaEntregaId = 1, Descripcion = "Salón" },
                new FormaEntrega {FormaEntregaId = 2, Descripcion = "Delivery" },
                new FormaEntrega {FormaEntregaId = 3, Descripcion = "Pedidos Ya" }
                );
            modelBuilder.Entity<TipoMercaderia>().HasData(
                new TipoMercaderia {TipoMercaderiaId = 1, Descripcion = "Entrada"},
                new TipoMercaderia {TipoMercaderiaId = 2, Descripcion = "Minutas" },
                new TipoMercaderia {TipoMercaderiaId = 3, Descripcion = "Pastas" },
                new TipoMercaderia {TipoMercaderiaId = 4, Descripcion = "Parrilla" },
                new TipoMercaderia {TipoMercaderiaId = 5, Descripcion = "Pizzas" },
                new TipoMercaderia {TipoMercaderiaId = 6, Descripcion = "Sandwich" },
                new TipoMercaderia {TipoMercaderiaId = 7, Descripcion = "Ensaladas" },
                new TipoMercaderia {TipoMercaderiaId = 8, Descripcion = "Bebidas" },
                new TipoMercaderia {TipoMercaderiaId = 9, Descripcion = "Cerveza Artesanal" },
                new TipoMercaderia {TipoMercaderiaId = 10, Descripcion = "Postres" }
                );
            modelBuilder.Entity<Mercaderia>().HasData(
                new Mercaderia {MercaderiaId = 1, Nombre = "Bastones de Muzzarella", TipoMercaderiaId = 1, Precio = 800, Ingredientes = "Pan rallado, huevo, muzarella", Preparacion = "Cortar la muzzarella en bastones, pasar por huevo y pan rallado. Freir en aceite caliente hasta dorar. Servir caliente", Imagen = "https://www.recetin.com/wp-content/uploads/2013/10/palitos_mozzarella.jpg" },
                new Mercaderia {MercaderiaId = 2, Nombre = "Empanadas fritas", TipoMercaderiaId = 1, Precio = 250, Ingredientes = "Masa, carne picada, cebolla, huevo", Preparacion = "Preparar la masa con harina, sal, agua y grasa. Rellenar con carne, cebolla, huevo. Freir en aceite caliente hasta dorar", Imagen = "https://statics.diariomendoza.com.ar/2021/05/61a5306da6e50.jpg" },
                new Mercaderia {MercaderiaId = 3, Nombre = "Milanesa", TipoMercaderiaId = 2, Precio = 1500, Ingredientes = "carne, sal, ajo, perejil, pan rallado, huevo", Preparacion = "Condimentar filetes de carne con sal, ajo y perejil. Pasar por huevo batido y pan rallado. Freir en aceite caliente hasta dorar.", Imagen = "https://vinomanos.com/wp-content/uploads/2019/02/milanesas-receta.jpg" },
                new Mercaderia {MercaderiaId = 4, Nombre = "Hamburguesa", TipoMercaderiaId = 2, Precio = 1200, Ingredientes = "carne picada, pan, tomate, lechuga, cebolla, aderezo", Preparacion = "Formar carne picada en disco. Cocinar a la parrilla o sartén. Armar hamburguesa en pan con lechuga, tomate, cebolla y aderezo a gusto.", Imagen = "https://previews.123rf.com/images/nblxer/nblxer1502/nblxer150200201/37174399-hamburguesa-casera-cerca-con-lechuga-fresca-verde-tomate-y-cebolla-roja-sobre-fondo-r%C3%BAstico.jpg" },
                new Mercaderia {MercaderiaId = 5, Nombre = "Ravioles con salsa blanca", TipoMercaderiaId = 3, Precio = 1800, Ingredientes = "Masa para ravioles, ricotta, espinacas, huevo, sal y pimienta, leche, harina, manteca", Preparacion = "Cocinar ravioles rellenos de ricotta y espinacas en agua con sal. Para la salsa blanca: derretir manteca, agregar harina, leche y condimentos.", Imagen = "https://chefs4estaciones.com/wp-content/uploads/2020/09/ravioles-con-salsa-de-3-quesos.jpg" },
                new Mercaderia {MercaderiaId = 6, Nombre = "Tallarines con bolognesa", TipoMercaderiaId = 3, Precio = 1700, Ingredientes = "Pasta para tallarines, agua, carne picada, cebolla, ajo, tomate, aceite, sal y pimienta", Preparacion = "Cocinar tallarines. En sartén, dorar carne picada en aceite. Agregar cebolla, ajo, tomate, sal y pimienta. Servir sobre los tallarines", Imagen = "https://imag.bonviveur.com/tallarines-a-la-bolonesa.jpg" },
                new Mercaderia {MercaderiaId = 7, Nombre = "Asado", TipoMercaderiaId = 4, Precio = 2500, Ingredientes = "vacío, asado, entraña, sal gruesa, carbón, chimichurri o salsa criolla.", Preparacion = "Encender fuego con carbón en la parrilla. Salar la carne. Cocinar a fuego medio/alto hasta lograr el término deseado. Cortar en tiras.", Imagen = "https://billiken.lat/wp-content/uploads/2022/06/asado-carne-cocinada-a-la-parrilla-quimica-SITIO.jpg" },
                new Mercaderia {MercaderiaId = 8, Nombre = "Matambre a la pizza", TipoMercaderiaId = 4, Precio = 2300, Ingredientes = "matambre, queso mozzarella, salsa de tomate, orégano, jamón cocido", Preparacion = "Limpiar matambre, estirar con mazo y salpimentar. Cocinarlo en la parilla. Colocar por encima jamón, queso, salsa de tomate y oregano. Cortar y servir", Imagen = "https://lasrecetasdelchef.net/wp-content/uploads/2016/11/matambre-a-la-pizza.jpg" },
                new Mercaderia {MercaderiaId = 9, Nombre = "Pizza de Muzarella", TipoMercaderiaId = 5, Precio = 1900, Ingredientes = "Masa para pizza, salsa de tomate, muzarella, aceitunas", Preparacion = "Estirar masa para pizza. Cubrir con salsa de tomate, queso muzarella y orégano. Cocinar en horno precalentado a 200°C por 10-15 min.", Imagen = "https://www.clarin.com/img/2022/10/05/utIOlIIyB_720x0__1.jpg" },
                new Mercaderia {MercaderiaId = 10, Nombre = "Pizza de Calabresa", TipoMercaderiaId = 5, Precio = 2000, Ingredientes = "Masa para pizza, salsa de tomate, muzarella, calabresa", Preparacion = "Estirar masa para pizza. Cubrir con salsa de tomate, queso muzarella, rodajas de calabresa. Cocinar en horno precalentado a 200°C por 10-15 min", Imagen = "https://as2.ftcdn.net/v2/jpg/02/96/96/71/1000_F_296967135_DAjY06XgRvqzYr3WYzHvmQp4C66QxM00.jpg" },
                new Mercaderia {MercaderiaId = 11, Nombre = "Sanguche de miga", TipoMercaderiaId = 6, Precio = 500, Ingredientes = "Pan de miga, jamon, queso, huevo duro, mayonesa", Preparacion = "Untar mayonesa en pan de miga. Rellenar con jamón, queso y huevo duro. ", Imagen = "https://super22deoctubre.com.ar/wp-content/uploads/2020/12/IMG_1046.jpg" },
                new Mercaderia {MercaderiaId = 12, Nombre = "Sanguche de pollo", TipoMercaderiaId = 6, Precio = 850, Ingredientes = "Pan, pollo cocido y desmenuzado, lechuga, tomate, cebolla, mayonesa, sal y pimienta.", Preparacion = "Mezclar pollo, mayonesa, sal y pimienta. Untar pan con la mezcla. Agregar lechuga, tomate y cebolla. Servir frío o tostado.", Imagen = "https://www.deliciosi.com/images/2200/2227/sandwich-de-pollo-desmenuzado.jpg" },
                new Mercaderia {MercaderiaId = 13, Nombre = "Ensalada Caesar", TipoMercaderiaId = 7, Precio = 750, Ingredientes = "Lechuga, pollo,crotones, parmesano, ajo, mayonesa, jugo de limón, aceite, salsa inglesa, mostaza de Dijon", Preparacion = "Cortar lechuga y colocar en ensaladera. Agregar pollo, crotones y parmesano. Mezclar ajo, mayonesa, limón, aceite, salsa inglesa y mostaza para preparar la salsa. Agregar a la ensalada", Imagen = "https://www.cocinacaserayfacil.net/wp-content/uploads/2018/06/Ensalada-cesar.jpg" },
                new Mercaderia {MercaderiaId = 14, Nombre = "Ensalada Rusa", TipoMercaderiaId = 7, Precio = 750, Ingredientes = "Papas, zanahorias, arvejas, mayonesa", Preparacion = "Cocinar papas, zanahorias y huevo. Cortar en cubos y mezclar con mayonesa y sal. Enfriar antes de servir.", Imagen = "https://cuk-it.com/wp-content/uploads/2020/11/thumb06-1-e1635893831894.jpg" },
                new Mercaderia {MercaderiaId = 15, Nombre = "Coca-Cola", TipoMercaderiaId = 8, Precio = 350, Ingredientes = " Botella de Coca-Cola x500ml", Preparacion = "-", Imagen = "https://previews.123rf.com/images/aryutkin/aryutkin2204/aryutkin220400017/185343105-la-gran-cantidad-de-botellas-de-vidrio-de-coca-cola-en-el-mostrador-del-supermercado-regi%C3%B3n-de.jpg" },
                new Mercaderia {MercaderiaId = 16, Nombre = "Agua Villavicencio", TipoMercaderiaId = 8, Precio = 250, Ingredientes = "Botella de Agua x500ml", Preparacion = "-", Imagen = "https://www.golomax.com.ar/uploads/centum/articles/original/13492_1.jpg" },
                new Mercaderia {MercaderiaId = 17, Nombre = "Cerveza Honey", TipoMercaderiaId = 9, Precio = 600, Ingredientes = "Chopp de cerveza Honey", Preparacion = "-", Imagen = "https://thumbs.dreamstime.com/b/pinta-de-honey-brown-beer-51858208.jpg" },
                new Mercaderia {MercaderiaId = 18, Nombre = "Cerveza IPA", TipoMercaderiaId = 9, Precio = 600, Ingredientes = "Chopp de cerveza IPA", Preparacion = "-", Imagen = "https://www.clarin.com/img/2020/08/18/PY22-FZSu_1256x620__2.jpg#1628113269253" },
                new Mercaderia {MercaderiaId = 19, Nombre = "Flan", TipoMercaderiaId = 10, Precio = 700, Ingredientes = "Leche, huevos, azúcar y caramelo.", Preparacion = "Mezclar 1L de leche, 6 huevos, 150g de azúcar. Verter en molde acaramelado y hornear a baño de María a 180°C por 1 hora. Enfriar y servir.", Imagen = "https://www.lactaid.com/sites/lactaid_us/files/recipe-images/easy_flan2.jpg" },
                new Mercaderia {MercaderiaId = 20, Nombre = "Copa Ensalada de frutas", TipoMercaderiaId = 10, Precio = 700, Ingredientes = "Naranja, manzana, uva, kiwi, durazno, anana, jugo de naranja", Preparacion = "Picar las frutas en cubos. Mezclar con jugo de naranja. Refrigerador por 30 minutos. Servir frío.", Imagen = "https://www.bancodealimentoschicago.org/wp-content/uploads/2022/04/Fruit-Salad.jpg" }
                );


           // Fluent API
           modelBuilder.Entity<TipoMercaderia>()
                .HasMany(g => g.Mercaderia)
                .WithOne(s => s.TipoMercaderia)
                .HasForeignKey(s => s.TipoMercaderiaId);

            modelBuilder.Entity<Mercaderia>()
                .HasMany(g => g.ComandaMercaderia)
                .WithOne(s => s.Mercaderia)
                .HasForeignKey(s => s.MercaderiaId)
                .IsRequired();

            modelBuilder.Entity<FormaEntrega>()
                .HasMany(g => g.Comanda)
                .WithOne(s => s.FormaEntrega)
                .HasForeignKey(s => s.FormaEntregaId);

            modelBuilder.Entity<Comanda>()
                .HasMany(g => g.ComandaMercaderia)
                .WithOne(s => s.Comanda)
                .HasForeignKey(s => s.ComandaId)
                .IsRequired();

            modelBuilder.Entity<Mercaderia>(entity =>
            {
                entity.Property(t => t.Nombre)
                      .HasMaxLength(50)
                      .HasColumnType("nvarchar")
                      .IsRequired();

                entity.Property(t => t.Ingredientes)
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar")
                      .IsRequired();

                entity.Property(t => t.Preparacion)
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar")
                      .IsRequired();

                entity.Property(t => t.Imagen)
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar")
                      .IsRequired();
            });
            modelBuilder.Entity<TipoMercaderia>(entity =>
            {
                entity.Property(t => t.Descripcion)
                      .HasMaxLength(50)
                      .HasColumnType("nvarchar")
                      .IsRequired();

            });
            modelBuilder.Entity<FormaEntrega>(entity =>
            {
                entity.Property(t => t.Descripcion)
                      .HasMaxLength(50)
                      .HasColumnType("nvarchar")
                      .IsRequired();

            });

            modelBuilder.Entity<Mercaderia>().Property(p => p.MercaderiaId).ValueGeneratedOnAdd();
            modelBuilder.Entity<TipoMercaderia>().Property(p => p.TipoMercaderiaId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Comanda>().Property(p => p.ComandaId).ValueGeneratedOnAdd();
            modelBuilder.Entity<ComandaMercaderia>().Property(p => p.ComandaMercaderiaId).ValueGeneratedOnAdd();
            modelBuilder.Entity<FormaEntrega>().Property(p => p.FormaEntregaId).ValueGeneratedOnAdd();

        }
        //entities
        public DbSet<Mercaderia> Mercaderia { get; set; }
        public DbSet<TipoMercaderia> TipoMercaderia { get; set; }
        public DbSet<FormaEntrega> FormaEntrega { get; set; }
        public DbSet<Comanda> Comanda { get; set; }
        public DbSet<ComandaMercaderia> ComandaMercaderia { get; set; }

    }
}