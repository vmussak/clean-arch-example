using CleanArchExample.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Delete
{
    public class CustomerSuitabilityDeleteUseCase : ICustomerSuitabilityDeleteUseCase
    {
        private readonly ICustomerSuitabilityRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerSuitabilityDeleteUseCase(ICustomerSuitabilityRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseHandler> Handle(Guid id)
        {
            _repository.Remove(id);

            await _unitOfWork.Commit();

            return new ResponseHandler(System.Net.HttpStatusCode.OK);
        }
    }
}
