using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Questionnaire;
using Nop.Services.Errors;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Steps;
using Nop.Services.TroubleShootingProduct;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Product;
using Nop.Web.Framework.Models.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Controllers
{
    public class TroubleShootingProductController : BaseAdminController
    {
        public IPermissionService PermissionService { get; }
        public ITroubleShootingProductService TroubleShootingProductService { get; }
        public IErrorService ErrorService { get; }
        public IStepService StepService { get; }
        public ILocalizationService LocalizationService { get; }

        public TroubleShootingProductController(
            IPermissionService permissionService,
            ITroubleShootingProductService troubleShootingProductService,
            IErrorService errorService,
            IStepService stepService,
            ILocalizationService localizationService
            )
        {
            PermissionService = permissionService;
            TroubleShootingProductService = troubleShootingProductService;
            ErrorService = errorService;
            StepService = stepService;
            LocalizationService = localizationService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        public async Task<IActionResult> List()
        {
            if (!await PermissionService.AuthorizeAsync(StandardPermissionProvider.ManageTroubleShootingProducts))
                return AccessDeniedView();
            TroubleShootingProductSearchModel troubleShootingSearchModel = new TroubleShootingProductSearchModel();
            troubleShootingSearchModel.SetGridPageSize();
            return View(troubleShootingSearchModel);
        }
        [HttpPost]
        public async Task<IActionResult> List(TroubleShootingProductSearchModel troubleShootingSearchModel)
        {
            if (!await PermissionService.AuthorizeAsync(StandardPermissionProvider.ManageTroubleShootingProducts))
                return await AccessDeniedDataTablesJson();
            var products = (await TroubleShootingProductService.GetAllAsync()).ToPagedList(troubleShootingSearchModel);
            var model = new TroubleShootingProductListModel().PrepareToGrid(troubleShootingSearchModel, products, () =>
            {
                return products.Select(x => x.ToModel<TroubleShootingProductModel>());
            });
            return Json(model);
        }
        public async Task<IActionResult> Create()
        {
            if (!await PermissionService.AuthorizeAsync(StandardPermissionProvider.ManageTroubleShootingProducts))
                return AccessDeniedView();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TroubleShootingProductModel troubleShootingProductModel)
        {
            bool added;
            try
            {
                await TroubleShootingProductService.CreateAsync(troubleShootingProductModel.ToEntity<QuestionnaireProduct>());
                added = true;
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Json(new { added });
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (!await PermissionService.AuthorizeAsync(StandardPermissionProvider.ManageTroubleShootingProducts))
                return AccessDeniedView();
            var product = await TroubleShootingProductService.GetByIdAsync(id);
            return View(product.ToModel<TroubleShootingProductModel>());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TroubleShootingProductModel troubleShootingProductModel)
        {
            bool edited;
            try
            {
                await TroubleShootingProductService.EditAsync(troubleShootingProductModel.ToEntity<QuestionnaireProduct>());
                edited = true;
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Json(new { edited });
        }
        public async Task<IActionResult> ProductErrorList(int id)
        {
            if (!await PermissionService.AuthorizeAsync(StandardPermissionProvider.ManageTroubleShootingProducts))
                return AccessDeniedView();
            var model = new ProductErrorSearchModel();
            model.SetGridPageSize();
            model.ProductId = id;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProductErrorList(ProductErrorSearchModel productErrorSearchModel, int productId)
        {
            var productErrors = (await TroubleShootingProductService.GetProductErrorsByProductIdAsync(productId)).ToPagedList(productErrorSearchModel);
            var model = new ProductErrorListModel().PrepareToGrid(productErrorSearchModel, productErrors, () =>
            {
                return productErrors.Select(x => new ProductErrorModel
                {
                    Id = x.Id,
                    Description = x?.Description ?? string.Empty,
                    ErrorName = ErrorService.GetByIdAsync(x.ErrorId).GetAwaiter().GetResult()?.Code ?? string.Empty
                }).ToList();
            });
            return Json(model);
        }
        public async Task<IActionResult> CreateProductError(int id)
        {
            AddEditProductErrorModel addEditProductErrorModel = new AddEditProductErrorModel()
            {
                ProductId = id,
                ErrorDDL = (await ErrorService.GetAllErrorsAsync())?.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Code }).ToList(),
                StepDDL = (await StepService.GetAllStepsAsync())?.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Title }).ToList()
            };
            addEditProductErrorModel.ErrorDDL.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = await LocalizationService.GetResourceAsync("Admin.TroubleShooting.ProductErrors.Fields.SelectError"),
                Value = "0"
            });
            addEditProductErrorModel.StepDDL.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = await LocalizationService.GetResourceAsync("Admin.TroubleShooting.ProductErrors.Fields.SelectStep"),
                Value = "0"
            });
            return View("AddEditProductError", addEditProductErrorModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductError(AddEditProductErrorModel addEditProductErrorModel)
        {
            bool added;
            var productErrorStep = new QuestionnaireProductError()
            {
                QuestionnaireProductId = addEditProductErrorModel.ProductId,
                Description = addEditProductErrorModel.Description,
                ErrorId = addEditProductErrorModel.SelectedErrorId,
                Id = addEditProductErrorModel.Id,
                StepId = addEditProductErrorModel.SelectedStepId
            };
            try
            {
                await TroubleShootingProductService.InsertProductErrorStepAsync(productErrorStep);
                added = true;
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Json(new { added = added, ProductId = addEditProductErrorModel.ProductId });
        }
        public async Task<IActionResult> EditProductError(int id)
        {
            var productErrorStep = await TroubleShootingProductService.GetProductErrorByIdAsync(id);

            AddEditProductErrorModel addEditProductErrorModel = new AddEditProductErrorModel();
            addEditProductErrorModel.Id = productErrorStep.Id;
            addEditProductErrorModel.ProductId = productErrorStep.QuestionnaireProductId;
            addEditProductErrorModel.Description = productErrorStep.Description;
            addEditProductErrorModel.ErrorDDL = (await ErrorService.GetAllErrorsAsync())?.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Code }).ToList();
            addEditProductErrorModel.StepDDL = (await StepService.GetAllStepsAsync())?.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = x.Id.ToString(), Text = x.Title }).ToList();
            addEditProductErrorModel.SelectedStepId = productErrorStep.StepId;
            addEditProductErrorModel.SelectedErrorId = productErrorStep.ErrorId;
            return View("AddEditProductError", addEditProductErrorModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditProductError(AddEditProductErrorModel addEditProductErrorModel)
        {
            bool added;
            var productErrorStep = await TroubleShootingProductService.GetProductErrorByIdAsync(addEditProductErrorModel.Id);
            try
            {
                productErrorStep.Description = addEditProductErrorModel.Description;
                productErrorStep.StepId = addEditProductErrorModel.SelectedStepId;
                productErrorStep.ErrorId = addEditProductErrorModel.SelectedErrorId;
                await TroubleShootingProductService.EditProductErrorStepAsync(productErrorStep);
                added = true;
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Json(new { added = added, ProductId = addEditProductErrorModel.ProductId });
        }
    }
}
