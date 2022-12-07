namespace Nop.Core.Domain.Questionnaire
{
    public class QuestionnaireProductErrorStep : BaseEntity
    {
        public int QuestionnaireProductErrorId { get; set; }
        public int StepId { get; set; }
        public virtual QuestionnaireProductError QuestionnaireProductError { get; set; }
        public virtual Step Step { get; set; }
    }
}
