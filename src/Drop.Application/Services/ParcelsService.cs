using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drop.Application.Commands;
using Drop.Application.DTO;

namespace Drop.Application.Services
{
    public class ParcelsService : IParcelsService
    {
        private static ISet<ParcelDto> _parcels = new HashSet<ParcelDto>(); // Not a thread-safe
        
        public async Task<ParcelDto> GetAsync(Guid parcelId) => _parcels.SingleOrDefault(p => p.Id == parcelId);

        public async Task AddAsync(AddParcel command)
        {
            _parcels.Add(new ParcelDto
            {
                Id = command.Id,
                Address = command.Address,
                Size = command.Size,
                State = "new"
            });
        }
    }
}