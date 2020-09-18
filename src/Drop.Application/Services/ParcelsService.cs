using System;
using System.Threading.Tasks;
using Drop.Application.Commands;
using Drop.Application.DTO;
using Drop.Application.Exceptions;
using Drop.Core.Entities;
using Drop.Core.Repositories;
using Drop.Core.ValueObjects;

namespace Drop.Application.Services
{
    public class ParcelsService : IParcelsService
    {
        private readonly IParcelsRepository _parcelsRepository;

        public ParcelsService(IParcelsRepository parcelsRepository)
        {
            _parcelsRepository = parcelsRepository;
        }

        public async Task<ParcelDto> GetAsync(Guid id)
        {
            var parcel = await _parcelsRepository.GetAsync(id);

            return parcel is null
                ? null
                : new ParcelDto
                {
                    Id = parcel.Id,
                    Address = parcel.Address,
                    Size = parcel.Size.ToString().ToLowerInvariant(),
                    State = parcel.State.ToString()
                };
        }
    
        public async Task AddAsync(AddParcel request)
        {
            if (!Enum.TryParse<ParcelSize>(request.Size, true, out var size))
            {
                throw new InvalidParcelSizeException(request.Size);
            }
        
            var parcel = new Parcel(request.Id, size, request.Address);
            await _parcelsRepository.AddAsync(parcel);
        }
    }
}