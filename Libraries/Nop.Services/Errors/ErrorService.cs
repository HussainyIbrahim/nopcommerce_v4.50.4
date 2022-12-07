using LinqToDB;
using Nop.Core.Domain.Questionnaire;
using Nop.Data;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Nop.Services.Errors
{
    public class ErrorService : IErrorService
    {
        public ErrorService(IRepository<Error> errorRepository)
        {
            ErrorRepository = errorRepository;
        }

        public IRepository<Error> ErrorRepository { get; }

        public async Task EditAsync(Error error)
        {
            await ErrorRepository.UpdateAsync(error);
        }

        public async Task<IList<Error>> GetAllErrorsAsync()
        {
            var errors = await ErrorRepository.Table.ToListAsync();
            return errors;
        }

        public async Task<Error> GetByIdAsync(int id)
        {
            return await ErrorRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(Error error)
        {
            await ErrorRepository.InsertAsync(error);
        }
    }
}
