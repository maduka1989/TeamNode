using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Models
{
    public class Ingredient_Qty_Per_Dish
    {
        public int ID { get; set; }
        public int DishID { get; set; }
        public int PortionID { get; set; }
        public int RawMaterialID { get; set; }
        public double Qty { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
