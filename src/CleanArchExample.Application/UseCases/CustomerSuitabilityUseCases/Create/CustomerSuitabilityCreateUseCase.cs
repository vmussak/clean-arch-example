using CleanArchExample.Application.Repositories;
using CleanArchExample.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Create
{
    public class CustomerSuitabilityCreateUseCase : ICustomerSuitabilityCreateUseCase
    {
        private readonly ICustomerSuitabilityRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerSuitabilityCreateUseCase(ICustomerSuitabilityRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseHandler> Handle(CustomerSuitabilityCreateRequest request)
        {
            var questionList = new List<InvestorProfileQuestion>(request.AnsweredQuestions.Count);
            foreach(var question in request.AnsweredQuestions)
            {
                questionList.Add(new InvestorProfileQuestion(
                    question.Description,
                    question.Answers.Select(x => new InvestorProfileAnswer(x.Description, x.Value)).ToList()
                ));
            }

            var customerSuitability = new CustomerSuitability(request.Cpf, questionList);

            if(!customerSuitability.IsValid)
            {
                return new ResponseHandler(false, customerSuitability.Messages);
            }

            _repository.Create(customerSuitability);

            await _unitOfWork.Commit();

            return new ResponseHandler(new CustomerSuitabilityPresenter(customerSuitability));
        }
    }
}
