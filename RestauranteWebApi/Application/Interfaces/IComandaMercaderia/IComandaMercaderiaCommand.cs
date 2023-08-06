using Domain;

namespace Application.Interfaces.IComandaMercaderia
{
    public interface IComandaMercaderiaCommand
    {
        void InsertComandaMercaderia(ComandaMercaderia comandaMercaderia);
        void DeleteComandaMercaderia(int comandaMercaderiaId);

        void ActualizarComandaMercaderia(ComandaMercaderia comandaMercaderia);
    }
}
