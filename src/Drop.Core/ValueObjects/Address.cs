using System;
using Drop.Core.Exceptions;

namespace Drop.Core.ValueObjects
{
    public class Address : IEquatable<Address>
    {
        public string Value { get; }

        public Address(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidAddressException(value);
            }
        
            Value = value;
        }
        
        public static implicit operator Address(string address) => new Address(address);

        public static implicit operator string(Address address) => address.Value;

        public bool Equals(Address other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Address) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
    }
}