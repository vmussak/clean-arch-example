using CleanArchExample.Application.Repositories;
using CleanArchExample.Domain;
using CleanArchExample.Domain.ValueObjects;
using CleanArchExample.Infra.Data.MongoDb.Collections;
using CleanArchExample.Infra.Data.MongoDb.Context;
using CleanArchExample.Infra.Data.MongoDb.Mappers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPInc.WealthServices.Communication.Database.MongoDb.Repositories;

namespace CleanArchExample.Infra.Data.MongoDb.Repositories
{
    public class CustomerSuitabilityRepository : BaseRepository<CustomerSuitabilityCollection>, ICustomerSuitabilityRepository
    {
        public CustomerSuitabilityRepository(IMongoContext context) : base(context, "CustomerSuitability")
        {
           
        }

        public void Create(CustomerSuitability customerSuitability)
        {
            var styleCollection = customerSuitability.ToCollection();

            base.Create(styleCollection);
        }

        public async Task<IEnumerable<CustomerSuitability>> GetAllByCpf(long cpf)
        {
            var builder = Builders<CustomerSuitabilityCollection>.Filter;
            var filter = builder.Where(x => x.Cpf == cpf);

            var customerSuitabilities = await base.GetByFilter(filter).ConfigureAwait(false);

            return customerSuitabilities?.Select(x => x.ToDomain());
        }

        public async Task<CustomerSuitability> GetLastByCpf(long cpf)
        {
            var builder = Builders<CustomerSuitabilityCollection>.Filter;
            var filter = builder.Where(x => x.Cpf == cpf);

            var orderBy = Builders<CustomerSuitabilityCollection>.Sort.Descending(x => x.CreatedAt);

            var customerSuitability = await base.GetFirst(filter, orderBy).ConfigureAwait(false);

            return customerSuitability?.ToDomain();
        }

        public async Task<IEnumerable<CustomerSuitability>> GetAll(Guid? id, long? cpf, InvestorProfile? investorProfile, int? totalValue)
        {
            var builder = Builders<CustomerSuitabilityCollection>.Filter;
            var filter = builder.Empty;

            if (id != null && id != Guid.Empty)
            {
                filter &= builder.Where(x => x.Id.Equals(id.Value));
            }

            if (cpf != null)
            {
                filter &= builder.Where(x => x.Cpf == cpf);
            }

            if (investorProfile != null)
            {
                filter &= builder.Where(x => x.InvestorProfile == (int)investorProfile);
            }

            if (totalValue != null)
            {
                filter &= builder.Where(x => x.TotalValue == totalValue);
            }

            var customerSuitability = await base.GetByFilter(filter).ConfigureAwait(false);

            return customerSuitability?.Select(x => x.ToDomain());
        }
    }
}
