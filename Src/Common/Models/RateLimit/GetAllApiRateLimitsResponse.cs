using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bybit.net.api.Models.RateLimit
{
    public class GetApiRateLimitResponse
    {
        public List<GetApiRateLimitItem>? list { get; set; }
    }

    public class GetAllApiRateLimitsResponse
    {
        public string? nextPageCursor { get; set; }
        public List<GetApiRateLimitItem>? list { get; set; } // reuse GetApiRateLimitItem
    }

    public class GetApiRateLimitItem
    {
        public string? uids { get; set; }    // Multiple UIDs separated by commas
        public string? bizType { get; set; } // Business type
        public int? rate { get; set; }       // API rate limit per second
    }
}
