namespace bybit.net.api.Models
{
    public class MasterUserPermissions : SubUserPermissions
    {
        public List<string> NFT { get; set; } = new List<string>(); // e.g., ["NFTQueryProductList"]
        public List<string> Affiliate { get; set; } = new List<string>(); // e.g., ["Affiliate"]
        public List<string> BlockTrade { get; set; } = new List<string>(); // e.g., ["BlockTrade"]
    }
}