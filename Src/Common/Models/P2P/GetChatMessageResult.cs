using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bybit.net.api.Models.P2P
{
    public class GetChatMessageResult
    {
        public List<P2pChatMessage>? result { get; set; }
    }

    public class P2pChatMessage
    {
        public string? id { get; set; }
        public string? message { get; set; }           // text or file URL
        public string? userId { get; set; }            // sender
        public int? msgType { get; set; }              // 0 sys, 1 text(user), 2 image(user), 5 text(admin), 6 image(admin), 7 pdf(user), 8 video(user)
        public int? msgCode { get; set; }              // system message code
        public string? createDate { get; set; }        // send time
        public string? contentType { get; set; }       // str | pic | pdf | video
        public string? orderId { get; set; }
        public string? msgUuid { get; set; }           // client message UUID
        public string? nickName { get; set; }
        public string? fileName { get; set; }          // file name for pic/pdf/video
        public string? accountId { get; set; }
        public int? isRead { get; set; }               // 1 read, 0 unread
        public string? roleType { get; set; }          // sys for system message
        public int? onlyForCustomer { get; set; }      // 1 true, 0 false
    }

}
