using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Product
{
    public record ProductErrorModel : BaseNopEntityModel
    {
        public string ErrorName { get; set; }
        public string Description { get; set; }
    }
}
