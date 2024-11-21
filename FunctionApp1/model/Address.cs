using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FunctionApp1.model
{
    internal class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Estate { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
    }
    
}
