using Application.Interfaces.IComanda;
using Domain;

namespace Infraestructura.Command
{
    public class ComandaCommand : IComandaCommand
    {
        private readonly RestauranteContext _context;

        public ComandaCommand(RestauranteContext context)
        {
            _context = context;
        }

        public void ActualizarComanda(Comanda comanda)
        {
            var ComandaOriginal = _context.Comanda.Single(C => C.ComandaId == comanda.ComandaId);
            ComandaOriginal.FormaEntregaId = comanda.FormaEntregaId;
            ComandaOriginal.PrecioTotal = comanda.PrecioTotal;
            ComandaOriginal.Fecha = comanda.Fecha;
            _context.Update(ComandaOriginal);
            _context.SaveChanges();
        }

        public void DeleteComanda(Guid comandaId)
        {
            var ComandaOriginal = _context.Comanda.Single(C => C.ComandaId == comandaId);
            _context.Remove(ComandaOriginal);
            _context.SaveChanges(); 
        }

        public void InsertComanda(Comanda comanda)
        {
            _context.Add(comanda);
            _context.SaveChanges();
        }
    }
}
