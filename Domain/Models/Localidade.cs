using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Localidade : BaseEntity
{
    public string Nombre { get; set; } = null!;
}
