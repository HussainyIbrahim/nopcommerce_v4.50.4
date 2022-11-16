using System.Collections.Generic;

namespace Nop.Core.Domain.Questionnaire
{
    public class Error : BaseEntity
    {
        public Error()
        {
            ProductError = new HashSet<QuestionnaireProductError>();
        }
        public string Code { get; set; }
        public virtual ICollection<QuestionnaireProductError> ProductError { get; set; }

    }
}
