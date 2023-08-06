using Domain;

namespace Application.Interfaces.IFormaEntrega
{
    public interface IFormaEntregaCommand
    {
        void InsertFormaEntrega(FormaEntrega formaEntrega);
        void DeleteFormaEntrega(int formaEntregaId);

        void ActualizarFormaEntrega(FormaEntrega formaEntrega);
    }
}
