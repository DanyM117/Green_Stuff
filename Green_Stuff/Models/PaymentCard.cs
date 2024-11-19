using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Green_Stuff.Models
{
    public partial class PaymentCard
    {
        public int IdCard { get; set; }

        public int Iduser { get; set; }

        [Required]
        [CreditCard]
        [Display(Name = "Número de Tarjeta")]
        public long Number { get; set; }

        [Required]
        [Display(Name = "Nombre en la Tarjeta")]
        public string NameOnCard { get; set; } = null!;

        [Required]
        [Range(100, 9999)]
        [Display(Name = "CVV")]
        public short Cvv { get; set; }

        public virtual User oUsers { get; set; } = null!;

        public virtual ICollection<Sale> oSales { get; set; } = new List<Sale>();
    }
}
