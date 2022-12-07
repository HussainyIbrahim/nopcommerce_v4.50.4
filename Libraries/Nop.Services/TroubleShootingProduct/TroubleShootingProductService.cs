using Nop.Core.Domain.Questionnaire;
using Nop.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Services.TroubleShootingProduct
{
    public class TroubleShootingProductService : ITroubleShootingProductService
    {
        public TroubleShootingProductService(
            IRepository<QuestionnaireProduct> repository,
            IRepository<QuestionnaireProductError> productErrorRepository
            )
        {
            Repository = repository;
            ProductErrorRepository = productErrorRepository;
        }

        public IRepository<QuestionnaireProduct> Repository { get; }
        public IRepository<QuestionnaireProductError> ProductErrorRepository { get; }

        public async Task<QuestionnaireProduct> GetByIdAsync(int id)
        {
            return await Repository.GetByIdAsync(id);
        }
        public async Task CreateAsync(QuestionnaireProduct questionnaireProduct)
        {
            await Repository.InsertAsync(questionnaireProduct);
        }
        public async Task EditAsync(QuestionnaireProduct questionnaireProduct)
        {
            await Repository.UpdateAsync(questionnaireProduct);
        }
        public async Task<IList<QuestionnaireProduct>> GetAllAsync()
        {
            return await Repository.Table.ToListAsync();
        }

        public async Task<List<QuestionnaireProductError>> GetProductErrorsByProductIdAsync(int id)
        {
            return await ProductErrorRepository.Table.Where(x => x.QuestionnaireProductId == id).ToListAsync();
        }

        public async Task InsertProductErrorStepAsync(QuestionnaireProductError productErrorStep)
        {
            await ProductErrorRepository.InsertAsync(productErrorStep);
        }
        public async Task EditProductErrorStepAsync(QuestionnaireProductError productErrorStep)
        {
            await ProductErrorRepository.UpdateAsync(productErrorStep);
        }
        public async Task<QuestionnaireProductError> GetProductErrorByIdAsync(int id)
        {
            return await ProductErrorRepository.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
