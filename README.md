# Bybit Open API Connector .Net6

[![Nuget](https://img.shields.io/nuget/v/bybit.net.api)](https://www.nuget.org/packages/bybit.net.api) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/wuhewuhe/bybit.net.api/blob/main/LICENSE) [![Contributor Victor](https://img.shields.io/badge/contributor-Victor-blue.svg)](https://github.com/wuhewuhe/bybit-java-api)

## Table of Contents

- [About](#about)
- [Development](#development)
- [Installation](#installation)
- [Release Notes](#release-notes)
- [Usage](#usage)
- [Contact](#contact)
- [Contributors](#contributors)

## About
**Bybit.Net.Api** offers an official, powerful, and efficient .NET connector to the  [Bybit public Trading API](https://bybit-exchange.github.io/docs/v5/intro)

Dive into a plethora of functionalities:
- Public Websocket Streaming
- Private Websocket Streaming
- Market Data Retrieval
- Trade Execution
- Position Management
- Account and Asset Info Retrieval
- User Management
- Upgrade History
- Spot Margin UTA & Classical Service
- Broker Earning Data

This initiative, originated by the renowned .NET developer Victor, now flourishes under the meticulous care of Bybit's dedicated team of in-house .NET professionals. 
Your contributions are warmly welcomed and appreciated!

## Development
**Bybit.Net.Api** constantly evolves, keeping pace with the freshest features from Bybit's API. Crafted for efficiency, the library maintains a slim profile by minimizing external dependencies. If you've broadened its horizons or ironed out bugs, we eagerly await your pull request.

## Installation
Ensure you're using .NET 10 or newer. This SDK depends on Microsoft.Extensions.Logging 7.0.0 and Newtonsoft 13.0.3.
Dotnet CLI
```bash
dotnet add package bybit.net.api
```

Nuget tool
```DotNet
NuGet\Install-Package bybit.net.api
```

Package reference
```DotNet
<PackageReference Include="bybit.net.api"/>
```
Furthermore methods to install pakcage, please check [Nuget Repository](https://www.nuget.org/packages/bybit.net.api)

## Release-Notes
### HTTP Sync & Async Request
- Receive Window Parameter: Added by default (5 seconds).
- Debug Mode Parameter: Added by default (false) to print request and response headers.
- Base URL Setting: Allows setting to testnet or mainnet. by default [HTTP_MAINNET_URL](https://api.bybit.com)
- Trade API: For create/amend/cancel single & batch orders, now supports dedicated class, dictionary.
- Asset API: Deposit and withdrawal operations will automatically generate a transfer ID.
- Account API: Add new function Set Spot Hedging
- Position API: Add new function Confirm New Risk Limit

### WebSocket
- Ping Pong Interval Parameter: Added by default (20 seconds).
- Max Alive Time Parameter: Only supports private channel, ranging from 30s to 600s (also supports minutes).

### Change Log
- Deprecated useTestnet from http rest api: replace by target url

## Usage
By default is bybit Mainnet, if you want to test in Bybit testnet, please add a parameter **useTestnet: true** when initiate service instance

Note: Replace placeholders (like YOUR_API_KEY, links, or other details) with the actual information. You can also customize this template to better fit the actual state and details of your DotNet API.
### RESTful APIs
- Market Kline
```DotNet
BybitMarketDataService market = new();
var klineInfo = await market.GetMarketKline(Category.SPOT, "BTCUSDT", MarketInterval.OneMinute);
Console.WriteLine(klineInfo);
```

### Authentication - RESTful APIs
- Place Single Order
```DotNet
BybitTradeService tradeService = new(apiKey: "xxxxxxxxxxxxxx", apiSecret: "xxxxxxxxxxxxxxxxxxxxx");
var orderInfo = await tradeService.PlaceOrder(category: Category.LINEAR, symbol: "BLZUSDT", side: Side.BUY, orderType: OrderType.MARKET, qty: "15", timeInForce: TimeInForce.GTC);
Console.WriteLine(orderInfo);
```

- Place Batch Order by Dictionary
```DotNet
Dictionary<string, object> dict1 = new() { { "symbol", "XRPUSDT" }, { "orderType", "Limit" }, { "side", "Buy" }, { "qty", "10" }, { "price", "0.6080" }, { "timeInForce", "GTC" } };
Dictionary<string, object> dict2 = new() { { "symbol", "BLZUSDT" }, { "orderType", "Limit" }, { "side", "Buy" }, { "qty", "10" }, { "price", "0.6080" }, { "timeInForce", "GTC" } };
List<Dictionary<string, object>> request = new() { dict1, dict2 };
var orderInfoString = await TradeService.PlaceBatchOrder(category: Category.LINEAR, request: request);
Console.WriteLine(orderInfoString);
```

- Place Batch Order by dedicated OrderRequest Class
```DotNet
var order1 = new OrderRequest { Symbol = "XRPUSDT", OrderType = OrderType.LIMIT.Value, Side = Side.BUY.Value, Qty = "10", Price = "0.6080", TimeInForce = TimeInForce.GTC.Value };
var order2 = new OrderRequest { Symbol = "BLZUSDT", OrderType = OrderType.LIMIT.Value, Side = Side.BUY.Value, Qty = "10", Price = "0.6080", TimeInForce = TimeInForce.GTC.Value };
var request = new List<OrderRequest> { order1, order2 };
var orderInfoString = await TradeService.PlaceBatchOrder(category: Category.LINEAR, request: request);
Console.WriteLine(orderInfoString);
```

- Account Wallet
```DotNet
BybitAccountService accountService = new(apiKey: "xxxxxxxxxxxxxx", apiSecret: "xxxxxxxxxxxxxxxxxxxxx");
var accountInfo = await accountService.GetAccountBalance(accountType: AccountType.Unified);
Console.WriteLine(accountInfo);
```
- Position Info
```DotNet
BybitPositionService positionService = new(apiKey: "xxxxxxxxxxxxxx", apiSecret: "xxxxxxxxxxxxxxxxxxxxx");
var positionInfo = await positionService.GetPositionInfo(category: Category.LINEAR, symbol: "BLZUSDT");
Console.WriteLine(positionInfo);
```

### WebSocket public channel
- Trade Subscribe
```DotNet
var linearWebsocket = new BybitLinearWebSocket(useTestNet: true, pingIntevral: 5);
linearWebsocket.OnMessageReceived(
    (data) =>
    {
        Console.WriteLine(data);
        return Task.CompletedTask;
    }, CancellationToken.None);
await linearWebsocket.ConnectAsync(new string[] { "publicTrade.BTCUSDT" }, CancellationToken.None);
```

### WebSocket private channel
- Order Subscribe
```DotNet
var privateWebsocket = new BybitPrivateWebsocket(apiKey: "xxxxxxxxxxx", apiSecret: "xxxxxxxxxxxxxxx", useTestNet: true, pingIntevral: 5, maxAliveTime:"120s");
privateWebsocket.OnMessageReceived(
    (data) =>
    {
        Console.WriteLine(data);
        return Task.CompletedTask;
    }, CancellationToken.None);
await privateWebsocket.ConnectAsync(new string[] { "order" }, CancellationToken.None);
```

## Contact
For support, join our Bybit API community on [Telegram](https://t.me/Bybitapi).

## Contributors
List of other contributors
<table>
  <tr>
    <td align="center">
        <a href="https://github.com/VictorFrWu">
            <img src="https://avatars.githubusercontent.com/u/32245754?v=4" width="100px;" alt=""/>
            <br />
            <sub>   
                <b>Victor</b>
            </sub>
        </a>
        <br />
        <a href="https://github.com/VictorFrWu/bybit.net.api/commits?author=VictorFrWu" title="Code">💻</a>
        <a href="https://github.com/VictorFrWu/bybit.net.api/commits?author=VictorFrWu" title="Documentation">📖</a>
    </td>
    <td align="center">
        <a href="https://github.com/kolya5544">
            <img src="https://avatars.githubusercontent.com/u/20096248?v=4" width="100px;" alt=""/>
            <br />
            <sub>   
                <b>Kolya</b>
            </sub>
        </a>
        <br />
        <a href="https://github.com/VictorFrWu/bybit.net.api/commits?author=kolya5544" title="Code">💻</a>
        <a href="https://github.com/VictorFrWu/bybit.net.api/commits?author=kolya5544" title="Documentation">📖</a>
    </td>
  </tr>
</table>
