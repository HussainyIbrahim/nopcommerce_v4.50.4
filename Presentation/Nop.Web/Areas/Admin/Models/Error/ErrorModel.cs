using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Error
{
    public record ErrorModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Admin.Catalog.Errors.Fields.Code")]
        public string Code { get; set; }
    }
}
