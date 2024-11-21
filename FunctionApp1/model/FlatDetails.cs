using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1.model
{
    internal class FlatDetails
    {
        public int Id { get; set; }
        public float? Price { get; set; }
        public Address Address { get; set; }
        public string Currency { get; set; }
        public string Rooms { get; set; }
        public string Area { get; set; }
        public string Floor { get; set; } //parter 

    }
}
