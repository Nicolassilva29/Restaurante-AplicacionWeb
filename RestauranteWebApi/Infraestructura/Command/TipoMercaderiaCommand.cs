using Application.Interfaces.ITipoMercaderia;
using Domain;

namespace Infraestructura.Command
{
    public class TipoMercaderiaCommand : ITipoMercaderiaCommand
    {
        private readonly RestauranteContext _context;

        public TipoMercaderiaCommand(RestauranteContext context)
        {
            _context = context;
        }

        public void ActualizarTipoMercaderia(TipoMercaderia tipoMercaderias)
        {
            var TipoMercaderiaOriginal = _context.TipoMercaderia.Single(tm => tm.TipoMercaderiaId == tipoMercaderias.TipoMercaderiaId);
            TipoMercaderiaOriginal.Descripcion = tipoMercaderias.Descripcion;
            _context.Update(TipoMercaderiaOriginal);
            _context.SaveChanges();
        }

        public void DeleteTipoMercaderia(int tipoMercaderiaId)
        {
            var TipoMercaderiaOriginal = _context.TipoMercaderia.Single(m => m.TipoMercaderiaId == tipoMercaderiaId);
            _context.Remove(TipoMercaderiaOriginal);
            _context.SaveChanges(); ;
        }

        public void InsertTipoMercaderia(TipoMercaderia tipoMercaderia)
        {
            _context.Add(tipoMercaderia);
            _context.SaveChanges();
        }
    }
}
