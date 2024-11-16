using System;
using System.Collections.Generic;

namespace Green_Stuff.Models;

public partial class Address
{
    public int IdAdd { get; set; }

    public int Iduser { get; set; }

    public string Name { get; set; } = null!;

    public DateTime ModificationDate { get; set; }

    public bool Enabled { get; set; }

    public virtual User IduserNavigation { get; set; } = null!;
}
