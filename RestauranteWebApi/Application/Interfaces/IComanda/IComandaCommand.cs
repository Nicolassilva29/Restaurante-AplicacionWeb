using Domain;

namespace Application.Interfaces.IComanda
{
    public interface IComandaCommand
    {
        void InsertComanda(Comanda comanda);
        void DeleteComanda(Guid comandaId);

        void ActualizarComanda(Comanda comanda);
    }
}
