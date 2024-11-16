using System;
using System.Collections.Generic;

namespace Green_Stuff.Models;

public partial class Warehouse
{
    public int IdItem { get; set; }

    public int Idsize { get; set; }

    public int? Idsun { get; set; }

    public int? Idwater { get; set; }

    public int Idcategory { get; set; }

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public string? Description { get; set; }

    public string? ImagePath { get; set; }

    public virtual Category oCategories { get; set; } = null!;

    public virtual Size oSizes { get; set; } = null!;

    public virtual ExpositionToSun? oExpositionToSun { get; set; }

    public virtual WaterDemmand? oWaterDemmand { get; set; }

    public virtual ICollection<SaleDetail> oSaleDetails { get; set; } = new List<SaleDetail>();
}
