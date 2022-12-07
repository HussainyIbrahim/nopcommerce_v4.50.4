using Nop.Core.Domain.Questionnaire;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Services.TroubleShootingProduct
{
    public interface ITroubleShootingProductService
    {
        public Task<QuestionnaireProduct> GetByIdAsync(int id);
        public Task CreateAsync(QuestionnaireProduct questionnaireProduct);
        public Task EditAsync(QuestionnaireProduct questionnaireProduct);
        public Task<IList<QuestionnaireProduct>> GetAllAsync();
        public Task<List<QuestionnaireProductError>> GetProductErrorsByProductIdAsync(int id);
        Task InsertProductErrorStepAsync(QuestionnaireProductError productErrorStep);
        Task EditProductErrorStepAsync(QuestionnaireProductError productErrorStep);
        Task<QuestionnaireProductError> GetProductErrorByIdAsync(int id);
    }
}
