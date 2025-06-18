using Bybit.Api.ApiServiceImp;
using Bybit.Api.Models.User;
using Bybit.Api.Utils;

using Xunit;

namespace Bybit.Api.Test;

public class UserServiceTest
{
    readonly BybitUserService userService = new(apiKey: "X6wmWloIPvaLXAKqv2", apiSecret: "rY1CWGYLHy0AUjdNZqqspvd3Krhp79fHp1sP", BybitConstants.HTTP_TESTNET_URL);
    #region Batch Order
    [Fact]
    public async Task Check_CreateSubApiKey()
    {
        SubUserPermissions permissions = new()
        {
            Wallet = new List<string> { "AccountTransfer" }
        };

        var subApikeyString = await userService.CreateUserSubApiKey(subuid: 100366706, readOnly: ReadOnly.READ_AND_WRITE, permissions: permissions);
        Console.WriteLine(subApikeyString);
        /*
         {"retCode":0,"retMsg":"","result":{"id":"665570","note":"","apiKey":"xxxxxxxx","readOnly":0,"secret":"xxxxxxxxxxxx",
        "permissions":{"ContractTrade":[],"Spot":[],"Wallet":["AccountTransfer"],"Options":[],"Derivatives":[],"CopyTrading":[],"BlockTrade":[],"Exchange":[],"NFT":[],"Affiliate":[]}},
        "retExtInfo":{},"time":1698606736596}
        */
    }

    [Fact]
    public async Task Check_ModifySubApiKey()
    {
        SubUserPermissions permissions = new()
        {
            Wallet = new List<string> { "AccountTransfer", "SubMemberTransfer" }
        };

        var subApikeyString = await userService.ModifyUserSubApiKey(apikey: "xxxxxxxxxxxxx", permissions: permissions);
        Console.WriteLine(subApikeyString);
    }
    #endregion
}