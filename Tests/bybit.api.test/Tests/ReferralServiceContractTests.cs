using bybit.net.api.ApiServiceImp;
using Xunit;

namespace bybit.api.test.Tests;

public class ReferralServiceContractTests
{
    [Fact]
    public async Task GetReferralCampaignRewards_UsesDocumentedContract()
    {
        var (service, handler) = CreateService();

        await service.GetReferralCampaignRewards();

        AssertRequest(handler, "/v5/referral/campaign/rewards", "{}");
    }

    [Fact]
    public async Task GetReferralCommissionRecords_UsesDocumentedContract()
    {
        var (service, handler) = CreateService();

        await service.GetReferralCommissionRecords(
            userIdList: new List<long> { 1, 2 },
            productLine: 3);

        AssertRequest(
            handler,
            "/v5/referral/commission/records",
            "{\"userIdList\":[1,2],\"productLine\":3}");
    }

    [Fact]
    public async Task GetReferralUserIdentityProfile_UsesDocumentedContract()
    {
        var (service, handler) = CreateService();

        await service.GetReferralUserIdentityProfile();

        AssertRequest(handler, "/v5/referral/user/identity-profile", "{}");
    }

    [Fact]
    public async Task GetReferralUserInvitations_UsesDocumentedContract()
    {
        var (service, handler) = CreateService();

        await service.GetReferralUserInvitations(
            userIdList: new List<long> { 1, 2 },
            friendStatus: 4);

        AssertRequest(
            handler,
            "/v5/referral/user/invitations",
            "{\"userIdList\":[1,2],\"friendStatus\":4}");
    }

    private static (BybitReferralService Service, SignedRequestRecordingHandler Handler) CreateService()
    {
        var handler = new SignedRequestRecordingHandler("{\"retCode\":0,\"retMsg\":\"OK\"}");
        var client = new HttpClient(handler) { BaseAddress = new Uri("https://api.bybit.com") };
        return (new BybitReferralService(client, "key", "secret"), handler);
    }

    private static void AssertRequest(
        SignedRequestRecordingHandler handler,
        string path,
        string body)
    {
        Assert.Equal(HttpMethod.Post, handler.Method);
        Assert.Equal(path, handler.Path);
        Assert.Equal(body, handler.Body);
    }
}
