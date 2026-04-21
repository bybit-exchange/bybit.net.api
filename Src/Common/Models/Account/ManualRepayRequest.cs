namespace bybit.net.api.Models.Account
{
    public class ManualRepayRequest
    {
        public string? Coin { get; set; }

        public string? Amount { get; set; }

        public RepaymentType? RepaymentType { get; set; }
    }
}
