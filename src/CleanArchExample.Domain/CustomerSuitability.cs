using CleanArchExample.Domain.ValueObjects;

namespace CleanArchExample.Domain
{
    public class CustomerSuitability : BaseEntity
    {
        public CustomerSuitability(long cpf, List<InvestorProfileQuestion> answeredQuestions)
        {
            Id = Guid.NewGuid();
            Cpf = cpf;
            CreatedAt = DateTime.Now;
            AnsweredQuestions = answeredQuestions;
            Validate();
            CalculateCustomerSuitabilityValues();
        }

        public CustomerSuitability(Guid id, long cpf, int totalValue, InvestorProfile investorProfile, DateTime createdAt, List<InvestorProfileQuestion> answeredQuestions)
        {
            Id = id;
            Cpf = cpf;
            CreatedAt = createdAt;
            InvestorProfile = investorProfile;
            CreatedAt = createdAt;
            TotalValue = totalValue;
            AnsweredQuestions = answeredQuestions;
        }

        public Guid Id { get; private set; }
        public long Cpf { get; private set; }
        public List<InvestorProfileQuestion> AnsweredQuestions { get; private set; }
        public InvestorProfile InvestorProfile { get; private set; }
        public int TotalValue { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public override bool Validate()
        {
            this.VerifyCpf()
                .VerifyQuestions();

            return this.IsValid;
        }

        private CustomerSuitability VerifyCpf()
        {
            if (this.Cpf <= 0)
            {
                this.IsValid = false;
                this.AddMessage("O atributo 'cpf' deve ser preenchido e maior que zero.");
            }

            return this;
        }

        private CustomerSuitability VerifyQuestions()
        {
            if (this.AnsweredQuestions == null || !this.AnsweredQuestions.Any())
            {
                this.IsValid = false;
                this.AddMessage("O atributo 'answeredQuestions' deve ser preenchido.");
                return this;
            }

            if (this.AnsweredQuestions.Any(x => !x.IsValid))
            {
                this.IsValid = false;
                this.AddMessage("O atributo 'answeredQuestions' deve ser preenchido corretamente.");
                foreach (var answer in this.AnsweredQuestions.Where(x => !x.IsValid))
                {
                    this.AddMessages(answer.Messages);
                }
            }

            return this;
        }

        private void CalculateCustomerSuitabilityValues()
        {
            if(!this.IsValid)
            {
                InvestorProfile = InvestorProfile.Unknown;
                TotalValue = -1;
                return;
            }

            this.TotalValue = this.AnsweredQuestions
                .SelectMany(answer => answer.Answers)
                .Sum(x => x.Value);

            this.InvestorProfile = GetInvestorProfile(this.TotalValue);
        }

        private InvestorProfile GetInvestorProfile(int totalValue)
        {
            return totalValue switch
            {
                >= 0 and < 10 => InvestorProfile.Conservative,
                >= 11 and < 25 => InvestorProfile.Moderate,
                >= 25 => InvestorProfile.Aggressive,
                _ => InvestorProfile.Unknown,
            };
        }
    }
}
