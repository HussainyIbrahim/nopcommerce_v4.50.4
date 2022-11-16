namespace Nop.Core.Domain.Questionnaire
{
    public class QuestionnaireProductErrorStep : BaseEntity
    {
        public int QuestionnaireProductErrorId { get; set; }
        public int StepId { get; set; }
        public virtual QuestionnaireProductError ProductError { get; set; }
        public virtual Step Step { get; set; }
    }
}
