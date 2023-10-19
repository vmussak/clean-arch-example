using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Domain
{
    public class InvestorProfileQuestion : BaseEntity
    {
        public InvestorProfileQuestion(string description, List<InvestorProfileAnswer> answers)
        {
            Description = description;
            Answers = answers;
            Validate();
        }

        public string Description { get; set; }

        public List<InvestorProfileAnswer> Answers { get; set; }

        public override bool Validate()
        {
            this.VerifyAnswers()
                .VerifyDescription();

            return this.IsValid;
        }

        private InvestorProfileQuestion VerifyDescription()
        {
            if (string.IsNullOrWhiteSpace(this.Description))
            {
                this.IsValid = false;
                this.AddMessage("O atributo 'description' deve ser preenchido.");
            }

            return this;
        }

        private InvestorProfileQuestion VerifyAnswers()
        {
            if (this.Answers == null)
            {
                this.IsValid = false;
                this.AddMessage("O atributo 'answers' deve ser preenchido");
                return this;
            }

            if(this.Answers.Any(x => !x.IsValid))
            {
                this.IsValid = false;
                this.AddMessage("O atributo 'answers' deve ser preenchido corretamente.");
                foreach(var answer in this.Answers.Where(x => !x.IsValid))
                {
                    this.AddMessages(answer.Messages);
                }
            }

            return this;
        }
    }
}
