using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Web.Areas.Admin.Models.Step
{
    public record StepCreateEditModel : BaseNopEntityModel
    {
        public StepCreateEditModel()
        {
            YesDDL = new List<SelectListItem>();
            NoDDL = new List<SelectListItem>();
        }
        [NopResourceDisplayName("Admin.Configuration.Steps.Fields.Title")]
        public string Title { get; set; }
        public string PictureId { get; set; }
        [NopResourceDisplayName("Admin.Configuration.Steps.Fields.YesStep")]
        public int? SelectedYesStepId { get; set; }
        [NopResourceDisplayName("Admin.Configuration.Steps.Fields.NoStep")]
        public int? SelectedNoStepId { get; set; }
        public IList<SelectListItem> YesDDL { get; set; }
        public IList<SelectListItem> NoDDL { get; set; }
    }
}
