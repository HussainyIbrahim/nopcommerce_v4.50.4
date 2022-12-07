using Nop.Core.Domain.Questionnaire;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Services.Errors
{
    public interface IErrorService
    {
        Task EditAsync(Error error);
        Task<IList<Error>> GetAllErrorsAsync();
        Task<Error> GetByIdAsync(int id);
        Task InsertAsync(Error error);
    }
}
