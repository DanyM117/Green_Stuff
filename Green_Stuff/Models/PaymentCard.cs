using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Green_Stuff.Models
{
    public partial class PaymentCard
    {
        [Key]
        public int IdCard { get; set; }

        [Required]
        [Display(Name = "ID de Usuario")]
        public int Iduser { get; set; }

        [Required]
        [Display(Name = "Nombre en la Tarjeta")]
        public string NameOnCard { get; set; }

        [Required]
        [Display(Name = "Número de Tarjeta")]
        [StringLength(16, MinimumLength = 13, ErrorMessage = "El número de tarjeta debe tener entre 13 y 16 dígitos.")]
        [RegularExpression(@"^\d{13,16}$", ErrorMessage = "El número de tarjeta debe contener solo dígitos.")]
        public string Number { get; set; }

        [Required]
        [Range(100, 9999, ErrorMessage = "El CVV es inválido.")]
        [Display(Name = "CVV")]
        public short Cvv { get; set; }

        public virtual User? oUsers { get; set; }

        public virtual ICollection<Sale> oSales { get; set; } = new List<Sale>();
    }
}
