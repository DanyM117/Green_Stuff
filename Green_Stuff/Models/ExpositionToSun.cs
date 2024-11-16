using System;
using System.Collections.Generic;

namespace Green_Stuff.Models;

public partial class ExpositionToSun
{
    public int IdSun { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime ModificationDate { get; set; }

    public bool Enabled { get; set; }

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
