namespace Green_Stuff.Models.ViewModels
{
    public class SalesAnalisysVM
    {
        public int IdSale { get; set; }
        public int Iduser
        {
            get; set;
        }

        public string Username { get; set; }
        public int Idcard { get; set; }
        public string P_Card { get; set; }
        public decimal TotalAmmount { get; set; }
        public string Cdate {  get; set; }
        public decimal TotalQuantity { get; set; }
    }
}
