using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Models
{
    public class Price
    {
        public int ID { get; set; }
        public int PortionID { get; set; }
        public int FoodID { get; set; }
        public double ActualPrice { get; set; }
        public double SellingPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
