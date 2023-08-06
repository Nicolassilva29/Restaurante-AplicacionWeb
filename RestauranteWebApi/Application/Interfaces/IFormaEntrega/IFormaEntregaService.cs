using Domain;

namespace Application.Interfaces.IFormaEntrega
{
    public interface IFormaEntregaService
    {
        public FormaEntrega CreateFormaEntrega(string descripcion);
        public void UpdateFormaEntrega(FormaEntrega formaEntrega);
        public void DeleteFormaEntrega(int formaEntregaId);
        public List<FormaEntrega> GetAllFormaEntrega();
        public FormaEntrega GetFormaEntregaById(int formaEntregaId);
    }
}
