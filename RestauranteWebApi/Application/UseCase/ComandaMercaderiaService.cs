using Application.Interfaces.IComandaMercaderia;
using Domain;

namespace Application.UseCase
{
    public class ComandaMercaderiaService : IComandaMercaderiaService
    {
        private readonly IComandaMercaderiaCommand _command;
        private readonly IComandaMercaderiaQuery _querys;

        public ComandaMercaderiaService(IComandaMercaderiaCommand command, IComandaMercaderiaQuery querys)
        {
            _command = command;
            _querys = querys;
        }

        public ComandaMercaderia CreateComandaMercaderia(Mercaderia mercaderia, Comanda comanda)
        {
            ComandaMercaderia comandaMercaderia = new ComandaMercaderia
            {
                Mercaderia = mercaderia,
                MercaderiaId = mercaderia.MercaderiaId,
                Comanda = comanda,
                ComandaId = comanda.ComandaId
            };
            _command.InsertComandaMercaderia(comandaMercaderia);
            return comandaMercaderia;
        }

        public void DeleteComandaMercaderia(int comandaMercaderiaId)
        {
            _command.DeleteComandaMercaderia(comandaMercaderiaId);        }

        public List<ComandaMercaderia> GetAllComandaMercaderia()
        {
            return _querys.GetAll();
        }

        public ComandaMercaderia GetComandaMercaderiaById(int comandaMercaderiaId)
        {
            return _querys.GetComandaMercaderiabyId(comandaMercaderiaId);
        }

        public void UpdateComandaMercaderia(ComandaMercaderia comandaMercaderia)
        {
            _command.ActualizarComandaMercaderia(comandaMercaderia);
        }
    }
}
