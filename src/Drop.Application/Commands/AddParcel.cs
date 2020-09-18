using System;

namespace Drop.Application.Commands
{
    // CQS / CQRS
    public class AddParcel // Behavior-centric
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Address { get; }
        public string Size { get; }

        public AddParcel(string address, string size)
        {
            Address = address;
            Size = size;
        }
    }
}