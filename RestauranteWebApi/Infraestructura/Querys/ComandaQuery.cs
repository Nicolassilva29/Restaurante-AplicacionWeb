using Application.Interfaces.IComanda;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Infraestructura.Querys
{
    public class ComandaQuery : IComandaQuery
    {
        private readonly RestauranteContext _context;

        public ComandaQuery(RestauranteContext context)
        {
            _context = context;
        }
        public List<Comanda> GetAll()
        {
            var ListaComanda = _context.Comanda.OrderBy(C => C.ComandaId).ToList();
            return ListaComanda;
        }

        public List<Comanda> GetComandabyFecha(string fecha)
        {
            var comanda = _context.Comanda.Include(tm => tm.ComandaMercaderia).OrderBy(p => p.Fecha).ToList();

            if (fecha != null)
            {
                comanda = comanda.Where(C => C.Fecha.ToString().Contains(fecha)).ToList();
            }

            return comanda;
        }

        public Comanda GetComandabyId(Guid comandaId)
        {
            var Comanda = _context.Comanda
                .Include(cm => cm.ComandaMercaderia)
                .FirstOrDefault(C=> C.ComandaId == comandaId);
            return Comanda;
        }
    }
}
