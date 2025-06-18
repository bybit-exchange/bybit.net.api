namespace bybit.net.api.Models.Account
{
    public struct TransactionType
    {
        private TransactionType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static TransactionType TRANSFER_IN => new("TRANSFER_IN");
        public static TransactionType TRANSFER_OUT => new("TRANSFER_OUT");
        public static TransactionType TRADE => new("TRADE");
        public static TransactionType SETTLEMENT => new("SETTLEMENT");
        public static TransactionType DELIVERY => new("DELIVERY");
        public static TransactionType LIQUIDATION => new("LIQUIDATION");
        public static TransactionType BONUS => new("BONUS");
        public static TransactionType FEE_REFUND => new("FEE_REFUND");
        public static TransactionType INTEREST => new("INTEREST");
        public static TransactionType CURRENCY_BUY => new("CURRENCY_BUY");
        public static TransactionType CURRENCY_SELL => new("CURRENCY_SELL");
        public static TransactionType BORROWED_AMOUNT_INS_LOAN => new("BORROWED_AMOUNT_INS_LOAN");
        public static TransactionType PRINCIPLE_REPAYMENT_INS_LOAN => new("PRINCIPLE_REPAYMENT_INS_LOAN");
        public static TransactionType INTEREST_REPAYMENT_INS_LOAN => new("INTEREST_REPAYMENT_INS_LOAN");
        public static TransactionType AUTO_SOLD_COLLATERAL_INS_LOAN => new("AUTO_SOLD_COLLATERAL_INS_LOAN");
        public static TransactionType AUTO_BUY_LIABILITY_INS_LOAN => new("AUTO_BUY_LIABILITY_INS_LOAN");
        public static TransactionType AUTO_PRINCIPLE_REPAYMENT_INS_LOAN => new("AUTO_PRINCIPLE_REPAYMENT_INS_LOAN");
        public static TransactionType AUTO_INTEREST_REPAYMENT_INS_LOAN => new("AUTO_INTEREST_REPAYMENT_INS_LOAN");
        public static TransactionType TRANSFER_IN_INS_LOAN => new("TRANSFER_IN_INS_LOAN"); // Transfer In when in the liquidation of OTC loan
        public static TransactionType TRANSFER_OUT_INS_LOAN => new("TRANSFER_OUT_INS_LOAN"); // Transfer Out when in the liquidation of OTC loan
        public static TransactionType SPOT_REPAYMENT_SELL => new("SPOT_REPAYMENT_SELL"); // One-click repayment currency sell
        public static TransactionType SPOT_REPAYMENT_BUY => new("SPOT_REPAYMENT_BUY"); // One-click repayment currency buy

        public override readonly string ToString() => Value;

        public static implicit operator string(TransactionType transactionType) => transactionType.Value;
    }

}
