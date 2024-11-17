using System;
using System.Collections.Generic;

namespace Green_Stuff.Models;

public partial class SaleDetail
{
    public int Idsale { get; set; }

    public int Iditem { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal Subtotal { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual Warehouse oWarehouses { get; set; } = null!;

    public virtual Sale oSales { get; set; } = null!;
}
