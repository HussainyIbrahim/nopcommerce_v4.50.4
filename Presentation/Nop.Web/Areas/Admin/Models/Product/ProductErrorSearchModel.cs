using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Product
{
    public record ProductErrorSearchModel : BaseSearchModel
    {
        public int ProductId { get; set; }
    }
}
