using System;
using System.Threading.Tasks;
using Drop.Application.Commands;
using Drop.Application.DTO;

namespace Drop.Application.Services
{
    public interface IParcelsService
    {
        Task<ParcelDto> GetAsync(Guid parcelId);
        Task AddAsync(AddParcel command);
    }
}