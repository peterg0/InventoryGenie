﻿namespace InventoryGenie.Models
{
    public class SaleRecord
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public int QuantityExchanged { get; set; }
        public double WholesalePrice { get; set; }
        public double ShelfPrice { get; set; }
        public int ProductID { get; set; }

    }
}
