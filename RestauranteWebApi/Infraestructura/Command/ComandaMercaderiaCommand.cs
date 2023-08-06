using Application.Interfaces.IComandaMercaderia;
using Domain;

namespace Infraestructura.Command
{
    public class ComandaMercaderiaCommand : IComandaMercaderiaCommand
    {
        private readonly RestauranteContext _context;

        public ComandaMercaderiaCommand(RestauranteContext context)
        {
            _context = context;
        }

        public void ActualizarComandaMercaderia(ComandaMercaderia comandaMercaderia)
        {
            var ComandaMercaderiaOriginal = _context.ComandaMercaderia.Single(Cm => Cm.ComandaMercaderiaId == comandaMercaderia.ComandaMercaderiaId);
            ComandaMercaderiaOriginal.MercaderiaId = comandaMercaderia.MercaderiaId;
            ComandaMercaderiaOriginal.ComandaId = comandaMercaderia.ComandaId;
            _context.Update(ComandaMercaderiaOriginal);
            _context.SaveChanges();
        }

        public void DeleteComandaMercaderia(int comandaMercaderiaId)
        {
            var ComandaMercaderiaOriginal = _context.ComandaMercaderia.Single(Cm => Cm.ComandaMercaderiaId == comandaMercaderiaId);
            _context.Remove(ComandaMercaderiaOriginal);
            _context.SaveChanges();
        }

        public void InsertComandaMercaderia(ComandaMercaderia comandaMercaderia)
        {
            _context.Add(comandaMercaderia);
            _context.SaveChanges();
        }
    }
}
