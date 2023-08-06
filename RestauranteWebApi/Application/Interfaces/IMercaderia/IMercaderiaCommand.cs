using Application.Request;
using Domain;

namespace Application.Interfaces.IMercaderia
{
    public interface IMercaderiaCommand
    {
        Mercaderia InsertMercaderia(Mercaderia mercaderia);
        Mercaderia DeleteMercaderia(int mercaderiaId);

        Mercaderia ActualizarMercaderia(int mercaderiaId, MercaderiaRequest mercaderiaRequest);
    }
}
