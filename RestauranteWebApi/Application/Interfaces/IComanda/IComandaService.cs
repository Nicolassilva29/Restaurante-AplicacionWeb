using Application.Request;
using Application.Response;
using Domain;

namespace Application.Interfaces.IComanda
{
    public interface IComandaService
    {
        public ComandaResponse CreateComanda(ComandaRequest comandaRequest );
        public void UpdateComanda(Comanda comanda);
        public void DeleteComanda(Guid comandaId);
        public List<Comanda> GetAllComanda();
        public ComandaGetResponse GetComandaById(Guid comandaId);
        public List<ComandaResponse> GetComandaByFecha(string fecha);
    }

}
