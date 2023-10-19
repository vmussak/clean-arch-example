using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Delete
{
    public interface ICustomerSuitabilityDeleteUseCase
    {
        Task<ResponseHandler> Handle(Guid id);
    }
}
