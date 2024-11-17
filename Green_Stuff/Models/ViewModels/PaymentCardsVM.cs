using Microsoft.AspNetCore.Mvc.Rendering;

namespace Green_Stuff.Models.ViewModels
{
    public class PaymentCardsVM
    {
        public PaymentCard oPaymentCards { get; set; }
        public List<SelectListItem> oPaymentUsersList { get; set; }
    }
}
