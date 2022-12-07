using Nop.Core.Domain.Questionnaire;
using Nop.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Services.Questionnaire
{
    public class QuestionnaireService : IQuestionnaireService
    {
        public QuestionnaireService(
            IRepository<Step> stepRepo,
            IRepository<QuestionnaireProduct> questionnaireProductRepo,
            IRepository<Error> errorRepo,
            IRepository<QuestionnaireProductError> questionnaireProductErrorRepo
            )
        {
            StepRepo = stepRepo;
            QuestionnaireProductRepo = questionnaireProductRepo;
            ErrorRepo = errorRepo;
            QuestionnaireProductErrorRepo = questionnaireProductErrorRepo;
        }

        public IRepository<Step> StepRepo { get; }
        public IRepository<QuestionnaireProduct> QuestionnaireProductRepo { get; }
        public IRepository<Error> ErrorRepo { get; }
        public IRepository<QuestionnaireProductError> QuestionnaireProductErrorRepo { get; }

        public async Task<List<QuestionnaireProductError>> GetErrorsByProductIdAsync(int productId)
        {
            var errors = await QuestionnaireProductErrorRepo.Table.Where(x => x.QuestionnaireProductId == productId).ToListAsync();
            return errors;
        }

        public async Task<List<QuestionnaireProduct>> GetQuestionnaireProductsDDL()
        {
            var products = await QuestionnaireProductRepo.GetAllAsync(query => { return query; });
            return await products.ToListAsync();
        }

        public async Task<Step> GetStepByErrorIdAsync(int producterrorStepId)
        {
            var stepDB = await StepRepo.GetAllAsync(query =>
            {
                query = from productError in QuestionnaireProductErrorRepo.Table
                        join step in StepRepo.Table on productError.StepId equals step.Id
                        where productError.Id == producterrorStepId
                        select step;
                return query;
            });
            return (await stepDB.ToListAsync()).FirstOrDefault();
        }

        public async Task<Step> GetStepByIdAsync(int stepId)
        {
            return await StepRepo.GetByIdAsync(stepId);
        }
    }
}
