using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Questionnaire;
using Nop.Core.Infrastructure;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Steps;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.Step;
using NUglify.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Controllers
{
    public class StepController : BaseAdminController
    {
        public StepController(
            IStepService stepService,
            IStepModelFactory stepModelFactory,
            IPermissionService permissionService,
            IWebHostEnvironment environment,
            INopFileProvider nopFileProvider
            )
        {
            StepService = stepService;
            StepModelFactory = stepModelFactory;
            PermissionService = permissionService;
            Environment = environment;
            NopFileProvider = nopFileProvider;
        }

        public IWebHostEnvironment Environment { get; }
        public INopFileProvider NopFileProvider { get; }
        public IStepService StepService { get; }
        public IStepModelFactory StepModelFactory { get; }
        public IPermissionService PermissionService { get; }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        public async Task<IActionResult> List()
        {
            // prepair model
            var model = await StepModelFactory.PrepareStepSearchModelAsync(new StepSearchModel());
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (!await PermissionService.AuthorizeAsync(StandardPermissionProvider.ManageSteps))
                return AccessDeniedView();
            var model = await StepModelFactory.PrepareEditStepModelAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CheckIfStepIsUsed(int id, int stepId)
        {
            bool isUsed = await StepService.CheckIfStepIsUsed(id, stepId);
            return Json(new { IsUsed = isUsed });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(IFormCollection keyValuePairs)
        {
            var title = keyValuePairs["title"].ToString();
            int id = int.Parse(keyValuePairs["id"]);
            int? yesId = int.Parse(keyValuePairs["yesId"]) == 0 ? null : int.Parse(keyValuePairs["yesId"]);
            int? noId = int.Parse(keyValuePairs["noId"]) == 0 ? null : int.Parse(keyValuePairs["noId"]);
            bool changeYesId = bool.Parse(keyValuePairs["changeYesId"]);
            bool changeNoId = bool.Parse(keyValuePairs["changeNoId"]);
            var imageName = "";
            if (keyValuePairs.Files.Count > 0)
            {
                var pic = keyValuePairs.Files[0];
                string uploads = Path.Combine(Environment.WebRootPath, "images", "thumbs");
                imageName = Guid.NewGuid().ToString() + "_" + pic.FileName;
                string filePath = Path.Combine(uploads, imageName);
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                await pic.CopyToAsync(fileStream);
            }
            try
            {
                if (changeYesId)
                    await StepService.SetYesRelationToBeNull(yesId.Value);
                if (changeNoId)
                    await StepService.SetYesRelationToBeNull(noId.Value);
                var step = await StepService.GetStepByIdAsync(id);
                step.Title = title;
                step.YesId = yesId;
                step.NoId = noId;
                step.ImageURL = string.IsNullOrWhiteSpace(imageName) ? null : imageName;
                await StepService.UpdateStepAsync(step);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        public async Task<IActionResult> Create()
        {

            if (!await PermissionService.AuthorizeAsync(StandardPermissionProvider.ManageSteps))
                return AccessDeniedView();

            var model = await StepModelFactory.PrepareStepModelAsync(new StepCreateEditModel());
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> List(StepSearchModel stepSearchModel)
        {
            if (!await PermissionService.AuthorizeAsync(StandardPermissionProvider.ManageSteps))
                return await AccessDeniedDataTablesJson();
            StepListModel model = await StepModelFactory.PrepareStepListModelAsync(stepSearchModel);
            model.Data.ForEach(x =>
            {
                x.ImageURL = x.ImageURL == null ? null : NopFileProvider.Combine(NopFileProvider.GetAbsolutePath(NopMediaDefaults.ImageThumbsPath), x.ImageURL);
            });
            return Json(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection keyValuePairs)
        {
            var title = keyValuePairs["title"].ToString();
            int? yesId = int.Parse(keyValuePairs["yesId"]) == 0 ? null : int.Parse(keyValuePairs["yesId"]);
            int? noId = int.Parse(keyValuePairs["noId"]) == 0 ? null : int.Parse(keyValuePairs["noId"]);
            bool changeYesId = bool.Parse(keyValuePairs["changeYesId"]);
            bool changeNoId = bool.Parse(keyValuePairs["changeNoId"]);
            var imageName = "";
            if (keyValuePairs.Files.Count > 0)
            {
                var pic = keyValuePairs.Files[0];
                string uploads = Path.Combine(Environment.WebRootPath, "images", "thumbs");
                imageName = Guid.NewGuid().ToString() + "_" + pic.FileName;
                string filePath = Path.Combine(uploads, imageName);
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                await pic.CopyToAsync(fileStream);
            }
            try
            {
                if (changeYesId)
                    await StepService.SetYesRelationToBeNull(yesId.Value);
                if (changeNoId)
                    await StepService.SetYesRelationToBeNull(noId.Value);
                await StepService.InsertStepAsync(new Step
                {
                    Title = title,
                    YesId = yesId,
                    NoId = noId,
                    ImageURL = string.IsNullOrWhiteSpace(imageName) ? null : imageName
                });
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}
