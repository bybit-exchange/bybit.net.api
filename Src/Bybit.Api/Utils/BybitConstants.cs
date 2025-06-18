namespace bybit.net.api
{
    public static class BybitConstants
    {
        public const string HTTP_MAINNET_URL = "https://api.bybit.com";
        public const string HTTP_MAINNET_BACKUP_URL = "https://api.bytick.com";
        public const string HTTP_TESTNET_URL = "https://api-testnet.bybit.com";
        public const string DEFAULT_REC_WINDOW = "5000";
        public const string DEFAULT_SIGN_TYPE = "2";
        // WebSocket public channel - Mainnet
        public const string SPOT_MAINNET = "wss://stream.bybit.com/v5/public/spot";
        public const string LINEAR_MAINNET = "wss://stream.bybit.com/v5/public/linear";
        public const string INVERSE_MAINNET = "wss://stream.bybit.com/v5/public/inverse";
        public const string OPTION_MAINNET = "wss://stream.bybit.com/v5/public/option";

        // WebSocket public channel - Testnet
        public const string SPOT_TESTNET = "wss://stream-testnet.bybit.com/v5/public/spot";
        public const string LINEAR_TESTNET = "wss://stream-testnet.bybit.com/v5/public/linear";
        public const string INVERSE_TESTNET = "wss://stream-testnet.bybit.com/v5/public/inverse";
        public const string OPTION_TESTNET = "wss://stream-testnet.bybit.com/v5/public/option";

        // WebSocket private channel
        public const string WEBSOCKET_PRIVATE_MAINNET = "wss://stream.bybit.com/v5/private";
        public const string WEBSOCKET_PRIVATE_TESTNET = "wss://stream-testnet.bybit.com/v5/private";
        #region V3
        public const string V3_CONTRACT_PRIVATE = "wss://stream.bybit.com/contract/private/v3";
        public const string V3_UNIFIED_PRIVATE = "wss://stream.bybit.com/unified/private/v3";
        public const string V3_SPOT_PRIVATE = "wss://stream.bybit.com/spot/private/v3";
        #endregion
    }
}
