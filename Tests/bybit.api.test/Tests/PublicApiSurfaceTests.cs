using bybit.net.api.ApiServiceImp;
using bybit.net.api.Models.Account;
using bybit.net.api.Models.Tax;
using System.Reflection;
using Xunit;

namespace bybit.api.test.Tests;

public class PublicApiSurfaceTests
{
    [Theory]
    [InlineData(typeof(BybitAccountService), nameof(BybitAccountService.DemoApplyMoney))]
    [InlineData(typeof(BybitTaxService), nameof(BybitTaxService.BatchCreateTaxReports))]
    [InlineData(typeof(BybitAlphaService), nameof(BybitAlphaService.GetLPOrderList))]
    [InlineData(typeof(BybitAlphaService), nameof(BybitAlphaService.GetLPPayTokenPrice))]
    [InlineData(typeof(BybitAlphaService), nameof(BybitAlphaService.GetPredictionOrderBook))]
    [InlineData(typeof(BybitAlphaService), nameof(BybitAlphaService.GetPredictionTokenPrice))]
    [InlineData(typeof(BybitAssetService), nameof(BybitAssetService.TransferSubMemberSave))]
    public void StronglyTypedCollectionEndpoints_HaveSinglePublicOverload(Type serviceType, string methodName)
    {
        var methods = serviceType
            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Where(method => method.Name == methodName);

        Assert.Single(methods);
    }

    [Fact]
    public void CollectionEndpoints_ExposeStronglyTypedParameters()
    {
        AssertCollectionParameter<BybitAccountService, DemoApplyMoneyItem>(nameof(BybitAccountService.DemoApplyMoney), "utaDemoApplyMoney");
        AssertCollectionParameter<BybitTaxService, TaxReportItem>(nameof(BybitTaxService.BatchCreateTaxReports), "items");
        AssertCollectionParameter<BybitAlphaService, long>(nameof(BybitAlphaService.GetLPOrderList), "orderStatus");
        AssertCollectionParameter<BybitAlphaService, string>(nameof(BybitAlphaService.GetLPPayTokenPrice), "tokenCode");
        AssertCollectionParameter<BybitAlphaService, string>(nameof(BybitAlphaService.GetPredictionOrderBook), "tokenIds");
        AssertCollectionParameter<BybitAlphaService, string>(nameof(BybitAlphaService.GetPredictionTokenPrice), "tokenIds");
        AssertCollectionParameter<BybitAssetService, string>(nameof(BybitAssetService.TransferSubMemberSave), "subMemberIds");
    }

    private static void AssertCollectionParameter<TService, TItem>(string methodName, string parameterName)
    {
        var method = typeof(TService).GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        Assert.NotNull(method);

        var parameter = method.GetParameters().Single(item => item.Name == parameterName);
        Assert.Equal(typeof(IReadOnlyCollection<TItem>), parameter.ParameterType);
    }
}
