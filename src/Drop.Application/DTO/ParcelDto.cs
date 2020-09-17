using System;

namespace Drop.Application.DTO
{
    public class ParcelDto // Data-centric
    {
        public Guid Id { get; set; }
        public string Size { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
    }
}