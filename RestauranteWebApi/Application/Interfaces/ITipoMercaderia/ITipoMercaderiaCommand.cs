using Domain;

namespace Application.Interfaces.ITipoMercaderia
{
    public interface ITipoMercaderiaCommand
    {
        void InsertTipoMercaderia(TipoMercaderia tipoMercaderia);
        void DeleteTipoMercaderia(int tipoMercaderiaId);

        void ActualizarTipoMercaderia(TipoMercaderia tipoMercaderia);
    }
}
