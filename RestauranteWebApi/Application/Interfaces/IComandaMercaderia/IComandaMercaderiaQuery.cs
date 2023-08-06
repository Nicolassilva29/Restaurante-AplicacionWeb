using Domain;

namespace Application.Interfaces.IComandaMercaderia
{
    public interface IComandaMercaderiaQuery
    {
        ComandaMercaderia GetComandaMercaderiabyId(int comandaMercaderiaId);
        List<ComandaMercaderia> GetAll();
    }
}
