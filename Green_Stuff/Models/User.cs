using System;
using System.Collections.Generic;

namespace Green_Stuff.Models;

public partial class User
{
    public int IdUser { get; set; }

    public int IduserType { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
    //public string ConfirmarPassword { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<Address> oAddresses { get; set; } = new List<Address>();

    public virtual ICollection<Advertisement> oAdvertisements { get; set; } = new List<Advertisement>();

    public virtual UserType oUserTypes { get; set; } = null!;

    public virtual ICollection<PaymentCard> oPaymentCards { get; set; } = new List<PaymentCard>();

    public virtual ICollection<Sale> oSales { get; set; } = new List<Sale>();
}
