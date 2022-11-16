using System;

namespace Nop.Web.Models.Tamayouz.Response
{
    public class SendOTPRequest
    {
        public string RequestId { get; set; }
        public string Msisdn { get; set; }
        public string BranchId { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
