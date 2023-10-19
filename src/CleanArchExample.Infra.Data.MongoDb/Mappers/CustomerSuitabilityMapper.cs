using Amazon.Runtime.Internal;
using CleanArchExample.Application.Extensions;
using CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases;
using CleanArchExample.Domain;
using CleanArchExample.Domain.ValueObjects;
using CleanArchExample.Infra.Data.MongoDb.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Infra.Data.MongoDb.Mappers
{
    public static class CustomerSuitabilityMapper
    {
        public static CustomerSuitabilityCollection ToCollection(this CustomerSuitability customerSuitability)
        {
            var collection = new CustomerSuitabilityCollection
            {
                Id = customerSuitability.Id,
                Cpf = customerSuitability.Cpf,
                TotalValue = customerSuitability.TotalValue,
                InvestorProfile = (int)customerSuitability.InvestorProfile,
                CreatedAt = customerSuitability.CreatedAt,
            };

            if (customerSuitability.AnsweredQuestions != null && customerSuitability.AnsweredQuestions.Any())
            {
                collection.AnsweredQuestions = customerSuitability.AnsweredQuestions.Select(x => new InvestorProfileQuestionCollection
                {
                    Description = x.Description,
                    Answers = x.Answers.Select(y => new InvestorProfileAnswerCollection
                    {
                        Description = y.Description,
                        Value = y.Value
                    }).ToList()
                }).ToList();
            }

            return collection;
        }

        public static CustomerSuitability ToDomain(this CustomerSuitabilityCollection customerSuitabilityCollection)
        {
            var questionList = new List<InvestorProfileQuestion>(customerSuitabilityCollection.AnsweredQuestions.Count);
            foreach (var question in customerSuitabilityCollection.AnsweredQuestions)
            {
                questionList.Add(new InvestorProfileQuestion(
                    question.Description,
                    question.Answers.Select(x => new InvestorProfileAnswer(x.Description, x.Value)).ToList()
                ));
            }

            return new CustomerSuitability(
                customerSuitabilityCollection.Id,
                customerSuitabilityCollection.Cpf,
                customerSuitabilityCollection.TotalValue,
                (InvestorProfile)customerSuitabilityCollection.InvestorProfile,
                customerSuitabilityCollection.CreatedAt,
                questionList
            );
        }
    }
}
