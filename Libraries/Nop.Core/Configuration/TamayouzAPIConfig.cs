namespace Nop.Core.Configuration
{
    public partial class TamayouzAPIConfig : IConfig
    {
        public string BaseURL { get; private set; }
        public string Authentication { get; private set; }
        public string OTP { get; private set; }
        public string Discount { get; private set; }
        public string AccountIdentity { get; private set; }
        public string Password { get; private set; }
        public string TimeZone { get; private set; }
        public string BranchId { get; private set; }
    }
}
