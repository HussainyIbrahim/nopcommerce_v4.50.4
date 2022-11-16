using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using System.Collections.Generic;

namespace Nop.Web.Areas.Admin.Models.Questionnaire
{
    public record QuestionnaireProductModel : BaseNopEntityModel
    {
        public QuestionnaireProductModel()
        {
            ProductDDL = new List<SelectListItem>();
            ErrorDDL = new List<SelectListItem>();
        }
        public IList<SelectListItem> ProductDDL { get; set; }
        public IList<SelectListItem> ErrorDDL { get; set; }
        public int SelectedProductId { get; set; }
        public int SelectedErrorId { get; set; }
    }
}
