using CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Create;
using CleanArchExample.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Get
{
    public interface ICustomerSuitabilityGetUseCases
    {
        Task<ResponseHandler> HandleGetAll(Guid? id, long? cpf, InvestorProfile? investorProfile, int? totalValue);

        Task<ResponseHandler> HandleGetHistory(long cpf);

        Task<ResponseHandler> HandleGetLast(long cpf);
    }
}
