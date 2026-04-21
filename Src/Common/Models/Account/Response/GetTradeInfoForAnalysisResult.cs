using Newtonsoft.Json;

namespace bybit.net.api.Models.Account.Response
{
    public class GetTradeInfoForAnalysisResult
    {
        [JsonProperty("symbolRnl")]
        public string? SymbolRnl { get; set; }

        [JsonProperty("netExecQty")]
        public string? NetExecQty { get; set; }

        [JsonProperty("sumExecValue")]
        public string? SumExecValue { get; set; }

        [JsonProperty("sumExecQty")]
        public string? SumExecQty { get; set; }

        [JsonProperty("avgBuyExecPrice")]
        public string? AvgBuyExecPrice { get; set; }

        [JsonProperty("sumBuyExecValue")]
        public string? SumBuyExecValue { get; set; }

        [JsonProperty("sumBuyExecQty")]
        public string? SumBuyExecQty { get; set; }

        [JsonProperty("sumBuyExecFee")]
        public string? SumBuyExecFee { get; set; }

        [JsonProperty("sumBuyOrderQty")]
        public string? SumBuyOrderQty { get; set; }

        [JsonProperty("avgSellExecPrice")]
        public string? AvgSellExecPrice { get; set; }

        [JsonProperty("sumSellExecValue")]
        public string? SumSellExecValue { get; set; }

        [JsonProperty("sumSellExecQty")]
        public string? SumSellExecQty { get; set; }

        [JsonProperty("sumSellExecFee")]
        public string? SumSellExecFee { get; set; }

        [JsonProperty("sumSellOrderQty")]
        public string? SumSellOrderQty { get; set; }

        [JsonProperty("maxMarginVersion")]
        public int? MaxMarginVersion { get; set; }

        [JsonProperty("baseCoin")]
        public string? BaseCoin { get; set; }

        [JsonProperty("settleCoin")]
        public string? SettleCoin { get; set; }

        [JsonProperty("sumPriceList")]
        public List<TradeInfoPriceSummary>? SumPriceList { get; set; }
    }

    public class TradeInfoPriceSummary
    {
        [JsonProperty("day")]
        public string? Day { get; set; }

        [JsonProperty("sumBuyExecValue")]
        public string? SumBuyExecValue { get; set; }

        [JsonProperty("sumSellExecValue")]
        public string? SumSellExecValue { get; set; }

        [JsonProperty("sumExecValue")]
        public string? SumExecValue { get; set; }
    }
}
