using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Product
{
    public record TroubleShootingProductModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Admin.TroubleShooting.Products.Fields.Name")]
        public string Name { get; set; }
    }
}
