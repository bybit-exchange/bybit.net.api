namespace bybit.net.api.Models
{
    public class SubUserPermissions
    {
        public List<string> ContractTrade { get; set; } = new List<string>();
        public List<string> Spot { get; set; } = new List<string>();
        public List<string> Wallet { get; set; } = new List<string>();
        public List<string> Options { get; set; } = new List<string>();
        public List<string> Derivatives { get; set; } = new List<string>();
        public List<string> Exchange { get; set; } = new List<string>();
        public List<string> CopyTrading { get; set; } = new List<string>();
    }
}