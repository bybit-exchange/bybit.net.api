using bybit.net.api;
using bybit.net.api.ApiServiceImp;
using bybit.net.api.Models;
using bybit.net.api.Models.Trade;
using bybit.net.api.Models.Trade.Response;
using Newtonsoft.Json;
using Xunit;

namespace bybit.api.test
{
    public class TradeServiceTest
    {
        readonly BybitTradeService TradeService = new(apiKey: "X6wmWloIPvaLXAKqv2", apiSecret: "rY1CWGYLHy0AUjdNZqqspvd3Krhp79fHp1sP", url: BybitConstants.HTTP_TESTNET_URL, debugMode: true);
        #region Trade History
        [Fact]
        public async Task Check_GetTradeHistory()
        {
            var tradeInfoString = await TradeService.GetTradeHistory(category: Category.LINEAR, symbol: "GASUSDT");
            await Console.Out.WriteLineAsync(tradeInfoString);
        }
        #endregion

        #region inverse oder
        [Fact]
        public async Task Check_PlaceInverseOrderByDict()
        {
            var orderInfoString = await TradeService.PlaceOrder(category: Category.LINEAR, symbol: "GASUSDT", side: Side.BUY, orderType: OrderType.LIMIT, qty: "10", price: "10", timeInForce: TimeInForce.GTC, positionIdx: 1);
            if (!string.IsNullOrEmpty(orderInfoString))
            {
                Console.WriteLine(orderInfoString);
                OrderResult? orderInfo = JsonConvert.DeserializeObject<OrderResult>(orderInfoString);
                Assert.Equal(0, orderInfo?.RetCode);
                Assert.Equal("OK", orderInfo?.RetMsg);
                Assert.NotNull(orderInfo?.Result?.OrderId);
            }
        }
        #endregion

        #region Batch Order
        [Fact]
        public async Task Check_PlaceBatchOrderByDict()
        {
            Dictionary<string, object> dict1 = new() { { "category", "linear" }, { "symbol", "XRPUSDT" }, { "orderType", "Limit" }, { "side", "Buy" }, { "qty", "10" }, { "price", "0.6080" }, { "timeInForce", "GTC" }, { "positionIdx", 1 } };
            Dictionary<string, object> dict2 = new() { { "category", "linear" }, { "symbol", "BLZUSDT" }, { "orderType", "Limit" }, { "side", "Buy" }, { "qty", "10" }, { "price", "0.6080" }, { "timeInForce", "GTC" }, { "positionIdx", 1 } };
            List<Dictionary<string, object>> request = new() { dict1, dict2 };
            var orderInfoString = await TradeService.PlaceBatchOrder(category: Category.LINEAR, request: request);
            if (!string.IsNullOrEmpty(orderInfoString))
            {
                Console.WriteLine(orderInfoString);
                BatchOrderResult? orderInfo = JsonConvert.DeserializeObject<BatchOrderResult>(orderInfoString);
                Assert.Equal(0, orderInfo?.RetCode);
                Assert.Equal("OK", orderInfo?.RetMsg);
                Assert.NotNull(orderInfo?.Result?.List?[0].OrderId);
            }
        }

        [Fact]
        public async Task Check_PlaceBatchOrderByClass()
        {
            var order1 = new OrderRequest { Category = "linear", Symbol = "XRPUSDT", OrderType = "Limit", Side = "Buy", Qty = "10", Price = "0.6080", TimeInForce = "GTC", PositionIdx = 1 };
            var order2 = new OrderRequest { Category = "linear", Symbol = "BLZUSDT", OrderType = "Limit", Side = "Buy", Qty = "10", Price = "0.6080", TimeInForce = "GTC", PositionIdx = 1 };
            var orderInfoString = await TradeService.PlaceBatchOrder(category: Category.LINEAR, request: new List<OrderRequest> { order1, order2 });
            if (!string.IsNullOrEmpty(orderInfoString))
            {
                Console.WriteLine(orderInfoString);
                BatchOrderResult? orderInfo = JsonConvert.DeserializeObject<BatchOrderResult>(orderInfoString);
                Assert.Equal(0, orderInfo?.RetCode);
                Assert.Equal("OK", orderInfo?.RetMsg);
                Assert.NotNull(orderInfo?.Result?.List?[0].OrderId);
            }
        }

