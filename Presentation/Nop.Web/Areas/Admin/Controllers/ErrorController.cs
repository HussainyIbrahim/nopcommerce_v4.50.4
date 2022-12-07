using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Questionnaire;
using Nop.Services.Errors;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Error;
using Nop.Web.Framework.Models.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Controllers
{
    public class ErrorController : BaseAdminController
    {
        public IPermissionService PermissionService { get; }
        public IErrorService ErrorService { get; }

        public ErrorController(
            IPermissionService permissionService,
            IErrorService errorService
            )
        {
            PermissionService = permissionService;
            ErrorService = errorService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            var model = new ErrorSearchModel();
            model.SetGridPageSize();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> List(ErrorSearchModel searchModel)
        {
            if (!await PermissionService.AuthorizeAsync(StandardPermissionProvider.ManageSteps))
                return await AccessDeniedDataTablesJson();
            var errors = (await ErrorService.GetAllErrorsAsync()).ToPagedList(searchModel);
            var model = new ErrorListModel().PrepareToGrid(searchModel, errors, () =>
            {
                return errors.Select(x => x.ToModel<ErrorModel>());
            });
            return Json(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ErrorModel errorModel)
        {
            bool added;
            try
            {
                await ErrorService.InsertAsync(errorModel.ToEntity<Error>());
                added = true;
            }
            catch (System.Exception)
            {
                throw;
            }
            return Json(new { success = added });
        }
        public async Task<IActionResult> Edit(int id)
        {
            var error = await ErrorService.GetByIdAsync(id);
            return View(error.ToModel<ErrorModel>());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ErrorModel errorModel)
        {

            bool edited;
            try
            {
                await ErrorService.EditAsync(errorModel.ToEntity<Error>());
                edited = true;
            }
            catch (System.Exception)
            {
                throw;
            }
            return Json(new { success = edited });
        }
    }
}
