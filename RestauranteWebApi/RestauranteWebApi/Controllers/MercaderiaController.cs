using Application.Exceptions;
using Application.Interfaces.IMercaderia;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteWebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MercaderiaController : ControllerBase
    {
        private readonly IMercaderiaService _mercaderiaService;

        public MercaderiaController(IMercaderiaService mercaderiaService)
        {
            _mercaderiaService = mercaderiaService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(MercaderiaGetResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public IActionResult GetMercaderia(int tipo, string? nombre, string? orden = "ASC")
        {
            try
            {
                var result = _mercaderiaService.GetMercaderiaInOrden(tipo, nombre, orden);
                return new JsonResult(result);
            }
            catch (ValorInvalidoException valorInvalido)
            {
                return BadRequest(new { message = valorInvalido.Message });
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(MercaderiaResponse), 201)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        [ProducesResponseType(typeof(BadRequest), 409)]
        
        public IActionResult CreateMercaderia(MercaderiaRequest request)
        {
            try
            {
                var result = _mercaderiaService.CreateMercaderia(request);
                if (result == null)
                {
                    return Conflict(new { message = "No se puede crear una Mercaderia con un nombre ya existente." });
                }
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (ValorInvalidoException valorInvalido)
            {
                return NotFound(new { valorInvalido.Message });
            }

        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MercaderiaResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult GetById(int id)
        {
            var result = _mercaderiaService.GetMercaderiaById(id);
            if (result == null)
            {

                return NotFound(new { message = "No se encuentra la mercaderia en la base de datos." });
            }
            return new JsonResult(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MercaderiaResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        [ProducesResponseType(typeof(BadRequest), 409)]
        public IActionResult UpdateMercaderia(int id, MercaderiaRequest request)
        {
            try
            {
                var result = _mercaderiaService.UpdateMercaderia(id, request);
                if (result == null)
                {
                    return Conflict(new { message = "El nombre ingresado ya se encuentra en la base de datos" });
                }
                return new JsonResult(result);
            }
            catch(ValorInvalidoException valorInvalido)
            {
                return NotFound(new { valorInvalido.Message });
            }
            
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MercaderiaResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        [ProducesResponseType(typeof(BadRequest), 409)]
        public IActionResult DeleteMercaderia(int id)
        {
            try
            {
                var result = _mercaderiaService.DeleteMercaderia(id);
                if (result == null)
                {

                    return Conflict(new { message = "No se puede eliminar la Mercaderia por que se encuentra dentro de una comanda" });
                }
                return new JsonResult(result);
            }
            catch (ValorInvalidoException ValidarMercaderia) 
            {
                return NotFound(new { message = ValidarMercaderia.Message });
            }
            
        }
    }
}
