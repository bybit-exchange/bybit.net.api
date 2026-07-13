using bybit.net.api.Services;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitBotService : BybitApiService
    {
        public BybitBotService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitBotService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string VALIDATE_SPOT_GRID_INPUT = "/v5/grid/validate-input";
        private const string CREATE_SPOT_GRID_BOT = "/v5/grid/create-grid";
        private const string CLOSE_SPOT_GRID_BOT = "/v5/grid/close-grid";
        private const string GET_SPOT_GRID_BOT_DETAIL = "/v5/grid/query-grid-detail";

        private const string CREATE_DCA_BOT = "/v5/dca/create-bot";
        private const string CLOSE_DCA_BOT = "/v5/dca/close-bot";

        private const string CREATE_FUTURES_COMBO_BOT = "/v5/fcombobot/create";
        private const string GET_FUTURES_COMBO_BOT_LIMIT = "/v5/fcombobot/getlimit";
        private const string CLOSE_FUTURES_COMBO_BOT = "/v5/fcombobot/close";
        private const string GET_FUTURES_COMBO_BOT_DETAIL = "/v5/fcombobot/detail";

        private const string VALIDATE_FUTURES_GRID_INPUT = "/v5/fgridbot/validate";
        private const string CREATE_FUTURES_GRID_BOT = "/v5/fgridbot/create";
        private const string CLOSE_FUTURES_GRID_BOT = "/v5/fgridbot/close";
        private const string GET_FUTURES_GRID_BOT_DETAIL = "/v5/fgridbot/detail";

        private const string CREATE_FUTURES_MARTINGALE_BOT = "/v5/fmartingalebot/create";
        private const string GET_FUTURES_MARTINGALE_BOT_LIMIT = "/v5/fmartingalebot/getlimit";
        private const string CLOSE_FUTURES_MARTINGALE_BOT = "/v5/fmartingalebot/close";
        private const string GET_FUTURES_MARTINGALE_BOT_DETAIL = "/v5/fmartingalebot/detail";

        public async Task<string?> ValidateSpotGridInput(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(VALIDATE_SPOT_GRID_INPUT, parameters);
        }

        public async Task<string?> CreateSpotGridBot(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(CREATE_SPOT_GRID_BOT, parameters);
        }

        public async Task<string?> CloseSpotGridBot(string gridId, string closeMode)
        {
            var body = new Dictionary<string, object>
            {
                { "grid_id", gridId },
                { "close_mode", closeMode }
            };

            return await SendBotRequest(CLOSE_SPOT_GRID_BOT, body);
        }

        public async Task<string?> GetSpotGridBotDetail(string gridId)
        {
            return await SendBotRequest(GET_SPOT_GRID_BOT_DETAIL, new Dictionary<string, object> { { "grid_id", gridId } });
        }

        public async Task<string?> CreateDcaBot(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(CREATE_DCA_BOT, new Dictionary<string, object> { { "parameters", parameters } });
        }

        public async Task<string?> CloseDcaBot(string botId, string closeMode)
        {
            var body = new Dictionary<string, object>
            {
                { "bot_id", botId },
                { "close_mode", closeMode }
            };

            return await SendBotRequest(CLOSE_DCA_BOT, body);
        }

        public async Task<string?> CreateFuturesComboBot(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(CREATE_FUTURES_COMBO_BOT, parameters);
        }

        public async Task<string?> GetFuturesComboBotLimit(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(GET_FUTURES_COMBO_BOT_LIMIT, parameters);
        }

        public async Task<string?> CloseFuturesComboBot(string botId)
        {
            return await SendBotRequest(CLOSE_FUTURES_COMBO_BOT, new Dictionary<string, object> { { "bot_id", botId } });
        }

        public async Task<string?> GetFuturesComboBotDetail(string botId)
        {
            return await SendBotRequest(GET_FUTURES_COMBO_BOT_DETAIL, new Dictionary<string, object> { { "bot_id", botId } });
        }

        public async Task<string?> ValidateFuturesGridInput(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(VALIDATE_FUTURES_GRID_INPUT, parameters);
        }

        public async Task<string?> CreateFuturesGridBot(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(CREATE_FUTURES_GRID_BOT, parameters);
        }

        public async Task<string?> CloseFuturesGridBot(string botId)
        {
            return await SendBotRequest(CLOSE_FUTURES_GRID_BOT, new Dictionary<string, object> { { "bot_id", botId } });
        }

        public async Task<string?> GetFuturesGridBotDetail(string botId)
        {
            return await SendBotRequest(GET_FUTURES_GRID_BOT_DETAIL, new Dictionary<string, object> { { "bot_id", botId } });
        }

        public async Task<string?> CreateFuturesMartingaleBot(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(CREATE_FUTURES_MARTINGALE_BOT, parameters);
        }

        public async Task<string?> GetFuturesMartingaleBotLimit(Dictionary<string, object> parameters)
        {
            return await SendBotRequest(GET_FUTURES_MARTINGALE_BOT_LIMIT, parameters);
        }

        public async Task<string?> CloseFuturesMartingaleBot(string botId)
        {
            return await SendBotRequest(CLOSE_FUTURES_MARTINGALE_BOT, new Dictionary<string, object> { { "bot_id", botId } });
        }

        public async Task<string?> GetFuturesMartingaleBotDetail(string botId)
        {
            return await SendBotRequest(GET_FUTURES_MARTINGALE_BOT_DETAIL, new Dictionary<string, object> { { "bot_id", botId } });
        }

        private async Task<string?> SendBotRequest(string endpoint, Dictionary<string, object> body)
        {
            var result = await this.SendSignedAsync<string>(endpoint, HttpMethod.Post, query: body);
            return result;
        }
    }
}
