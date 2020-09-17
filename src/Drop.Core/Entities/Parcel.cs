using System;
using Drop.Core.ValueObjects;

namespace Drop.Core.Entities
{
    public class Parcel
    {
        public Guid Id { get; }
        public ParcelSize Size { get; }
        public Address Address { get; }
        public ParcelState State { get; private set; }

        public Parcel(Guid id, ParcelSize size, Address address, ParcelState state = ParcelState.New)
        {
            Id = id;
            Size = size;
            Address = address;
            State = state;
        }
    }
}