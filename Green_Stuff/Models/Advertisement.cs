using System;
using System.Collections.Generic;

namespace Green_Stuff.Models;

public partial class Advertisement
{
    public int IdAdvertisement { get; set; }

    public int Iduser { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImagePath { get; set; }

    public virtual User IduserNavigation { get; set; } = null!;
}
