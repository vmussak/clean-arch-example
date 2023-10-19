using CleanArchExample.Domain.ValueObjects;

namespace CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Create
{
    public class CustomerSuitabilityCreateRequest
    {
        public long Cpf { get; set; }
        public List<InvestorProfileQuestionRequest> AnsweredQuestions { get; set; }
    }

    public class InvestorProfileQuestionRequest
    {
        public string Description { get; set; }

        public List<InvestorProfileAnswerRequest> Answers { get; set; }
    }

    public class InvestorProfileAnswerRequest
    {
        public string Description { get; set; }

        public int Value { get; set; }
    }
}
