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

    public string? ImagePath { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();

    public virtual UserType IduserTypeNavigation { get; set; } = null!;

    public virtual ICollection<PaymentCard> PaymentCards { get; set; } = new List<PaymentCard>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
