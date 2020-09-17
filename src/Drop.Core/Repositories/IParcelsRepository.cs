using System;
using System.Threading.Tasks;
using Drop.Core.Entities;

namespace Drop.Core.Repositories
{
    public interface IParcelsRepository
    {
        Task<Parcel> GetAsync(Guid id);
        Task AddAsync(Parcel parcel);
        Task UpdateAsync(Parcel parcel);
    }
}