using System.Collections.Generic;
using Green_Stuff.Models;

namespace Green_Stuff.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public List<PaymentCard> PaymentCards { get; set; }
    }
}
