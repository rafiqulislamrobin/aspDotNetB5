using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace assignment_2
{
   public class student : IData
   {

        public int Id { get; set; }
        public string name { get; set; }
        public int amount { get; set; }
    }
}
