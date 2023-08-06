using Application.Exceptions;
using Application.Interfaces.IComanda;
using Application.Interfaces.IComandaMercaderia;
using Application.Interfaces.IFormaEntrega;
using Application.Interfaces.IMercaderia;
using Application.Interfaces.ITipoMercaderia;
using Application.Request;
using Application.Response;
using Domain;
using Microsoft.EntityFrameworkCore.Storage;
using System.Globalization;

namespace Application.UseCase
{
    public class ComandaService : IComandaService
    {
        private readonly IComandaCommand _command;
        private readonly IComandaQuery _querys;
        private readonly IMercaderiaQuery _mercaderiaQuery;
        private readonly IFormaEntregaQuery _formaEntregaQuery;
        private readonly ITipoMercaderiaQuery _tipoMercaderiaQuery;
        private readonly IComandaMercaderiaService _comandaMercaderiaService;

        public ComandaService(IComandaCommand command, IComandaQuery querys, IMercaderiaQuery mercaderiaQuery, IFormaEntregaQuery formaEntregaQuery, ITipoMercaderiaQuery tipoMercaderiaQuery, IComandaMercaderiaService comandaMercaderiaService)
        {
            _command = command;
            _querys = querys;
            _mercaderiaQuery = mercaderiaQuery;
            _formaEntregaQuery = formaEntregaQuery;
            _tipoMercaderiaQuery = tipoMercaderiaQuery;
            _comandaMercaderiaService = comandaMercaderiaService;
        }

        public ComandaResponse CreateComanda(ComandaRequest comandaRequest) {
            
            var precio = 0;
            var listaMercaderia = comandaRequest.Mercaderias;
            

            foreach (var mercaderiaId in listaMercaderia)
            {   
                var mercaderia = _mercaderiaQuery.GetMercaderiabyId(mercaderiaId);
                if (mercaderia == null) 
                {
                    throw new ValorInvalidoException("La mercadería con ID " + mercaderiaId + " no existe en la base de datos."); 
                };
            }

            bool ValidarFormaEntrega = _formaEntregaQuery.GetAll().Any(c => c.FormaEntregaId == comandaRequest.FormaEntrega);
            if(!ValidarFormaEntrega) { throw new FormaEntregaBadRequestException(); }


            List<Mercaderia> listaDeMercaderias = new List<Mercaderia>();
            List<MercaderiaComandaResponse> listaMercaderiaComandaResponse = new List<MercaderiaComandaResponse> ();

            foreach (var mercaderias in listaMercaderia)
            {
                
                var mercaderia = _mercaderiaQuery.GetMercaderiabyId(mercaderias);
                precio = mercaderia.Precio + precio;
                listaDeMercaderias.Add(mercaderia);
            }
            
            var comanda = new Comanda
            {
                Fecha = DateTime.Now,
                FormaEntregaId = comandaRequest.FormaEntrega,
                PrecioTotal = precio,
                FormaEntrega = _formaEntregaQuery.GetFormaEntregabyId(comandaRequest.FormaEntrega)
            };

            _command.InsertComanda(comanda);
            foreach (var recorrerMercaderia in listaDeMercaderias) {

                _comandaMercaderiaService.CreateComandaMercaderia(recorrerMercaderia, comanda);
                var mercaderiaComandaResponse = new MercaderiaComandaResponse
                {
                    Id = recorrerMercaderia.MercaderiaId,
                    Nombre = recorrerMercaderia.Nombre,
                    Precio = recorrerMercaderia.Precio
                };
                listaMercaderiaComandaResponse.Add(mercaderiaComandaResponse);
            }
            return new ComandaResponse
            {
                Id = comanda.ComandaId,
                Mercaderias = listaMercaderiaComandaResponse,
                FormaEntrega = new FormaEntregaResponse
                {
                    Id = comanda.FormaEntregaId,
                    Descripcion = _formaEntregaQuery.GetFormaEntregabyId(comanda.FormaEntregaId).Descripcion
                },
                Fecha = comanda.Fecha.ToString(),
                Total = (double)comanda.PrecioTotal
            };

        }

