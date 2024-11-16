using System;
using System.Collections.Generic;

namespace Green_Stuff.Models;

public partial class UserType
{
    public int IdUserType { get; set; }

    public string Name { get; set; } = null!;

    public DateTime ModificationDate { get; set; }

    public bool Enabled { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
