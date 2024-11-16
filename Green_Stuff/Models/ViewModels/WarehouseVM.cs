using Microsoft.AspNetCore.Mvc.Rendering;

namespace Green_Stuff.Models.ViewModels
{
    public class WarehouseVM
    {
        public Warehouse oWarehouse { get; set; }
        public List<SelectListItem> oSizesList { get; set; }
        public List<SelectListItem> oExpositionToSunList { get; set; }
        public List<SelectListItem> oWaterDemmandList { get; set; }
        public List<SelectListItem> oCategoriesList { get; set; }
    }
}
