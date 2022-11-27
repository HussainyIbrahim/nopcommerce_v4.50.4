using DocumentFormat.OpenXml.Wordprocessing;
using Nop.Services.Localization;
using Nop.Services.Steps;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Localization;
using Nop.Web.Areas.Admin.Models.Step;
using Nop.Web.Framework.Models.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Factories
{
    public class StepModelFactory : IStepModelFactory
    {
        public StepModelFactory(
            IStepService stepService,
            ILocalizationService localizationService
            )
        {
            StepService = stepService;
            LocalizationService = localizationService;
        }

        public IStepService StepService { get; }
        public ILocalizationService LocalizationService { get; }

        public async Task<StepCreateEditModel> PrepareEditStepModelAsync(int id)
        {
            var stepCreateEditModel = new StepCreateEditModel();
            var step = await StepService.GetStepByIdAsync(id);
            var unUsedSteps = await StepService.GetUnusedStepsAsync();
            stepCreateEditModel.YesDDL = unUsedSteps.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToList();
            stepCreateEditModel.NoDDL = unUsedSteps.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToList();
            stepCreateEditModel.NoDDL.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = await LocalizationService.GetResourceAsync("Admin.Steps.SelectStep"),
                Value = "0"
            });
            stepCreateEditModel.YesDDL.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = await LocalizationService.GetResourceAsync("Admin.Steps.SelectStep"),
                Value = "0"
            });
            stepCreateEditModel.Id = step.Id;
            stepCreateEditModel.SelectedNoStepId = step.NoId ?? 0;
            stepCreateEditModel.SelectedYesStepId = step.YesId ?? 0;
            stepCreateEditModel.PictureId = step.ImageURL;
            stepCreateEditModel.Title = step.Title;
            stepCreateEditModel.YesDDL = stepCreateEditModel.YesDDL.Where(s => s.Value != step.Id.ToString()).ToList();
            stepCreateEditModel.NoDDL = stepCreateEditModel.NoDDL.Where(s => s.Value != step.Id.ToString()).ToList();
            return stepCreateEditModel;
        }

        public async Task<StepListModel> PrepareStepListModelAsync(StepSearchModel stepSearchModel)
        {
            var steps = (await StepService.GetAllStepsAsync()).ToPagedList(stepSearchModel);
            //prepare list model
            var model = new StepListModel()
            .PrepareToGrid(stepSearchModel, steps, () =>
            {
                return steps.Select(step => step.ToModel<StepModel>());
            });

            return model;
        }

        public async Task<StepCreateEditModel> PrepareStepModelAsync(StepCreateEditModel stepCreateEditModel)
        {
            var unUsedSteps = await StepService.GetUnusedStepsAsync();
            stepCreateEditModel.YesDDL = unUsedSteps.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToList();
            stepCreateEditModel.NoDDL = unUsedSteps.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToList();
            stepCreateEditModel.NoDDL.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = await LocalizationService.GetResourceAsync("Admin.Steps.SelectStep"),
                Value = "0"
            });
            stepCreateEditModel.YesDDL.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = await LocalizationService.GetResourceAsync("Admin.Steps.SelectStep"),
                Value = "0"
            });
            return stepCreateEditModel;
        }

        public virtual Task<StepSearchModel> PrepareStepSearchModelAsync(StepSearchModel stepSearchModel)
        {
            //prepare page parameters
            stepSearchModel.SetGridPageSize();
            return Task.FromResult(stepSearchModel);
        }
    }
}
