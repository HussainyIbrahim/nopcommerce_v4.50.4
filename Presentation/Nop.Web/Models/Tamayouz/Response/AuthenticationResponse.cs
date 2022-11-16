using System;

namespace Nop.Web.Models.Tamayouz.Response
{
    public class AuthenticationResponse
    {
        public string token { get; set; }
        public DateTime tokenExpirationDate { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string msisdn { get; set; }
        public string branchId { get; set; }
        public string branchName { get; set; }
        public string[] claims { get; set; }
    }
}
