using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nop.Web.Areas.Admin.Models.Questionnaire;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework;
using Nop.Services.Questionnaire;
using System.Threading.Tasks;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using System.Linq;
using Nop.Core.Domain.Questionnaire;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using Nop.Services.Localization;
using Nop.Services.Steps;
using Nop.Services.Errors;

namespace Nop.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class QuestionnaireController : BaseAdminController
    {
        public IQuestionnaireService QuestionnaireService { get; }
        public ILocalizationService LocalizationService { get; }
        public IErrorService ErrorService { get; }

        public QuestionnaireController(
            IQuestionnaireService questionnaireService,
            ILocalizationService localizationService,
            IErrorService errorService
            )
        {
            QuestionnaireService = questionnaireService;
            LocalizationService = localizationService;
            ErrorService = errorService;
        }
        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }
        public virtual async Task<IActionResult> List()
        {
            var products = await QuestionnaireService.GetQuestionnaireProductsDDL();
            var model = new QuestionnaireProductModel();
            model.ProductDDL = products.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            }).ToList();
            model.ProductDDL.Insert(0, new SelectListItem
            {
                Text = await LocalizationService.GetResourceAsync("Questionnaire.SelectProduct"),
                Value = "0"
            });
            return View(model);
        }
        public virtual IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetStepById(int stepId)
        {
            // var jsonStr = "{\n      \"id\": 4,\n      \"title\": \"After the indoor pipe temperature is lower than 49℃, the air conditioner will restart normally.\",\n      \"yesId\": null,\n      \"noId\": null,\n      \"no\": null,\n      \"yes\": null,\n      \"inverseNo\": null\n    }";
            var step = await QuestionnaireService.GetStepByIdAsync(stepId);
            return Ok(step.ToModel<StepModel>());
        }
        [HttpGet]
        public async Task<IActionResult> GetErrorDDLByProductId(int productId)
        {
            var errors = await QuestionnaireService.GetErrorsByProductIdAsync(productId);
            var errorsDDLModl = errors.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = ErrorService.GetByIdAsync(x.ErrorId).GetAwaiter().GetResult()?.Code ?? default,
            }).ToList();
            errorsDDLModl.Insert(0, new SelectListItem
            {
                Text = await LocalizationService.GetResourceAsync("Questionnaire.SelectError"),
                Value = "0"
            });
            return Ok(errorsDDLModl);
        }
        [HttpGet]
        public async Task<IActionResult> GetStepByErrorId(int producterrorStepId)
        {
            var step = await QuestionnaireService.GetStepByErrorIdAsync(producterrorStepId);
            var model = step.ToModel<StepModel>();
            return Ok(model);
        }
    }
}
