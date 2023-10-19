using CleanArchExample.Application.Repositories;
using CleanArchExample.Domain;
using CleanArchExample.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Get
{
    public class CustomerSuitabilityGetUseCases : ICustomerSuitabilityGetUseCases
    {
        private readonly ICustomerSuitabilityRepository _repository;

        public CustomerSuitabilityGetUseCases(ICustomerSuitabilityRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseHandler> HandleGetAll(Guid? id, long? cpf, InvestorProfile? investorProfile, int? totalValue)
        {
            var suitabilities = await _repository.GetAll(id, cpf, investorProfile, totalValue);

            return new ResponseHandler(suitabilities.Select(x => new CustomerSuitabilityPresenter(x)));
        }

        public async Task<ResponseHandler> HandleGetHistory(long cpf)
        {
            var suitabilities = await _repository.GetAllByCpf(cpf);

            return new ResponseHandler(suitabilities.Select(x => new CustomerSuitabilityPresenter(x)));
        }

        public async Task<ResponseHandler> HandleGetLast(long cpf)
        {
            var suitability = await _repository.GetLastByCpf(cpf);

            return new ResponseHandler(new CustomerSuitabilityPresenter(suitability));
        }
    }
}
