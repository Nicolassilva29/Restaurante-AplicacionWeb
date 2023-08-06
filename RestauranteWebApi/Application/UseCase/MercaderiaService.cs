using Application.Exceptions;
using Application.Interfaces.IComandaMercaderia;
using Application.Interfaces.IMercaderia;
using Application.Interfaces.ITipoMercaderia;
using Application.Request;
using Application.Response;
using Domain;

namespace Application.UseCase
{
    public class MercaderiaService : IMercaderiaService
    {
        private readonly IMercaderiaCommand _command;
        private readonly IMercaderiaQuery _querys;
        private readonly ITipoMercaderiaQuery _tipoMercaderiaQuery;
        private readonly IComandaMercaderiaQuery _comandaMercaderia;


        public MercaderiaService(IMercaderiaCommand command, IMercaderiaQuery querys, ITipoMercaderiaQuery tipoMercaderiaQuery, IComandaMercaderiaQuery comandaMercaderiaQuery)
        {
            _command = command;
            _querys = querys;
            _tipoMercaderiaQuery = tipoMercaderiaQuery;
            _comandaMercaderia = comandaMercaderiaQuery;
        }

        public MercaderiaResponse CreateMercaderia(MercaderiaRequest mercaderiaRequest)
        {
            bool ExisteNombre = _querys.GetAll().Any(m => m.Nombre.ToUpper() == mercaderiaRequest.Nombre.ToUpper());
            
            if (ExisteNombre) 
            { 
                return null; 
            }
            bool ExisteTipo = _tipoMercaderiaQuery.GetAll().Any(tm => tm.TipoMercaderiaId == mercaderiaRequest.Tipo);
            if (!ExisteTipo) { throw new ValorInvalidoException("Tipo de mercaderia Inexistente"); }
           

            var mercaderianueva = new Mercaderia
            {
                Nombre = mercaderiaRequest.Nombre,
                TipoMercaderiaId = mercaderiaRequest.Tipo,
                Precio = (int)mercaderiaRequest.Precio,
                Ingredientes = mercaderiaRequest.Ingredientes,
                Preparacion = mercaderiaRequest.Preparacion,
                Imagen = mercaderiaRequest.Imagen
            };
            _command.InsertMercaderia(mercaderianueva);
            return new MercaderiaResponse
            {
                Id = mercaderianueva.MercaderiaId,
                Nombre = mercaderianueva.Nombre,
                Tipo = new TipoMercaderiaResponse
                {
                    Id = mercaderianueva.TipoMercaderiaId,
                    Descripcion = _tipoMercaderiaQuery.GetTipoMercaderiabyId(mercaderiaRequest.Tipo).Descripcion
                },
                Precio = mercaderianueva.Precio,
                Ingredientes = mercaderianueva.Ingredientes,
                Preparacion = mercaderianueva.Preparacion,
                Imagen = mercaderianueva.Imagen
            };
        }

        public MercaderiaResponse DeleteMercaderia(int mercaderiaId)
        {
            bool ValidarMercaderia = _querys.GetAll().Any(c => c.MercaderiaId == mercaderiaId);
            if (!ValidarMercaderia) { throw new ValorInvalidoException("La mercadería con ID " + mercaderiaId + " no existe en la base de datos."); }
            var listaComandaMercaderia = _comandaMercaderia.GetAll();
            foreach(var comandaMercaderia in listaComandaMercaderia)
            {
                if(comandaMercaderia.MercaderiaId == mercaderiaId)
                {
                    return null;
                }
            }
            var mercaderia = _command.DeleteMercaderia(mercaderiaId);
            return new MercaderiaResponse
            {
                Id = mercaderia.MercaderiaId,
                Nombre = mercaderia.Nombre,
                Tipo = new TipoMercaderiaResponse
                {
                    Id = mercaderia.TipoMercaderiaId,
                    Descripcion = _tipoMercaderiaQuery.GetTipoMercaderiabyId(mercaderia.TipoMercaderiaId).Descripcion
                },
                Precio = mercaderia.Precio,
                Ingredientes = mercaderia.Ingredientes,
                Preparacion = mercaderia.Preparacion,
                Imagen = mercaderia.Imagen
            };
        }

