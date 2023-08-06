using Domain;

namespace Application.Interfaces.IComandaMercaderia
{
    public interface IComandaMercaderiaService
    {
        public ComandaMercaderia CreateComandaMercaderia(Mercaderia mercaderia, Comanda comanda);
        public void UpdateComandaMercaderia(ComandaMercaderia comandaMercaderia);
        public void DeleteComandaMercaderia(int comandaMercaderiaId);
        public List<ComandaMercaderia> GetAllComandaMercaderia();
        public ComandaMercaderia GetComandaMercaderiaById(int comandaMercaderiaId);
    }
}
