using Microsoft.AspNetCore.Mvc.Rendering;

namespace Green_Stuff.Models.ViewModels
{
    public class UsersVM
    {
        public User oUser {  get; set; }
        public List<SelectListItem> oUserTypeList { get; set; }
    }
}
