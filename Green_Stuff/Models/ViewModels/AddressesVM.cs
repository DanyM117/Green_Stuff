using Microsoft.AspNetCore.Mvc.Rendering;

namespace Green_Stuff.Models.ViewModels
{
    public class AddressesVM
    {
        public Address oAddress { get; set; }
        public List<SelectListItem> oUserList { get; set; }
    }
}
