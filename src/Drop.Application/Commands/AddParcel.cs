using System;

namespace Drop.Application.Commands
{
    // CQS / CQRS
    public class AddParcel // Behavior-centric
    {
        public Guid Id { get; }
        public string Address { get; }
        public string Size { get; }

        public AddParcel(Guid id, string address, string size)
        {
            Id = id;
            Address = address;
            Size = size;
        }
    }
}