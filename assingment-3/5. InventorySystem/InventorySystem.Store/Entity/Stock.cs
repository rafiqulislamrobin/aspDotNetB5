using InventorySystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Store.Entity
{
    public class Stock : IEntity<int>
    {
        public int Id { get; set; }
        public int Qunatity { get; set; }
        public int productId { get; set; }

        public Product Product { get; set; }
    }
}
