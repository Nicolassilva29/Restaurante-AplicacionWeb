using Application.Interfaces.ITipoMercaderia;
using Domain;

namespace Infraestructura.Querys
{
    public class TipoMercaderiaQuery : ITipoMercaderiaQuery
    {
        private readonly RestauranteContext _context;

        public TipoMercaderiaQuery(RestauranteContext context)
        {
            _context = context;
        }
        public List<TipoMercaderia> GetAll()
        {
            var ListaTipoMercaderia = _context.TipoMercaderia.OrderBy(m => m.TipoMercaderiaId).ToList();
            return ListaTipoMercaderia;
        }

        public TipoMercaderia GetTipoMercaderiabyId(int tipoMercaderiaId)
        {
            var TipoMercaderia = _context.TipoMercaderia.Single(m => m.TipoMercaderiaId == tipoMercaderiaId);
            return TipoMercaderia;
        }
    }
}
