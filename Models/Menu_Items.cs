using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Models
{
    public class Menu_Items
    {
        public int ID { get; set; }
        public int PriceID { get; set; }
        public int MenuTypeID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
