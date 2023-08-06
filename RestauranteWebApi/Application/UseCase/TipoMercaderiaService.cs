using Application.Interfaces.ITipoMercaderia;
using Domain;

namespace Application.UseCase
{
    public class TipoMercaderiaService : ITipoMercaderiaService
    {
        private readonly ITipoMercaderiaCommand _command;
        private readonly ITipoMercaderiaQuery _querys;

        public TipoMercaderiaService(ITipoMercaderiaCommand command, ITipoMercaderiaQuery querys)
        {
            _command = command;
            _querys = querys;
        }
        public TipoMercaderia CreateTipoMercaderia(TipoMercaderia tipoMercaderia)
        {
            var TipoMercaderianueva = new TipoMercaderia
            {
                TipoMercaderiaId = tipoMercaderia.TipoMercaderiaId,
                Descripcion = tipoMercaderia.Descripcion

            };
            _command.InsertTipoMercaderia(TipoMercaderianueva);
            return TipoMercaderianueva;
        }

        public void DeleteTipoMercaderia(int tipoMercaderiaId)
        {
            _command.DeleteTipoMercaderia(tipoMercaderiaId);
        }

        public List<TipoMercaderia> GetAllTipoMercaderia()
        {
            return (_querys.GetAll());
        }

        public TipoMercaderia GetTipoMercaderiaById(int tipoMercaderiaId)
        {
           return _querys.GetTipoMercaderiabyId(tipoMercaderiaId);
        }

        public void UpdateTipoMercaderia(TipoMercaderia tipoMercaderia)
        {
            _command.ActualizarTipoMercaderia(tipoMercaderia);
        }
    }
}
