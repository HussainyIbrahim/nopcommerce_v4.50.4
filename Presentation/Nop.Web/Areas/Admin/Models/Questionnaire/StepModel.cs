using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Questionnaire
{
    public record StepModel : BaseNopEntityModel
    {
        public string Title { get; set; }
        public int? YesId { get; set; }
        public int? NoId { get; set; }
        public virtual StepModel No { get; set; }
        public virtual StepModel Yes { get; set; }
    }
}
