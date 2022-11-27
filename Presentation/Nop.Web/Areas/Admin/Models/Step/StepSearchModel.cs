using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Step
{
    public partial record StepSearchModel : BaseSearchModel
    {
        public string StepName { get; set; }
    }
}
