using Application.Interfaces.IFormaEntrega;
using Domain;

namespace Application.UseCase
{
    public class FormaEntregaService : IFormaEntregaService
    {
        private readonly IFormaEntregaCommand _command;
        private readonly IFormaEntregaQuery _querys;

        public FormaEntregaService(IFormaEntregaCommand command, IFormaEntregaQuery querys)
        {
            _command = command;
            _querys = querys;
        }

        public FormaEntrega CreateFormaEntrega(string descripcion)
        {
            var FormaEntreganueva = new FormaEntrega
            {
                Descripcion = descripcion

            };
            _command.InsertFormaEntrega(FormaEntreganueva);
            return FormaEntreganueva;
        }

        public void DeleteFormaEntrega(int formaEntregaId)
        {
            _command.DeleteFormaEntrega(formaEntregaId);
        }

        public List<FormaEntrega> GetAllFormaEntrega()
        {
            return _querys.GetAll();
        }

        public FormaEntrega GetFormaEntregaById(int formaEntregaId)
        {
            return _querys.GetFormaEntregabyId(formaEntregaId);
        }

        public void UpdateFormaEntrega(FormaEntrega formaEntrega)
        {
            _command.ActualizarFormaEntrega(formaEntrega);
        }
    }
}
