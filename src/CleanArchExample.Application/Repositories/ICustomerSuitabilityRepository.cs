using CleanArchExample.Domain;
using CleanArchExample.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Application.Repositories
{
    public interface ICustomerSuitabilityRepository
    {
        void Create(CustomerSuitability customerSuitability);
        void Remove(Guid id);
        Task<IEnumerable<CustomerSuitability>> GetAllByCpf(long cpf);
        Task<CustomerSuitability> GetLastByCpf(long cpf);
        Task<IEnumerable<CustomerSuitability>> GetAll(Guid? id, long? cpf, InvestorProfile? investorProfile, int? totalValue);
    }
}
