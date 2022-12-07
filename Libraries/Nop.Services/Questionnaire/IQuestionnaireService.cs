using Nop.Core.Domain.Questionnaire;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Services.Questionnaire
{
    public interface IQuestionnaireService
    {
        public Task<Step> GetStepByIdAsync(int stepId);
        public Task<List<QuestionnaireProduct>> GetQuestionnaireProductsDDL();
        public Task<List<QuestionnaireProductError>> GetErrorsByProductIdAsync(int productId);
        public Task<Step> GetStepByErrorIdAsync(int producterrorStepId);
    }
}
