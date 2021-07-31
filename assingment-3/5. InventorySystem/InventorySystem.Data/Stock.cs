using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Data
{
    public class Stock
    {
        public int Id { get; set; }
        public int Qunatity { get; set; }
        public Product productId { get; set; }


    }
}
