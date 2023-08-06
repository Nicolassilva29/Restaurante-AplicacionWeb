using Domain;

namespace Application.Interfaces.ITipoMercaderia
{
    public interface ITipoMercaderiaService
    {
        public TipoMercaderia CreateTipoMercaderia(TipoMercaderia tipoMercaderia);
        public void UpdateTipoMercaderia(TipoMercaderia tipoMercaderia);
        public void DeleteTipoMercaderia(int tipoMercaderiaId);
        public List<TipoMercaderia> GetAllTipoMercaderia();
        public TipoMercaderia GetTipoMercaderiaById(int tipoMercaderiaId);
    }
}
