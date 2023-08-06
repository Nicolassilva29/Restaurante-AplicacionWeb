using Application.Interfaces.IMercaderia;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Querys
{
    public class MercaderiaQuery : IMercaderiaQuery
    {
        private readonly RestauranteContext _context;

        public MercaderiaQuery(RestauranteContext context)
        {
            _context = context;
        }
        public List<Mercaderia> GetAll()
        {
            var ListaMercaderia = _context.Mercaderia.OrderBy(m => m.MercaderiaId).ToList();
            return ListaMercaderia;
        }

        public Mercaderia GetMercaderiabyId(int mercaderiaId)
        {
            var mercaderia = _context.Mercaderia
                .Include(tm => tm.TipoMercaderia)
                .FirstOrDefault(m => m.MercaderiaId == mercaderiaId);
            return mercaderia;
        }

        public List<Mercaderia> GetMercaderiaInOrden(int tipoMercaderiaId, string nombre, string orden)
        {
            var mercaderias = _context.Mercaderia.Include(tm => tm.TipoMercaderia).OrderBy(p => p.Precio).ToList();

            if(nombre != null)
            {
                mercaderias = mercaderias.Where(n => n.Nombre.ToUpper().Contains(nombre.ToUpper())).ToList();
            }
            if(tipoMercaderiaId != 0)
            {
                mercaderias = mercaderias.Where(t => t.TipoMercaderiaId == tipoMercaderiaId).ToList();
            }
            if(orden.ToUpper() == "DESC")
            {
                return mercaderias.OrderByDescending(p => p.Precio).ToList();
            }
            return mercaderias;


        }

    }
}
