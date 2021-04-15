using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int TableID { get; set; }
        public int TaxID { get; set; }
        public double GrossPrice { get; set; }
        public double NetPrice { get; set; }
        public double TaxAmount { get; set; }
        public int OrderTypeID { get; set; }
        public string OrderPrepTimer { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
