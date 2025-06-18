namespace bybit.net.api.Models.Account
{
    public struct AccountType
    {
        private AccountType(string value)
        {
            Value = value;
        }

        // UTA
        public static AccountType InverseDerivatives => new("CONTRACT");
        public static AccountType Unified => new("UNIFIED");
        public static AccountType Fund => new("FUND");

        // Standard account
        public static AccountType Spot => new("SPOT");
        public static AccountType Option => new("OPTION");
        public static AccountType Investment => new("INVESTEMENT");

        public string Value { get; private set; }

        public override readonly string ToString() => Value;

        public static implicit operator string(AccountType accountType) => accountType.Value;
    }

}
