using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bybit.net.api.Models.RFQ
{
    public class RfqLeg
    {
        public string? category { get; set; }  // spot | linear | option
        public string? symbol { get; set; }    // contract name
        public string? side { get; set; }      // Buy | Sell
        public string? qty { get; set; }       // quantity
        public string? price { get; set; }     // quote price
    }
}
