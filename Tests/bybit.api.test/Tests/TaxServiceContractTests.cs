using bybit.net.api.ApiServiceImp;
using bybit.net.api.Models.Tax;
using Xunit;

namespace bybit.api.test.Tests;

public class TaxServiceContractTests
{
    [Fact]
    public async Task BatchCreateTaxReports_SerializesTypedItems()
    {
        var handler = new SignedRequestRecordingHandler(
            "{\"retCode\":0,\"retMsg\":\"OK\",\"result\":{\"batchId\":\"batch-1\"}}");
        var client = new HttpClient(handler) { BaseAddress = new Uri("https://api.bybit.com") };
        var service = new BybitTaxService(client, "key", "secret");

        await service.BatchCreateTaxReports(1, 2, new[]
        {
            new TaxReportItem { Type = "EARN", Number = "1" },
        });

        Assert.Equal(HttpMethod.Post, handler.Method);
        Assert.Equal("/v5/fht/compliance/tax/private/batch_create", handler.Path);
        Assert.Equal(
            "{\"startTime\":1,\"endTime\":2,\"items\":[{\"type\":\"EARN\",\"number\":\"1\"}]}",
            handler.Body);
    }

    [Fact]
    public async Task BatchQueryTaxReports_UsesDocumentedContract()
    {
        var handler = new SignedRequestRecordingHandler(
            "{\"retCode\":0,\"retMsg\":\"OK\",\"result\":{}}");
        var client = new HttpClient(handler) { BaseAddress = new Uri("https://api.bybit.com") };
        var service = new BybitTaxService(client, "key", "secret");

        await service.BatchQueryTaxReports("batch-1");

        Assert.Equal(HttpMethod.Get, handler.Method);
        Assert.Equal("/v5/fht/compliance/tax/private/batch_query", handler.Path);
        Assert.Equal("batchId=batch-1", handler.Query);
    }
}
