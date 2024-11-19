using Microsoft.AspNetCore.Mvc.Rendering;

namespace Green_Stuff.Models.ViewModels
{
    public class AdvertisementsVM
    {
        public Advertisement oAd {  get; set; }
        public List<SelectListItem> oadslist { get; set; }
        public string Username { get; set; }
    }
}
