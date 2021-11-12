using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class InventoryRecieved
    {
        [Key]
        public int In_RecievedID { get; set; }
        public string DateRecieved { get; set; }
        public string Vendor { get; set; }
        public string Invoice_Number { get; set; }
        public string Location { get; set; }
        public string Inventory_ItemName { get; set; }
        public int Quantity { get; set; }
        public string PurchaseCost { get; set; }
        public int VendorItemNumber { get; set; }
        public int SerialLotNumber { get; set; }
        public string ExpirationDate { get; set; }
    }
}
