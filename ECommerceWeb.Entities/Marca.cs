﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Entities;

public class Marca : EntityBase
{
    [StringLength(100)]
    public string Nombre { get; set; } = default!;
}