using Application.Interfaces.IComandaMercaderia;
using Domain;

namespace Infraestructura.Querys
{
    public class ComandaMercaderiaQuery : IComandaMercaderiaQuery
    {
        private readonly RestauranteContext _context;

        public ComandaMercaderiaQuery(RestauranteContext context)
        {
            _context = context;
        }
        public List<ComandaMercaderia> GetAll()
        {
            var ListaComandaMercaderia = _context.ComandaMercaderia.OrderBy(Cm => Cm.ComandaMercaderiaId).ToList();
            return ListaComandaMercaderia;
        }

        public ComandaMercaderia GetComandaMercaderiabyId(int comandaMercaderiaId)
        {
            var ComandaMercaderia = _context.ComandaMercaderia.Single(Cm => Cm.ComandaMercaderiaId == comandaMercaderiaId);
            return ComandaMercaderia;
        }
    }
}
