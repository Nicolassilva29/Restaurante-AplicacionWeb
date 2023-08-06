using Domain;

namespace Application.Interfaces.IFormaEntrega
{
    public interface IFormaEntregaQuery
    {
        FormaEntrega GetFormaEntregabyId(int formaEntregaId);
        List<FormaEntrega> GetAll();
    }
}
