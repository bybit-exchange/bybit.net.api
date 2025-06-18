using Newtonsoft.Json;

namespace bybit.net.api.Models.Trade.Response
{
    public class BatchOrderResult
    {
        public int? RetCode { get; set; }
        public string? RetMsg { get; set; }
        public BatchOrderResultList? Result { get; set; }
        public BatchOrderExtraInfoList? RetExtInfo { get; set; }
        public long? Time { get; set; }
    }
}