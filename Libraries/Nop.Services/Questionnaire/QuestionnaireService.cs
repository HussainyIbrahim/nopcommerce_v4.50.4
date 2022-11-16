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
            IRepository<QuestionnaireProductError> questionnaireProductErrorRepo,
            IRepository<QuestionnaireProductErrorStep> questionnaireProductErrorStepRepo
            )
        {
            StepRepo = stepRepo;
            QuestionnaireProductRepo = questionnaireProductRepo;
            ErrorRepo = errorRepo;
            QuestionnaireProductErrorRepo = questionnaireProductErrorRepo;
            QuestionnaireProductErrorStepRepo = questionnaireProductErrorStepRepo;
        }

        public IRepository<Step> StepRepo { get; }
        public IRepository<QuestionnaireProduct> QuestionnaireProductRepo { get; }
        public IRepository<Error> ErrorRepo { get; }
        public IRepository<QuestionnaireProductError> QuestionnaireProductErrorRepo { get; }
        public IRepository<QuestionnaireProductErrorStep> QuestionnaireProductErrorStepRepo { get; }

        public async Task<List<Error>> GetErrorsByProductIdAsync(int productId)
        {
            var errors = await ErrorRepo.GetAllAsync(query =>
            {
                query = from error in query
                        join productError in QuestionnaireProductErrorRepo.Table on error.Id equals productError.ErrorId
                        where productError.QuestionnaireProductId == productId
                        select error;
                return query;
            });
            return await errors.ToListAsync();
        }

        public async Task<List<QuestionnaireProduct>> GetQuestionnaireProductsDDL()
        {
            var products = await QuestionnaireProductRepo.GetAllAsync(query => { return query; });
            return await products.ToListAsync();
        }

        public async Task<Step> GetStepByErrorIdAsync(int errorId, int productId)
        {
            var stepDB = await StepRepo.GetAllAsync(query =>
            {
                query = from productError in QuestionnaireProductErrorRepo.Table
                        join productErrorStep in QuestionnaireProductErrorStepRepo.Table on productError.Id equals productErrorStep.QuestionnaireProductErrorId
                        join step in StepRepo.Table on productErrorStep.StepId equals step.Id
                        where productError.ErrorId == errorId && productError.QuestionnaireProductId == productId
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
