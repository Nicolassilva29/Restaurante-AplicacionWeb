using Application.Exceptions;
using Application.Interfaces.IComanda;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteWebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
       private readonly IComandaService _comandaService;

        public ComandaController(IComandaService comandaService)
        {
            _comandaService = comandaService;
        }
        [HttpGet]

        [ProducesResponseType(typeof(ComandaResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public IActionResult GetComandaByFecha(string? fecha)
        {
            var result = _comandaService.GetComandaByFecha(fecha);
            if (result == null)
            {
                return BadRequest(new { message = "Formato Invalido. Ingrese una fecha correcta" });
            }
            return new JsonResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ComandaResponse), 201)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public IActionResult CreateComanda(ComandaRequest comandaRequest)
        {
            try 
            {
                var result = _comandaService.CreateComanda(comandaRequest);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (ValorInvalidoException ValidarMercaderia) 
            {
                return BadRequest(new { message = ValidarMercaderia.Message });
            }


            catch (FormaEntregaBadRequestException) 
            {
                return BadRequest(new { message = "Forma de Entrega Invalida. Ingrese un valor del 1 al 3" });
            }
            
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ComandaGetResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult GetComandaById(Guid id) 
        {
            var result = _comandaService.GetComandaById(id);
            if (result == null)
            {
                return NotFound(new {message = "No se encontró ninguna comanda con ese Guid"});
            }
            return new JsonResult(result);
        }
    }
}
