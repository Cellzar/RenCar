﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO;

public class RespuestaDto
{
    public string? Estado { get; set; }
    public string? Mensaje { get; set; }
    public bool Ok { get; set; }
    public object? Datos { get; set; }
}
