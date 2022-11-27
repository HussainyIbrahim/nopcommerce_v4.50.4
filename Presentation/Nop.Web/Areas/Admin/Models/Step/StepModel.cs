using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Step
{
    public record StepModel : BaseNopEntityModel
    {
        public string Title { get; set; }
        public string ImageURL { get; set; }
    }
}
