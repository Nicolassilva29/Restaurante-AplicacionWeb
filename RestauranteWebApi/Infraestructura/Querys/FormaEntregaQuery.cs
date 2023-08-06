using Application.Interfaces.IFormaEntrega;
using Domain;

namespace Infraestructura.Querys
{
    public class FormaEntregaQuery : IFormaEntregaQuery
    {
        private readonly RestauranteContext _context;

        public FormaEntregaQuery(RestauranteContext context)
        {
            _context = context;
        }

        public List<FormaEntrega> GetAll()
        {
            var ListaFormaEntrega = _context.FormaEntrega.OrderBy(fe => fe.FormaEntregaId).ToList();
            return ListaFormaEntrega;
        }

        public FormaEntrega GetFormaEntregabyId(int formaEntregaId)
        {
            var FormaEntrega = _context.FormaEntrega.FirstOrDefault(fe => fe.FormaEntregaId == formaEntregaId);
            return FormaEntrega;
        }
    }
}
