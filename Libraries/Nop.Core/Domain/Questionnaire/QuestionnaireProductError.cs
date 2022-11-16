namespace Nop.Core.Domain.Questionnaire
{
    public class QuestionnaireProductError : BaseEntity
    {
        public int QuestionnaireProductId { get; set; }
        public int ErrorId { get; set; }
        public string Description { get; set; }
        public virtual Error Error { get; set; }
        public virtual QuestionnaireProduct QuestionnaireProduct { get; set; }
    }
}
