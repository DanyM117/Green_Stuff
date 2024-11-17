using System;
using System.Collections.Generic;

namespace Green_Stuff.Models;

public partial class PaymentCard
{
    public int IdCard { get; set; }

    public int Iduser { get; set; }

    public long Number { get; set; }

    public string NameOnCard { get; set; } = null!;

    public short Cvv { get; set; }

    public virtual User oUsers { get; set; } = null!;

    public virtual ICollection<Sale> oSales { get; set; } = new List<Sale>();
}
