namespace Client.Models
{
    class CostsMonthlyModel
    {
        public string Month { get; set; }
        public int Ammount { get; set; }
        public decimal Costs { get; set; }

        public string CostDisplay { get; set; }
    }
}
