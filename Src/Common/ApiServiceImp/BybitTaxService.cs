using bybit.net.api.Services;
using bybit.net.api.Models.Tax;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitTaxService : BybitApiService
    {
        public BybitTaxService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitTaxService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string BATCH_CREATE_TAX_REPORTS = "/v5/fht/compliance/tax/private/batch_create";
        /// <summary>
        /// Batch create tax report files
        /// </summary>
        public async Task<string?> BatchCreateTaxReports(long startTime, long endTime, IReadOnlyCollection<TaxReportItem> items, string? sourceInstitution = null, string? exportFileType = null)
        {
            var query = new Dictionary<string, object>
            {
                { "startTime", startTime },
                { "endTime", endTime },
                { "items", items }
            };
            BybitParametersUtils.AddOptionalParameters(query,
                ("sourceInstitution", sourceInstitution),
                ("exportFileType", exportFileType)
            );
            var result = await this.SendSignedAsync<string>(BATCH_CREATE_TAX_REPORTS, HttpMethod.Post, query: query);
            return result;
        }
        private const string BATCH_QUERY_TAX_REPORTS = "/v5/fht/compliance/tax/private/batch_query";
        /// <summary>
        /// Query batch tax report status
        /// </summary>
        public async Task<string?> BatchQueryTaxReports(string batchId)
        {
            var query = new Dictionary<string, object>
            {
                { "batchId", batchId }
            };
            var result = await this.SendSignedAsync<string>(BATCH_QUERY_TAX_REPORTS, HttpMethod.Get, query: query);
            return result;
        }
    }
}
