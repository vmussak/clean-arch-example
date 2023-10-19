using CleanArchExample.Domain;
using CleanArchExample.Application.Extensions;

namespace CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases
{
    public class CustomerSuitabilityPresenter
    {
        public CustomerSuitabilityPresenter(CustomerSuitability customerSuitability)
        {
            this.Id = customerSuitability.Id;
            this.Cpf = customerSuitability.Cpf;
            this.TotalValue = customerSuitability.TotalValue;
            this.InvestorProfile = (int)customerSuitability.InvestorProfile;
            this.InvestorProfileDescription = customerSuitability.InvestorProfile.GetDescription();
            this.CreatedAt = customerSuitability.CreatedAt;
            if(customerSuitability.AnsweredQuestions != null && customerSuitability.AnsweredQuestions.Any())
            {
                this.AnsweredQuestions = customerSuitability.AnsweredQuestions.Select(x => new InvestorProfileQuestionPresenter
                {
                    Description = x.Description,
                    Answers = x.Answers.Select(y => new InvestorProfileAnswerPresenter
                    {
                        Description = y.Description,
                        Value = y.Value
                    }).ToList()
                }).ToList();
            }
        }

        public Guid Id { get; set; }
        public long Cpf { get; set; }
        public List<InvestorProfileQuestionPresenter> AnsweredQuestions { get; set; }
        public int InvestorProfile { get; set; }
        public string InvestorProfileDescription { get; set; }
        public int TotalValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class InvestorProfileQuestionPresenter
    {
        public string Description { get; set; }

        public List<InvestorProfileAnswerPresenter> Answers { get; set; }
    }

    public class InvestorProfileAnswerPresenter
    {
        public string Description { get; set; }

        public int Value { get; set; }
    }
}