        public void UpdateComanda(Comanda comanda)
        {
            _command.ActualizarComanda(comanda);
        }

        public void DeleteComanda(Guid comandaId)
        {
            _command.DeleteComanda(comandaId);
        }

        public List<Comanda> GetAllComanda()
        {
            return _querys.GetAll();
        }

        public ComandaGetResponse GetComandaById(Guid comandaId)
        {
            List<MercaderiaGetResponse> ListamercaderiaGetResponse = new List<MercaderiaGetResponse>();
            var comanda = _querys.GetComandabyId(comandaId);

            if (comanda == null) { return null; }

            foreach(var mercaderia in comanda.ComandaMercaderia)
            {
                var mercaderiaQuery = _mercaderiaQuery.GetMercaderiabyId(mercaderia.MercaderiaId);
                var mercaderiaGetResponse = new MercaderiaGetResponse
                {
                    Id = mercaderia.MercaderiaId,
                    Nombre = mercaderiaQuery.Nombre,
                    Precio = mercaderiaQuery.Precio,
                    Tipo = new TipoMercaderiaResponse
                    {
                        Id = mercaderiaQuery.TipoMercaderiaId,
                        Descripcion = _tipoMercaderiaQuery.GetTipoMercaderiabyId(mercaderiaQuery.TipoMercaderiaId).Descripcion
                    },
                    Imagen = mercaderiaQuery.Imagen
                };
                ListamercaderiaGetResponse.Add(mercaderiaGetResponse);
            }
            return new ComandaGetResponse
            {
                Id = comanda.ComandaId,
                Mercaderias = ListamercaderiaGetResponse,
                FormaEntrega = new FormaEntregaResponse
                {
                    Id = comanda.FormaEntregaId,
                    Descripcion = _formaEntregaQuery.GetFormaEntregabyId(comanda.FormaEntregaId).Descripcion
                },
                Total = (double)comanda.PrecioTotal,
                Fecha = comanda.Fecha.ToString()
            };
        }

        public List<ComandaResponse> GetComandaByFecha(string fecha)
        {
            DateTime fechaConvertida;
            if (!string.IsNullOrEmpty(fecha))
            {
                if (!DateTime.TryParse(fecha, out fechaConvertida) && (!string.IsNullOrEmpty(fecha)))
                {
                    return null;
                }
                fecha = fechaConvertida.ToString("d/M/yyyy");
            }
            
            var ComandaFecha = _querys.GetComandabyFecha(fecha);
            List<ComandaResponse> ListacomandaResponse = new List<ComandaResponse>();
           
            foreach (var comandas in ComandaFecha)
            {
                List<MercaderiaComandaResponse> ListaMercaderiaComandaResponse = new List<MercaderiaComandaResponse>();
                foreach (var mercaderias in comandas.ComandaMercaderia)
                {
                    var mercaderiaComandaResponse = new MercaderiaComandaResponse
                    {
                        Id = mercaderias.MercaderiaId,
                        Nombre = _mercaderiaQuery.GetMercaderiabyId(mercaderias.MercaderiaId).Nombre,
                        Precio = (double)_mercaderiaQuery.GetMercaderiabyId(mercaderias.MercaderiaId).Precio
                    };
                    ListaMercaderiaComandaResponse.Add(mercaderiaComandaResponse);
                }
                var comandaResponse = new ComandaResponse
                {
                    Id = comandas.ComandaId,
                    Mercaderias = ListaMercaderiaComandaResponse,
                    FormaEntrega = new FormaEntregaResponse
                    {
                        Id = comandas.FormaEntregaId,
                        Descripcion = _formaEntregaQuery.GetFormaEntregabyId(comandas.FormaEntregaId).Descripcion
                    },
                    Total = (double)comandas.PrecioTotal,
                    Fecha = comandas.Fecha.ToString()
                };
                ListacomandaResponse.Add(comandaResponse);
            }
            return ListacomandaResponse;
        }
    }
}
