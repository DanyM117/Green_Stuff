using Green_Stuff.Models;

namespace Green_Stuff.Models.ViewModels
{
    public class CartItem
    {
        public Warehouse Product { get; set; }
        public int Quantity { get; set; }
    }
}
