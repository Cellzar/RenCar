using Application.Common.Interfaces.Repository;
using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class MercadoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    public MercadoController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Obtiene todos los mercados.
    /// </summary>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet]
    public async Task<ActionResult<RespuestaDto>> Get()
    {
        var respuesta = new RespuestaDto();

        try
        {
            var datos = await _unitOfWork.MercadoRepository.GetAll();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Datos obtenidos correctamente";
            respuesta.Ok = true;
            respuesta.Datos = datos;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = "Ha ocurrido un error al obtener los datos" + ex.Message;
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }

    /// <summary>
    /// Obtiene un mercado por su ID.
    /// </summary>
    /// <param name="id">El ID del usuario a obtener.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RespuestaDto>> GetById(int id)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var mercado = await _unitOfWork.MercadoRepository.GetById(id);

            if (mercado == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Mercado con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Mercado encontrado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = mercado;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al obtener el mercado con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }


    /// <summary>
    /// Crea un nuevo mercado.
    /// </summary>
    /// <param name="usuario">El Mercado a crear.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpPost]
    public async Task<ActionResult<RespuestaDto>> Create([FromBody] Mercado mercado)
    {
        var respuesta = new RespuestaDto();

        try
        {

            await _unitOfWork.MercadoRepository.Add(mercado);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Mercado creado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = mercado;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = "Ha ocurrido un error al crear el mercado: " + ex.Message;
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }

    /// <summary>
    /// Actualiza un mercado existente.
    /// </summary>
    /// <param name="id">El ID del mercado a actualizar.</param>
    /// <param name="mercado">El objeto Mercado con los datos actualizados.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<RespuestaDto>> Update(int id, [FromBody] Mercado mercado)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var mercadoExistente = await _unitOfWork.MercadoRepository.GetById(id);

            if (mercadoExistente == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Mercado con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            mercadoExistente.Nombre = mercado.Nombre;

            _unitOfWork.MercadoRepository.Update(mercadoExistente);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = $"Mercado con ID {id} actualizado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = mercadoExistente;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al actualizar el mercado con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }

    /// <summary>
    /// Elimina un mercado existente.
    /// </summary>
    /// <param name="id">El ID del mercado a eliminar.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<RespuestaDto>> Delete(int id)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var mercadoExistente = await _unitOfWork.MercadoRepository.GetById(id);

            if (mercadoExistente == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Mercado con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            _unitOfWork.MercadoRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = $"Mercado con ID {id} eliminado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = null;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al eliminar el mercado con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }
}
