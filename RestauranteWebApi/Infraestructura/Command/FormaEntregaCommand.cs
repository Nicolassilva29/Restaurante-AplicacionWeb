using Application.Interfaces.IFormaEntrega;
using Domain;

namespace Infraestructura.Command
{
    public class FormaEntregaCommand : IFormaEntregaCommand
    {
        private readonly RestauranteContext _context;

        public FormaEntregaCommand(RestauranteContext context)
        {
            _context = context;
        }

        public void ActualizarFormaEntrega(FormaEntrega formaEntrega)
        {
            var FormaEntregaOriginal = _context.FormaEntrega.Single(fe => fe.FormaEntregaId == formaEntrega.FormaEntregaId);
            FormaEntregaOriginal.Descripcion = formaEntrega.Descripcion;
            _context.Update(FormaEntregaOriginal);
            _context.SaveChanges();
        }

        public void DeleteFormaEntrega(int formaEntregaId)
        {
            var FormaEntregaOriginal = _context.FormaEntrega.Single(fe => fe.FormaEntregaId == formaEntregaId);
            _context.Remove(FormaEntregaOriginal);
            _context.SaveChanges(); ;
        }

        public void InsertFormaEntrega(FormaEntrega formaEntrega)
        {
            _context.Add(formaEntrega);
            _context.SaveChanges();
        }
    }
}
