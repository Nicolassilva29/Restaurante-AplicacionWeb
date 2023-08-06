using Application.Interfaces.IMercaderia;
using Application.Request;
using Domain;

namespace Infraestructura.Command
{
    public class MercaderiaCommand : IMercaderiaCommand
    {
        private readonly RestauranteContext _context;

        public MercaderiaCommand(RestauranteContext context)
        {
            _context = context;
        }

        public Mercaderia DeleteMercaderia(int mercaderiaId)
        {
            var MercaderiaOriginal = _context.Mercaderia.Single(m => m.MercaderiaId == mercaderiaId);
            _context.Remove(MercaderiaOriginal);
            _context.SaveChanges();

            return MercaderiaOriginal;
        }

        public Mercaderia InsertMercaderia(Mercaderia mercaderia)
        {
            _context.Add(mercaderia);
            _context.SaveChanges();
            return mercaderia;
        }
        public Mercaderia ActualizarMercaderia(int mercaderiaId, MercaderiaRequest mercaderiaRequest)
        {
            var mercaderiaOriginal = _context.Mercaderia.FirstOrDefault(m => m.MercaderiaId == mercaderiaId);
            var tipoMercaderia = _context.TipoMercaderia.FirstOrDefault(tm => tm.TipoMercaderiaId == mercaderiaRequest.Tipo);

            mercaderiaOriginal.Nombre = mercaderiaRequest.Nombre;
            mercaderiaOriginal.TipoMercaderia = tipoMercaderia;
            mercaderiaOriginal.TipoMercaderiaId = tipoMercaderia.TipoMercaderiaId;
            mercaderiaOriginal.Precio = (int)mercaderiaRequest.Precio;
            mercaderiaOriginal.Ingredientes = mercaderiaRequest.Ingredientes;
            mercaderiaOriginal.Preparacion = mercaderiaRequest.Preparacion;
            mercaderiaOriginal.Imagen = mercaderiaRequest.Imagen;

            _context.Update(mercaderiaOriginal);
            _context.SaveChanges();
            return mercaderiaOriginal;
        }
    }
}
