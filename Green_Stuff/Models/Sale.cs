using System;
using System.Collections.Generic;

namespace Green_Stuff.Models;

public partial class Sale
{
    public int IdSale { get; set; }
    public string Username { get; set; }
    public int Iduser { get; set; }

    public int Idcard { get; set; }
    public string P_Card { get; set; }
    public decimal TotalAmmount { get; set; }

    public DateTime CreationDate { get; set; }
    public string Cdate {  get; set; }

    public decimal TotalQuantity { get; set; }

    public virtual PaymentCard oCards { get; set; } = null!;

    public virtual User oUsers { get; set; } = null!;

    public virtual ICollection<SaleDetail> oSaleDetails { get; set; } = new List<SaleDetail>();
}
