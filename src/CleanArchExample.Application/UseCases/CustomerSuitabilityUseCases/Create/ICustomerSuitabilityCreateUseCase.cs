﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Create
{
    public interface ICustomerSuitabilityCreateUseCase
    {
        Task<ResponseHandler> Handle(CustomerSuitabilityCreateRequest request);
    }
}
