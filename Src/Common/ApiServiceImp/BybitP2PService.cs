using bybit.net.api.Services;
using bybit.net.api.Models;
using bybit.net.api.Models.P2P;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bybit.net.api.ApiServiceImp
{
    public class BybitP2PService : BybitApiService
    {
        public BybitP2PService(string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
        : this(httpClient: new HttpClient(), apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        public BybitP2PService(HttpClient httpClient, string apiKey, string apiSecret, string? url = null, string recvWindow = BybitConstants.DEFAULT_REC_WINDOW, bool debugMode = false)
            : base(httpClient: httpClient, apiKey: apiKey, apiSecret: apiSecret, url: url, recvWindow: recvWindow, debugMode: debugMode)
        {
        }

        private const string GET_P2P_ADS = "/v5/p2p/item/online";

        /// <summary>
        /// Get Ads
        /// Query online P2P ads.
        /// </summary>
        /// <param name="tokenId">Token ID, e.g., USDT</param>
        /// <param name="currencyId">Currency ID, e.g., USD</param>
        /// <param name="side">"0" buy; "1" sell</param>
        /// <param name="page">Page number, default 1</param>
        /// <param name="size">Page size, default 10</param>
        /// <returns></returns>
        public async Task<P2PResponse<GetAdsResult>?> GetAds(string tokenId, string currencyId, string side, string? page = null, string? size = null)
        {
            var body = new Dictionary<string, object>
            {
                { "tokenId", tokenId },
                { "currencyId", currencyId },
                { "side", side }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("page", page),
                ("size", size)
            );

            var result = await this.SendPublicAsync<P2PResponse<GetAdsResult>>(GET_P2P_ADS, HttpMethod.Post, query: body);
            return result;
        }

        private const string POST_P2P_AD = "/v5/p2p/item/create";

        /// <summary>
        /// Post Ad
        /// Create a P2P advertisement.
        /// Note: For HTTP POST, parameters are sent via query: body.
        /// </summary>
        /// <param name="tokenId">e.g., USDT</param>
        /// <param name="currencyId">e.g., USD</param>
        /// <param name="side">"0" buy; "1" sell</param>
        /// <param name="priceType">"0" fixed; "1" floating</param>
        /// <param name="premium">Premium for floating rate</param>
        /// <param name="price">Price</param>
        /// <param name="minAmount">Min transaction amount</param>
        /// <param name="maxAmount">Max transaction amount</param>
        /// <param name="remark">Ad description (<=900 chars)</param>
        /// <param name="tradingPreferenceSet">
        /// Required object with preference fields. Pass as a Dictionary&lt;string, object&gt; containing any of:
        /// hasUnPostAd,isKyc,isEmail,isMobile,hasRegisterTime,registerTimeThreshold,orderFinishNumberDay30,completeRateDay30,
        /// nationalLimit,hasOrderFinishNumberDay30,hasCompleteRateDay30,hasNationalLimit
        /// </param>
        /// <param name="paymentIds">Payment method type IDs (len ≤ 5)</param>
        /// <param name="quantity">Token quantity in the ad</param>
        /// <param name="paymentPeriod">Payment period in minutes</param>
        /// <param name="itemType">"ORIGIN" or "BULK"</param>
        /// <returns></returns>
        public async Task<P2PResponse<PostAdResult>?> PostAd(
            string tokenId,
            string currencyId,
            string side,
            string priceType,
            string premium,
            string price,
            string minAmount,
            string maxAmount,
            string remark,
            Dictionary<string, object> tradingPreferenceSet,
            IEnumerable<string> paymentIds,
            string quantity,
            string paymentPeriod,
            string itemType)
        {
            var body = new Dictionary<string, object>
            {
                { "tokenId", tokenId },
                { "currencyId", currencyId },
                { "side", side },
                { "priceType", priceType },
                { "premium", premium },
                { "price", price },
                { "minAmount", minAmount },
                { "maxAmount", maxAmount },
                { "remark", remark },
                { "tradingPreferenceSet", tradingPreferenceSet },
                { "paymentIds", paymentIds?.ToArray() ?? Array.Empty<string>() },
                { "quantity", quantity },
                { "paymentPeriod", paymentPeriod },
                { "itemType", itemType }
            };

            var result = await this.SendSignedAsync<P2PResponse<PostAdResult>>(POST_P2P_AD, HttpMethod.Post, query: body);
            return result;
        }

        private const string REMOVE_P2P_AD = "/v5/p2p/item/cancel";

        /// <summary>
        /// Remove Ad
        /// Cancel a P2P advertisement.
        /// </summary>
        /// <param name="itemId">Advertisement ID</param>
        /// <returns></returns>
        public async Task<P2PResponse<object>?> RemoveAd(string itemId)
        {
            var body = new Dictionary<string, object>
            {
                { "itemId", itemId }
            };

            var result = await this.SendSignedAsync<P2PResponse<object>>(REMOVE_P2P_AD, HttpMethod.Post, query: body);
            return result;
        }

        private const string UPDATE_RELIST_P2P_AD = "/v5/p2p/item/update";

        /// <summary>
        /// Update / Relist Ad
        /// Modify or re-online a P2P advertisement.
        /// Note: For HTTP POST, parameters are sent via query: body.
        /// </summary>
        /// <param name="id">Advertisement ID</param>
        /// <param name="priceType">"0" fixed; "1" floating</param>
        /// <param name="premium">Floating ratio</param>
        /// <param name="price">Price per token</param>
        /// <param name="minAmount">Min amount (fiat)</param>
        /// <param name="maxAmount">Max amount (fiat)</param>
        /// <param name="remark">Ad description (<=900 chars)</param>
        /// <param name="tradingPreferenceSet">
        /// Preference fields as Dictionary&lt;string, object&gt;:
        /// hasUnPostAd,isKyc,isEmail,isMobile,hasRegisterTime,registerTimeThreshold,orderFinishNumberDay30,
        /// completeRateDay30,nationalLimit,hasOrderFinishNumberDay30,hasCompleteRateDay30,hasNationalLimit
        /// </param>
        /// <param name="paymentIds">Payment method type IDs (len ≤ 5)</param>
        /// <param name="actionType">"MODIFY" or "ACTIVE"</param>
        /// <param name="quantity">Token quantity</param>
        /// <param name="paymentPeriod">Payment period in minutes</param>
        /// <returns></returns>
        public async Task<P2PResponse<object>?> UpdateOrRelistAd(
            string id,
            string priceType,
            string premium,
            string price,
            string minAmount,
            string maxAmount,
            string remark,
            Dictionary<string, object> tradingPreferenceSet,
            IEnumerable<string> paymentIds,
            string actionType,
            string quantity,
            string paymentPeriod)
        {
            var body = new Dictionary<string, object>
            {
                { "id", id },
                { "priceType", priceType },
                { "premium", premium },
                { "price", price },
                { "minAmount", minAmount },
                { "maxAmount", maxAmount },
                { "remark", remark },
                { "tradingPreferenceSet", tradingPreferenceSet },
                { "paymentIds", paymentIds?.ToArray() ?? Array.Empty<string>() },
                { "actionType", actionType },
                { "quantity", quantity },
                { "paymentPeriod", paymentPeriod }
            };

            var result = await this.SendSignedAsync<P2PResponse<object>>(UPDATE_RELIST_P2P_AD, HttpMethod.Post, query: body);
            return result;
        }

        private const string GET_MY_P2P_ADS = "/v5/p2p/item/personal/list";

        /// <summary>
        /// Get My Ads
        /// Query your P2P advertisements.
        /// Note: POST parameters go via query: body.
        /// </summary>
        /// <param name="itemId">Advertisement ID</param>
        /// <param name="status">1 Sold Out; 2 Available</param>
        /// <param name="side">0 buy; 1 sell</param>
        /// <param name="tokenId">e.g., USDT</param>
        /// <param name="page">default 1</param>
        /// <param name="size">default 10</param>
        /// <param name="currencyId">e.g., USD</param>
        /// <returns></returns>
        public async Task<P2PResponse<GetMyAdsResult>?> GetMyAds(
            string? itemId = null,
            string? status = null,
            string? side = null,
            string? tokenId = null,
            string? page = null,
            string? size = null,
            string? currencyId = null)
        {
            var body = new Dictionary<string, object>();

            BybitParametersUtils.AddOptionalParameters(body,
                ("itemId", itemId),
                ("status", status),
                ("side", side),
                ("tokenId", tokenId),
                ("page", page),
                ("size", size),
                ("currencyId", currencyId)
            );

            var result = await this.SendSignedAsync<P2PResponse<GetMyAdsResult>>(GET_MY_P2P_ADS, HttpMethod.Post, query: body);
            return result;
        }

        private const string GET_MY_AD_DETAILS = "/v5/p2p/item/info";

        /// <summary>
        /// Get My Ad Details
        /// Returns detailed info for a specific P2P advertisement.
        /// Note: POST parameters are sent via query: body.
        /// </summary>
        /// <param name="itemId">Advertisement ID</param>
        /// <returns></returns>
        public async Task<P2PResponse<MyAdItem>?> GetMyAdDetails(string itemId)
        {
            var body = new Dictionary<string, object>
            {
                { "itemId", itemId }
            };

            var result = await this.SendSignedAsync<P2PResponse<MyAdItem>>(GET_MY_AD_DETAILS, HttpMethod.Post, query: body);
            return result;
        }

        private const string GET_P2P_ALL_ORDERS = "/v5/p2p/order/simplifyList";

        /// <summary>
        /// Get All Orders
        /// Paginated P2P orders. POST uses query: body.
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="size">Rows per page</param>
        /// <param name="status">Order status filter</param>
        /// <param name="beginTime">Begin time</param>
        /// <param name="endTime">End time</param>
        /// <param name="tokenId">Token id</param>
        /// <param name="side">Side filter. 0 buy; 1 sell</param>
        /// <returns></returns>
        public async Task<P2PResponse<GetAllOrdersResult>?> GetAllOrders(
            int page,
            int size,
            int? status = null,
            string? beginTime = null,
            string? endTime = null,
            string? tokenId = null,
            int? side = null)
        {
            var body = new Dictionary<string, object>
            {
                { "page", page },
                { "size", size }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("status", status),
                ("beginTime", beginTime),
                ("endTime", endTime),
                ("tokenId", tokenId),
                ("side", side)
            );

            var result = await this.SendSignedAsync<P2PResponse<GetAllOrdersResult>>(GET_P2P_ALL_ORDERS, HttpMethod.Post, query: body);
            return result;
        }

        private const string GET_P2P_ORDER_DETAIL = "/v5/p2p/order/info";

        /// <summary>
        /// Get Order Detail
        /// Returns detailed info for a P2P order. POST uses query: body.
        /// </summary>
        /// <param name="orderId">Order ID</param>
        /// <returns></returns>
        public async Task<P2PResponse<GetOrderDetailResult>?> GetOrderDetail(string orderId)
        {
            var body = new Dictionary<string, object>
            {
                { "orderId", orderId }
            };

            var result = await this.SendSignedAsync<P2PResponse<GetOrderDetailResult>>(GET_P2P_ORDER_DETAIL, HttpMethod.Post, query: body);
            return result;
        }

        private const string GET_P2P_PENDING_ORDERS = "/v5/p2p/order/pending/simplifyList";

        /// <summary>
        /// Get Pending Orders
        /// POST uses query: body.
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="size">Rows per page</param>
        /// <param name="status">Optional status filter</param>
        /// <param name="beginTime">Begin time</param>
        /// <param name="endTime">End time</param>
        /// <param name="tokenId">Token id</param>
        /// <param name="side">Side filter. 0 buy; 1 sell</param>
        /// <returns></returns>
        public async Task<P2PResponse<GetAllOrdersResult>?> GetPendingOrders(
            int page,
            int size,
            int? status = null,
            string? beginTime = null,
            string? endTime = null,
            string? tokenId = null,
            int? side = null)
        {
            var body = new Dictionary<string, object>
            {
                { "page", page },
                { "size", size }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("status", status),
                ("beginTime", beginTime),
                ("endTime", endTime),
                ("tokenId", tokenId),
                ("side", side)
            );

            var result = await this.SendSignedAsync<P2PResponse<GetAllOrdersResult>>(GET_P2P_PENDING_ORDERS, HttpMethod.Post, query: body);
            return result;
        }

        private const string MARK_P2P_ORDER_AS_PAID = "/v5/p2p/order/pay";

        /// <summary>
        /// Mark Order as Paid
        /// Marks a P2P order as paid using a specific payment method.
        /// Note: For HTTP POST, parameters are sent via query: body.
        /// </summary>
        /// <param name="orderId">Order ID</param>
        /// <param name="paymentType">Payment method type</param>
        /// <param name="paymentId">Payment method ID</param>
        /// <returns></returns>
        public async Task<P2PResponse<object>?> MarkOrderAsPaid(string orderId, string paymentType, string paymentId)
        {
            var body = new Dictionary<string, object>
            {
                { "orderId", orderId },
                { "paymentType", paymentType },
                { "paymentId", paymentId }
            };

            var result = await this.SendSignedAsync<P2PResponse<object>>(MARK_P2P_ORDER_AS_PAID, HttpMethod.Post, query: body);
            return result;
        }

        private const string RELEASE_P2P_ORDER_ASSETS = "/v5/p2p/order/finish";

        /// <summary>
        /// Release Assets
        /// Release assets for a P2P order.
        /// Note: For HTTP POST, parameters are sent via query: body.
        /// </summary>
        /// <param name="orderId">Order ID</param>
        /// <returns></returns>
        public async Task<P2PResponse<object>?> ReleaseAssets(string orderId)
        {
            var body = new Dictionary<string, object>
            {
                { "orderId", orderId }
            };

            var result = await this.SendSignedAsync<P2PResponse<object>>(RELEASE_P2P_ORDER_ASSETS, HttpMethod.Post, query: body);
            return result;
        }

        private const string REVIEW_SELLER_CANCEL_ORDER_APPLY = "/v5/p2p/order/buyer/examine/sellerCancelOrderApply";

        /// <summary>
        /// Review Seller Cancel Order Apply
        /// Approve or reject a seller cancel request as the buyer.
        /// </summary>
        /// <param name="orderId">Order ID</param>
        /// <param name="examineResult">PASS or REJECT</param>
        /// <param name="rejectReason">Reject reason. Required when examineResult is REJECT</param>
        /// <param name="rejectProofs">Reject proof image URLs or identifiers, comma-separated. Required when examineResult is REJECT</param>
        /// <param name="rejectRemark">Reject remark</param>
        /// <returns></returns>
        public async Task<GeneralResponse<object>?> ReviewSellerCancelOrderApply(
            string orderId,
            string examineResult,
            string? rejectReason = null,
            string? rejectProofs = null,
            string? rejectRemark = null)
        {
            var body = new Dictionary<string, object>
            {
                { "orderId", orderId },
                { "examineResult", examineResult }
            };

            BybitParametersUtils.AddOptionalParameters(body,
                ("rejectReason", rejectReason),
                ("rejectProofs", rejectProofs),
                ("rejectRemark", rejectRemark)
            );

            var result = await this.SendSignedAsync<GeneralResponse<object>>(REVIEW_SELLER_CANCEL_ORDER_APPLY, HttpMethod.Post, query: body);
            return result;
        }

        private const string SEND_P2P_CHAT_MESSAGE = "/v5/p2p/order/message/send";

        /// <summary>
        /// Send Chat Message
        /// Send a text or file message to the P2P order chat.
        /// Note: For HTTP POST, parameters are sent via query: body.
        /// </summary>
        /// <param name="message">Text content or URL of the file</param>
        /// <param name="contentType">str | pic | pdf | video</param>
        /// <param name="orderId">Order ID</param>
        /// <param name="msgUuid">Client message UUID</param>
        /// <param name="fileName">Optional filename for pic/pdf/video</param>
        /// <returns></returns>
        public async Task<P2PResponse<object>?> SendChatMessage(
            string message,
            string contentType,
            string orderId,
            string msgUuid,
            string? fileName = null)
        {
            var body = new Dictionary<string, object>
            {
                { "message", message },
                { "contentType", contentType },
                { "orderId", orderId },
                { "msgUuid", msgUuid }
            };

            BybitParametersUtils.AddOptionalParameters(body, ("fileName", fileName));

            var result = await this.SendSignedAsync<P2PResponse<object>>(SEND_P2P_CHAT_MESSAGE, HttpMethod.Post, query: body);
            return result;
        }

        private const string UPLOAD_P2P_CHAT_FILE = "/v5/p2p/oss/upload_file";

        /// <summary>
        /// Upload Chat File
        /// Upload a file for P2P order chat.
        /// </summary>
        /// <param name="fileContent">File content stream</param>
        /// <param name="fileName">File name</param>
        /// <param name="contentType">File content type</param>
        /// <returns></returns>
        public async Task<P2PResponse<UploadChatFileResult>?> UploadChatFile(Stream fileContent, string fileName, string contentType)
        {
            using var content = new MultipartFormDataContent();
            var file = new StreamContent(fileContent);
            file.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            content.Add(file, "upload_file", fileName);

            var result = await this.SendSignedMultipartAsync<P2PResponse<UploadChatFileResult>>(UPLOAD_P2P_CHAT_FILE, content);
            return result;
        }

        private const string GET_P2P_CHAT_MESSAGE = "/v5/p2p/order/message/listpage";

        /// <summary>
        /// Get Chat Message
        /// Paginated chat messages for a P2P order. POST uses query: body.
        /// </summary>
        /// <param name="orderId">Order ID</param>
        /// <param name="size">Page size</param>
        /// <param name="currentPage">Current page number</param>
        /// <returns></returns>
        public async Task<P2PResponse<GetChatMessageResult>?> GetChatMessage(string orderId, string size, string? currentPage = null)
        {
            var body = new Dictionary<string, object>
            {
                { "orderId", orderId },
                { "size", size }
            };
            BybitParametersUtils.AddOptionalParameters(body, ("currentPage", currentPage));

            var result = await this.SendSignedAsync<P2PResponse<GetChatMessageResult>>(GET_P2P_CHAT_MESSAGE, HttpMethod.Post, query: body);
            return result;
        }

        private const string GET_P2P_ACCOUNT_INFORMATION = "/v5/p2p/user/personal/info";

        /// <summary>
        /// Get Account Information
        /// Returns your P2P account profile. POST uses query: body with no parameters.
        /// </summary>
        /// <returns></returns>
        public async Task<P2PResponse<GetAccountInformationResult>?> GetAccountInformation()
        {
            var body = new Dictionary<string, object>(); // empty POST payload goes in query
            var result = await this.SendSignedAsync<P2PResponse<GetAccountInformationResult>>(GET_P2P_ACCOUNT_INFORMATION, HttpMethod.Post, query: body);
            return result;
        }

        private const string GET_P2P_COUNTERPARTY_USER_INFO = "/v5/p2p/user/order/personal/info";

        /// <summary>
        /// Get Counterparty User Info
        /// Returns profile for the counterparty of a P2P order.
        /// Note: HTTP POST uses query: body.
        /// </summary>
        /// <param name="originalUid">Counterparty User ID</param>
        /// <param name="orderId">Order ID</param>
        /// <returns></returns>
        public async Task<P2PResponse<GetAccountInformationResult>?> GetCounterpartyUserInfo(string? originalUid = null, string? orderId = null)
        {
            var body = new Dictionary<string, object>();
            BybitParametersUtils.AddOptionalParameters(body,
                ("originalUid", originalUid),
                ("orderId", orderId)
            );

            var result = await this.SendSignedAsync<P2PResponse<GetAccountInformationResult>>(GET_P2P_COUNTERPARTY_USER_INFO, HttpMethod.Post, query: body);
            return result;
        }

        private const string GET_P2P_USER_PAYMENT = "/v5/p2p/user/payment/list";

        /// <summary>
        /// Get User Payment
        /// Returns your saved P2P payment methods. POST sends params via query: body (none here).
        /// </summary>
        public async Task<P2PResponse<List<UserPaymentItem>>?> GetUserPayment()
        {
            var body = new Dictionary<string, object>(); // empty payload
            var result = await this.SendSignedAsync<P2PResponse<List<UserPaymentItem>>>(GET_P2P_USER_PAYMENT, HttpMethod.Post, query: body);
            return result;
        }
    }
}
