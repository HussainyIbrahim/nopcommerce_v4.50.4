using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Nop.Core.Domain.Questionnaire;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Nop.Services.Steps
{
    public interface IStepService
    {
        Task<bool> CheckIfStepIsUsed(int id, int stepId);
        Task<IList<Step>> GetAllStepsAsync();
        Task<Step> GetStepByIdAsync(int id);
        Task<List<Step>> GetUnusedStepsAsync();
        Task InsertStepAsync(Step step);
        Task SetYesRelationToBeNull(int noId);
        Task UpdateStepAsync(Step step);
    }
}
