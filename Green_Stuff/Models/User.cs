using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Green_Stuff.Models;

public partial class User
{
    public int IdUser { get; set; }

    public int IduserType { get; set; }

    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string Password { get; set; } = null!;

    public string? ImagePath { get; set; }

    [ValidateNever]
    public virtual ICollection<Address>? oAddresses { get; set; }

    [ValidateNever]
    public virtual ICollection<Advertisement>? oAdvertisements { get; set; }

    [ValidateNever]
    public virtual UserType? oUserTypes { get; set; }

    [ValidateNever]
    public virtual ICollection<PaymentCard>? oPaymentCards { get; set; }

    [ValidateNever]
    public virtual ICollection<Sale>? oSales { get; set; }
}
