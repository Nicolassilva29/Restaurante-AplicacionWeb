using Domain;

namespace Application.Interfaces.IComanda
{
    public interface IComandaQuery
    {
        Comanda GetComandabyId(Guid comandaId);
        List<Comanda> GetAll();
        List<Comanda> GetComandabyFecha(string fecha);
    }
}
