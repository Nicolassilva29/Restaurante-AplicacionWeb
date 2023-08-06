using Domain;

namespace Application.Interfaces.ITipoMercaderia
{
    public interface ITipoMercaderiaQuery
    {
        TipoMercaderia GetTipoMercaderiabyId(int tipoMercaderiaId);
        List<TipoMercaderia> GetAll();
    }
}
