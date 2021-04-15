using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Models
{
    public class OrderedItems
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int PriceID { get; set; }
        public double Quantity { get; set; }
        public string CustomerComment { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int FoodID { get; set; }

    }
}
