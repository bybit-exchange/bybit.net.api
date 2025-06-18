namespace bybit.net.api.Models.Account
{
    public class SetCollateralCoinRequest
    {
        public string? coin { get; set; } // Product type: Unified account or Classic account
        public string? collateralSwitch { get; set; } // Symbol name
    }
}