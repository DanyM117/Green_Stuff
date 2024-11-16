﻿using System;
using System.Collections.Generic;

namespace Green_Stuff.Models;

public partial class Sale
{
    public int IdSale { get; set; }

    public int Iduser { get; set; }

    public int Idcard { get; set; }

    public decimal TotalAmmount { get; set; }

    public DateTime CreationDate { get; set; }

    public decimal TotalQuantity { get; set; }

    public virtual PaymentCard IdcardNavigation { get; set; } = null!;

    public virtual User IduserNavigation { get; set; } = null!;

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}