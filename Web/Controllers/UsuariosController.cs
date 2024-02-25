﻿using Application.Common.Interfaces.Repository;
using Domain.DTO;
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
}