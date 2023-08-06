using Application.Request;
using Application.Response;
using Domain;

namespace Application.Interfaces.IMercaderia
{
    public interface IMercaderiaService
    {
        public MercaderiaResponse CreateMercaderia(MercaderiaRequest mercaderiaRequest);
        public MercaderiaResponse UpdateMercaderia(int mercaderiaId, MercaderiaRequest mercaderiaRequest);
        public MercaderiaResponse DeleteMercaderia(int mercaderiaId);
        public List<Mercaderia> GetAllMercaderia();
        public MercaderiaResponse GetMercaderiaById(int mercaderiaId);
        public List<MercaderiaGetResponse> GetMercaderiaInOrden( int tipoMercaderiaId, string nombre, string orden);
    }
}
