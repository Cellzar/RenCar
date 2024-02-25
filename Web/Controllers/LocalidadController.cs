using Application.Common.Interfaces.Repository;
using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class LocalidadController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    public LocalidadController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Obtiene todos las localidades.
    /// </summary>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet]
    public async Task<ActionResult<RespuestaDto>> Get()
    {
        var respuesta = new RespuestaDto();

        try
        {
            var datos = await _unitOfWork.LocalidadRepository.GetAll();

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
    /// Obtiene una localidad por su ID.
    /// </summary>
    /// <param name="id">El ID de la localidad a obtener.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RespuestaDto>> GetById(int id)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var localidad = await _unitOfWork.LocalidadRepository.GetById(id);

            if (localidad == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Localidad con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Localidad encontrado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = localidad;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al obtener la localidad con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }


    /// <summary>
    /// Crea una nueva localidad.
    /// </summary>
    /// <param name="localidad">La localidad a crear.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpPost]
    public async Task<ActionResult<RespuestaDto>> Create([FromBody] Localidade localidad)
    {
        var respuesta = new RespuestaDto();

        try
        {

            await _unitOfWork.LocalidadRepository.Add(localidad);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Localidad creado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = localidad;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = "Ha ocurrido un error al crear la localidad : " + ex.Message;
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }

    /// <summary>
    /// Actualiza una localidad existente.
    /// </summary>
    /// <param name="id">El ID de la localidad a actualizar.</param>
    /// <param name="localidad">El objeto Usuario con los datos actualizados.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<RespuestaDto>> Update(int id, [FromBody] Localidade localidad)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var localidadExistente = await _unitOfWork.LocalidadRepository.GetById(id);

            if (localidadExistente == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Localidad con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            localidadExistente.Nombre = localidad.Nombre;

            _unitOfWork.LocalidadRepository.Update(localidadExistente);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = $"Localidad con ID {id} actualizado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = localidadExistente;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al actualizar la localidad con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }

    /// <summary>
    /// Elimina una localidad existente.
    /// </summary>
    /// <param name="id">El ID de la localidad a eliminar.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<RespuestaDto>> Delete(int id)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var usuarioExistente = await _unitOfWork.LocalidadRepository.GetById(id);

            if (usuarioExistente == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Localidad con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            _unitOfWork.LocalidadRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = $"Localidad con ID {id} eliminado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = null;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al eliminar la localidad con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }
}
