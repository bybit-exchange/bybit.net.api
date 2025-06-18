using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bybit.net.api.Models.Market
{
    public class MarketTickerResult
    {
        [JsonProperty("category")]
        public string? Category { get; set; }

        [JsonProperty("list")]
        public List<TickerInfoEntry>? MarketTickerInfoEntries { get; set; }
    }
}
