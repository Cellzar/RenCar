using Application.Common.Interfaces.Repository;
using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class UsuariosController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    public UsuariosController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Obtiene todos los usuarios.
    /// </summary>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet]
    public async Task<ActionResult<RespuestaDto>> Get()
    {
        var respuesta = new RespuestaDto();

        try
        {
            var datos = await _unitOfWork.UsuarioRepository.GetAll();

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
    /// Obtiene un usuario por su ID.
    /// </summary>
    /// <param name="id">El ID del usuario a obtener.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RespuestaDto>> GetById(int id)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetById(id);

            if (usuario == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Usuario con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Usuario encontrado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = usuario;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al obtener el usuario con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }


    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    /// <param name="usuario">El usuario a crear.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpPost]
    public async Task<ActionResult<RespuestaDto>> Create([FromBody] Usuario usuario)
    {
        var respuesta = new RespuestaDto();

        try
        {
            // Validar el usuario antes de agregarlo

            await _unitOfWork.UsuarioRepository.Add(usuario);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Usuario creado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = usuario;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = "Ha ocurrido un error al crear el usuario: " + ex.Message;
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }

    /// <summary>
    /// Actualiza un usuario existente.
    /// </summary>
    /// <param name="id">El ID del usuario a actualizar.</param>
    /// <param name="usuario">El objeto Usuario con los datos actualizados.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<RespuestaDto>> Update(int id, [FromBody] Usuario usuario)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var usuarioExistente = await _unitOfWork.UsuarioRepository.GetById(id);

            if (usuarioExistente == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Usuario con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Apellidos = usuario.Apellidos;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Ciudad = usuario.Ciudad;
            usuarioExistente.CodigoPostal = usuario.CodigoPostal;
            usuarioExistente.Direccion = usuario.Direccion;

            _unitOfWork.UsuarioRepository.Update(usuarioExistente);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = $"Usuario con ID {id} actualizado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = usuarioExistente;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al actualizar el usuario con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }

    /// <summary>
    /// Elimina un usuario existente.
    /// </summary>
    /// <param name="id">El ID del usuario a eliminar.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<RespuestaDto>> Delete(int id)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var usuarioExistente = await _unitOfWork.UsuarioRepository.GetById(id);

            if (usuarioExistente == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Usuario con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            _unitOfWork.UsuarioRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = $"Usuario con ID {id} eliminado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = null;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al eliminar el usuario con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }

}