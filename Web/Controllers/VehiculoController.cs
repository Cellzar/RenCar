using Application.Common.Interfaces.Repository;
using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class VehiculoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    public VehiculoController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Obtiene todos los vehiculo.
    /// </summary>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet]
    public async Task<ActionResult<RespuestaDto>> Get()
    {
        var respuesta = new RespuestaDto();

        try
        {
            var datos = await _unitOfWork.VehiculoRepository.GetAll();

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
    /// Obtiene un vehiculo por su ID.
    /// </summary>
    /// <param name="id">El ID del vehiculo a obtener.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RespuestaDto>> GetById(int id)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var mercado = await _unitOfWork.VehiculoRepository.GetById(id);

            if (mercado == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Vehiculo con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Vehiculo encontrado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = mercado;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al obtener el vehiculo con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }

    /// <summary>
    /// Obtiene vehículos disponibles en base a los parámetros de búsqueda especificados.
    /// </summary>
    /// <param name="dto">DTO que contiene los parámetros de búsqueda.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet("ObtenerVehiculosDisponiblesPorLocalidades")]
    public async Task<ActionResult<RespuestaDto>> ObtenerVehiculosDisponiblesPorLocalidades([FromQuery] BusquedaVehiculosDto dto)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var vehiculosDisponibles = await _unitOfWork.VehiculoRepository.ObtenerVehiculosDisponiblesPorLocalidades(dto);

            return Ok(vehiculosDisponibles);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al obtener los vehículos disponibles: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }

    /// <summary>
    /// Obtiene vehículos disponibles en base a los parámetros de búsqueda especificados.
    /// </summary>
    /// <param name="dto">DTO que contiene los parámetros de búsqueda.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet("ObtenerVehiculosDisponiblesPorMercadoYLocalidad")]
    public async Task<ActionResult<RespuestaDto>> ObtenerVehiculosDisponiblesPorMercadoYLocalidad(int mercadoId, string localidadRecogida)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var vehiculosDisponibles = await _unitOfWork.VehiculoRepository.ObtenerVehiculosDisponiblesPorMercadoYLocalidad(mercadoId, localidadRecogida);

            return Ok(vehiculosDisponibles);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al obtener los vehículos disponibles: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }


    /// <summary>
    /// Obtiene vehículos disponibles en base a los parámetros de búsqueda especificados.
    /// </summary>
    /// <param name="dto">DTO que contiene los parámetros de búsqueda.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet("ObtenerVehiculosDisponiblesPorMercadoYLocalidadDevolucion")]
    public async Task<ActionResult<RespuestaDto>> ObtenerVehiculosDisponiblesPorMercadoYLocalidadDevolucion(int mercadoId, string localidadDevolucion)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var vehiculosDisponibles = await _unitOfWork.VehiculoRepository.ObtenerVehiculosDisponiblesPorMercadoYLocalidadDevolucion(mercadoId, localidadDevolucion);

            return Ok(vehiculosDisponibles);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al obtener los vehículos disponibles: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }



    /// <summary>
    /// Crea un nuevo Vehiculo.
    /// </summary>
    /// <param name="usuario">El Vehiculo a crear.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpPost]
    public async Task<ActionResult<RespuestaDto>> Create([FromBody] Vehiculo vehiculo)
    {
        var respuesta = new RespuestaDto();

        try
        {

            await _unitOfWork.VehiculoRepository.Add(vehiculo);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Vehiculo creado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = vehiculo;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = "Ha ocurrido un error al crear el vehiculo: " + ex.Message;
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }

    /// <summary>
    /// Actualiza un vehiculo existente.
    /// </summary>
    /// <param name="id">El ID del vehiculo a actualizar.</param>
    /// <param name="mercado">El objeto Vehiculo con los datos actualizados.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<RespuestaDto>> Update(int id, [FromBody] Vehiculo vehiculo)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var VehiculoExistente = await _unitOfWork.VehiculoRepository.GetById(id);

            if (VehiculoExistente == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Vehiculo con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            VehiculoExistente.Marca = vehiculo.Marca;
            VehiculoExistente.Modelo = vehiculo.Modelo;
            VehiculoExistente.Disponible = vehiculo.Disponible;

            _unitOfWork.VehiculoRepository.Update(VehiculoExistente);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = $"Vehiculo con ID {id} actualizado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = VehiculoExistente;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al actualizar el Vehiculo con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }

    /// <summary>
    /// Elimina un Vehiculo existente.
    /// </summary>
    /// <param name="id">El ID del Vehiculo a eliminar.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<RespuestaDto>> Delete(int id)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var VehiculoExistente = await _unitOfWork.VehiculoRepository.GetById(id);

            if (VehiculoExistente == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Vehiculo con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            _unitOfWork.VehiculoRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = $"Vehiculo con ID {id} eliminado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = null;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al eliminar el Vehiculo con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }


}
