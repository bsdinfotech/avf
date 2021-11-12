using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class AddInventory
    {
        
        public int Inventory_Id { get; set; }
        public string Inventory_ItemName { get; set; }
        public string Inventory_type { get; set; }
        public string CrossReference { get; set; }
        public string ReorderPoint { get; set; }
        public string SalesPrice { get; set; }
        public string DistributionUnit { get; set; }
        public string DateRecieved { get; set; }
        public string Invoice_Number { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public string PurchaseCost { get; set; }
        public int SerialNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string Vendor { get; set; }
        public int VendorItemNumber { get; set; }
        public string VendorLocation { get; set; }

    }
}
