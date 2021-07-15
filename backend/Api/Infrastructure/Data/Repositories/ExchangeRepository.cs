using APITemplate.Domain.Entities;
using APITemplate.Domain.Repositories.Interfaces;
using Project.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITemplate.Infrastructure.Data.Repositories
{
    public class ExchangeRepository : GenericRepository<Exchange>, IExchangeRepository
    {
        public ExchangeRepository(IUnitOfWork context) : base(context)
        {
        }

        public override async Task<Exchange> CreateOrUpdateAsync(Exchange entity)
        {
            bool exists = await Exists(x => x.ExchangeID == entity.ExchangeID);
            if (entity.ExchangeID != Guid.Empty && exists)
            {
                Update(entity);
            }
            else
            {
                Add(entity);
            }
            return entity;
        }
    }
}
