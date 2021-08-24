using BuyAndSell.Data.Dat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Info.Entities
{
    public class Product : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Qunatity { get; set; }

        public Customer Customer { get; set; }

    }
}