        [Fact]
        public async Task Check_PlaceBatchOrderByStruct()
        {
            var order1 = new OrderRequest { Category = "linear", Symbol = "XRPUSDT", OrderType = OrderType.LIMIT.Value, Side = Side.BUY.Value, Qty = "10", Price = "0.6080", TimeInForce = TimeInForce.GTC.Value };
            var order2 = new OrderRequest { Category = "linear", Symbol = "BLZUSDT", OrderType = OrderType.LIMIT.Value, Side = Side.BUY.Value, Qty = "10", Price = "0.6080", TimeInForce = TimeInForce.GTC.Value };
            var request = new List<OrderRequest> { order1, order2 };
            var orderInfoString = await TradeService.PlaceBatchOrder(category: Category.LINEAR, request: request);
            if (!string.IsNullOrEmpty(orderInfoString))
            {
                Console.WriteLine(orderInfoString);
                BatchOrderResult? orderInfo = JsonConvert.DeserializeObject<BatchOrderResult>(orderInfoString);
                Assert.Equal(0, orderInfo?.RetCode);
                Assert.Equal("OK", orderInfo?.RetMsg);
                Assert.NotNull(orderInfo?.Result?.List?[0].OrderId);
            }
        }

        [Fact]
        public async Task Check_AmendOrder()
        {
            var orderInfoString = await TradeService.AmendOrder(orderId: "xxxxxxxxxxxxxx", category: Category.LINEAR, symbol: "XRPUSDT", price: "0.5", qty: "15");
            if (!string.IsNullOrEmpty(orderInfoString))
            {
                Console.WriteLine(orderInfoString);
                OrderResult? orderInfo = JsonConvert.DeserializeObject<OrderResult>(orderInfoString);
                Assert.Equal(0, orderInfo?.RetCode);
                Assert.Equal("OK", orderInfo?.RetMsg);
            }
        }

        [Fact]
        public async Task Check_AmendBatchOrder()
        {
            var order1 = new OrderRequest { Symbol = "XRPUSDT", OrderId = "xxxxxxxxxx", Qty = "10", Price = "0.6080" };
            var order2 = new OrderRequest { Symbol = "BLZUSDT", OrderId = "xxxxxxxxxx", Qty = "15", Price = "0.6090" };
            var orderInfoString = await TradeService.AmendBatchOrder(category: Category.LINEAR, request: new List<OrderRequest> { order1, order2 });
            if (!string.IsNullOrEmpty(orderInfoString))
            {
                Console.WriteLine(orderInfoString);
                OrderResult? orderInfo = JsonConvert.DeserializeObject<OrderResult>(orderInfoString);
                Assert.Equal(0, orderInfo?.RetCode);
                Assert.Equal("OK", orderInfo?.RetMsg);
            }
        }

        [Fact]
        public async Task Check_CancelOrder()
        {
            var orderInfoString = await TradeService.CancelOrder(orderId: "xxxxxxxxxxxxxxxxxx", category: Category.SPOT, symbol: "XRPUSDT");
            if (!string.IsNullOrEmpty(orderInfoString))
            {
                Console.WriteLine(orderInfoString);
                OrderResult? orderInfo = JsonConvert.DeserializeObject<OrderResult>(orderInfoString);
                Assert.Equal(0, orderInfo?.RetCode);
                Assert.Equal("OK", orderInfo?.RetMsg);
            }
        }

        [Fact]
        public async Task Check_CancelBatchOrder()
        {
            var order1 = new OrderRequest { Symbol = "BTC-10FEB23-24000-C", OrderLinkId = "9b381bb1-401" };
            var order2 = new OrderRequest { Symbol = "BTC-10FEB23-24000-C", OrderLinkId = "82ee86dd-001" };
            var orderInfoString = await TradeService.CancelBatchOrder(category: Category.LINEAR, request: new List<OrderRequest> { order1, order2 });
            if (!string.IsNullOrEmpty(orderInfoString))
            {
                Console.WriteLine(orderInfoString);
                OrderResult? orderInfo = JsonConvert.DeserializeObject<OrderResult>(orderInfoString);
                Assert.Equal(0, orderInfo?.RetCode);
                Assert.Equal("OK", orderInfo?.RetMsg);
            }
        }

        [Fact]
        public async Task Check_CancelAllOrder()
        {
            var orderInfoString = await TradeService.CancelAllOrder(category: Category.LINEAR, baseCoin: "USDT", symbol: "BTCUSDT");
            if (!string.IsNullOrEmpty(orderInfoString))
            {
                Console.WriteLine(orderInfoString);
                OrderResult? orderInfo = JsonConvert.DeserializeObject<OrderResult>(orderInfoString);
                Assert.Equal(0, orderInfo?.RetCode);
                Assert.Equal("OK", orderInfo?.RetMsg);
            }
        }
        #endregion
    }
}