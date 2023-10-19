using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Domain
{
    public class InvestorProfileAnswer : BaseEntity
    {
        public InvestorProfileAnswer(string description, int value)
        {
            Description = description;
            Value = value;
            Validate();
        }

        public string Description { get; set; }

        public int Value { get; set; }

        public override bool Validate()
        {
            this.VerifyPositiveValue()
                .VerifyDescription();

            return this.IsValid;
        }

        private InvestorProfileAnswer VerifyDescription()
        {
            if (string.IsNullOrWhiteSpace(this.Description))
            {
                this.IsValid = false;
                this.AddMessage("O atributo 'description' deve ser preenchido.");
            }

            return this;
        }

        private InvestorProfileAnswer VerifyPositiveValue()
        {
            if (this.Value < 0)
            {
                this.IsValid = false;
                this.AddMessage("O atributo 'value' deve ter valor maior ou igual a zero.");
            }

            return this;
        }
    }
}
