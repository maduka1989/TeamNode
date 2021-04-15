using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Models
{
    public class Tax
    {
        public int ID { get; set; }
        public string TaxName { get; set; }
        public double Percentage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsAvailable { get; set; }
    }
}
