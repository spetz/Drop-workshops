using System;
using System.Threading.Tasks;
using Drop.Application.Commands;
using Drop.Application.DTO;

namespace Drop.Application.Services
{
    public interface IParcelsService
    {
        Task<ParcelDto> Get(Guid parcelId);
        Task Add(AddParcel parcel);
    }
}