using BuyAndSell.Data.Dat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Info.Entities
{
    public class Customer : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Age { get; set; }
        public List<Product> Products { get; set; }
    }
}
