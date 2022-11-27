using Nop.Web.Areas.Admin.Models.Step;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Factories
{
    public interface IStepModelFactory
    {
        Task<StepListModel> PrepareStepListModelAsync(StepSearchModel stepSearchModel);
        Task<StepCreateEditModel> PrepareStepModelAsync(StepCreateEditModel stepCreateEditModel);
        Task<StepCreateEditModel> PrepareEditStepModelAsync(int id);
        Task<StepSearchModel> PrepareStepSearchModelAsync(StepSearchModel stepSearchModel);
    }
}
