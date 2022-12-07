using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Web.Areas.Admin.Models.Product
{
    public record AddEditProductErrorModel : BaseNopEntityModel
    {
        public AddEditProductErrorModel()
        {
            ErrorDDL = new List<SelectListItem>();
            StepDDL = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.TroubleShooting.ProductErrors.Fields.Error")]
        public int SelectedErrorId { get; set; }
        [NopResourceDisplayName("Admin.TroubleShooting.ProductErrors.Fields.Step")]
        public int SelectedStepId { get; set; }
        [NopResourceDisplayName("Admin.TroubleShooting.ProductErrors.Fields.Description")]
        public string Description { get; set; }
        public IList<SelectListItem> ErrorDDL { get; set; }
        public IList<SelectListItem> StepDDL { get; set; }
        public int ProductId { get; set; }
    }
}
