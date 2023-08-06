using Domain;

namespace Application.Interfaces.IMercaderia
{
    public interface IMercaderiaQuery
    {
        Mercaderia GetMercaderiabyId(int mercaderiaId);
        List<Mercaderia> GetAll();
        List<Mercaderia> GetMercaderiaInOrden(int tipoMercaderiaId, string nombre, string orden);

    }
}