        public List<Mercaderia> GetAllMercaderia()
        {
            return (_querys.GetAll());
        }

        public MercaderiaResponse GetMercaderiaById(int mercaderiaId)
        {
            var mercaderia = _querys.GetMercaderiabyId(mercaderiaId);
            if (mercaderia == null) { return null; }
            return new MercaderiaResponse
            {
                Id = mercaderia.MercaderiaId,
                Nombre = mercaderia.Nombre,
                Tipo = new TipoMercaderiaResponse
                {
                    Id = mercaderia.TipoMercaderiaId,
                    Descripcion = mercaderia.TipoMercaderia.Descripcion,
                },
                Precio = mercaderia.Precio,
                Ingredientes = mercaderia.Ingredientes,
                Preparacion = mercaderia.Preparacion,
                Imagen = mercaderia.Imagen
            };
        }
        public MercaderiaResponse UpdateMercaderia(int mercaderiaId, MercaderiaRequest mercaderiaRequest)
        {
            var mercaderia = _querys.GetMercaderiabyId(mercaderiaId);
            if (mercaderia == null) { throw new ValorInvalidoException("El Id ingresado no corresponde a ninguna mercaderia registrada"); }

            bool ExisteNombre = _querys.GetAll().Any(m => m.Nombre.ToUpper() == mercaderiaRequest.Nombre.ToUpper());
            if (ExisteNombre) { return null; };

            bool ExisteTipo = _tipoMercaderiaQuery.GetAll().Any(tm => tm.TipoMercaderiaId == mercaderiaRequest.Tipo);
            if (!ExisteTipo) { throw new ValorInvalidoException("El tipo de mercaderia ingresado no existe."); }

            

            var updateMercaderia =  _command.ActualizarMercaderia( mercaderiaId, mercaderiaRequest);

            return new MercaderiaResponse
            {
                Id = updateMercaderia.MercaderiaId,
                Nombre = updateMercaderia.Nombre,
                Tipo = new TipoMercaderiaResponse
                {
                    Id = updateMercaderia.TipoMercaderiaId,
                    Descripcion = _tipoMercaderiaQuery.GetTipoMercaderiabyId(updateMercaderia.TipoMercaderiaId).Descripcion
                },
                Precio = updateMercaderia.Precio,
                Ingredientes = updateMercaderia.Ingredientes,
                Preparacion = updateMercaderia.Preparacion,
                Imagen = updateMercaderia.Imagen
            };
        }
        public List<MercaderiaGetResponse> GetMercaderiaInOrden( int tipoMercaderiaId, string nombre, string orden)
        {
            if (orden.ToUpper() != "ASC" && orden.ToUpper() != "DESC" && orden != null) { throw new ValorInvalidoException("El orden ingresado es incorrecto. Ingrese 'ASC' si desea orden ASCENDENTE o ingrese 'DESC' si desea orden DESCENDENTE");}

            List<MercaderiaGetResponse> mercaderias = new List<MercaderiaGetResponse>(); 
            var Mercaderias = _querys.GetMercaderiaInOrden(tipoMercaderiaId, nombre, orden);
            foreach (var mercaderia in Mercaderias)
            {
                var mercaderiaResponse = new MercaderiaGetResponse
                {
                    Id = mercaderia.MercaderiaId,
                    Nombre = mercaderia.Nombre,
                    Precio = mercaderia.Precio,
                    Tipo = new TipoMercaderiaResponse
                    {
                        Id = mercaderia.TipoMercaderiaId,
                        Descripcion = mercaderia.TipoMercaderia.Descripcion,
                    },
                    Imagen = mercaderia.Imagen
                };

                mercaderias.Add(mercaderiaResponse);
            }
            if (tipoMercaderiaId != 0)
            {
                bool ExisteTipo = _tipoMercaderiaQuery.GetAll().Any(tm => tm.TipoMercaderiaId == tipoMercaderiaId);
                if (!ExisteTipo) { return mercaderias; }
            }

            if (nombre != null)
            {
                bool ExisteNombre = _querys.GetAll().Any(m => m.Nombre.ToUpper().Contains(nombre.ToUpper()));
                if (!ExisteNombre) { return mercaderias; }

            }

            return mercaderias;
        }
    }

}

