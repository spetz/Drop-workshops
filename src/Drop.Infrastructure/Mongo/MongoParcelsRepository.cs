using System;
using System.Threading.Tasks;
using Drop.Core.Entities;
using Drop.Core.Repositories;

namespace Drop.Infrastructure.Mongo
{
    public class MongoParcelsRepository : IParcelsRepository
    {
        public Task<Parcel> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Parcel parcel)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Parcel parcel)
        {
            throw new NotImplementedException();
        }
    }
}