using Nop.Core.Domain.Questionnaire;
using Nop.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Services.Steps
{
    public class StepService : IStepService
    {
        public StepService(
            IRepository<Step> stepRepo
            )
        {
            StepRepo = stepRepo;
        }
        public IRepository<Step> StepRepo { get; }

        public async Task<bool> CheckIfStepIsUsed(int id, int stepId)
        {
            return await StepRepo.Table.AnyAsync(x => (stepId == 0 || x.Id != stepId) && (x.YesId == id || x.NoId == id));
        }

        public async Task<IList<Step>> GetAllStepsAsync()
        {
            var steps = await StepRepo.GetAllAsync(query =>
            {
                return query;
            });
            return steps;
        }

        public async Task<Step> GetStepByIdAsync(int id)
        {
            return await StepRepo.GetByIdAsync(id);
        }

        public async Task<List<Step>> GetUnusedStepsAsync()
        {
            var steps = await StepRepo.GetAllAsync(query =>
            {
                query = query.Where(s => s.YesId == null && s.NoId == null);
                return query;
            });
            return await steps.ToListAsync();
        }

        public async Task InsertStepAsync(Step step)
        {
            await StepRepo.InsertAsync(step);
        }

        public async Task SetYesRelationToBeNull(int id)
        {
            var step = await StepRepo.Table.FirstOrDefaultAsync(x => x.YesId == id || x.NoId == id);
            if (step.YesId == id)
                step.YesId = null;
            if (step.NoId == id)
                step.No = null;
            await StepRepo.UpdateAsync(step);
        }

        public async Task UpdateStepAsync(Step step)
        {
            await StepRepo.UpdateAsync(step);
        }
    }
}
