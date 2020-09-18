using System;
using System.Threading.Tasks;
using Drop.Core.Entities;
using Drop.Core.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Drop.Infrastructure.Caching
{
    internal class InMemoryParcelsRepository : IParcelsRepository
    {
        private readonly IMemoryCache _cache;

        public InMemoryParcelsRepository(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task<Parcel> GetAsync(Guid id) => Task.FromResult(_cache.Get<Parcel>(GetKey(id)));

        public Task AddAsync(Parcel parcel) => Task.FromResult(_cache.Set(GetKey(parcel.Id), parcel));

        public Task UpdateAsync(Parcel parcel) => Task.FromResult(_cache.Set(GetKey(parcel.Id), parcel));

        private static string GetKey(Guid id) => $"parcels:{id}";
    }
}