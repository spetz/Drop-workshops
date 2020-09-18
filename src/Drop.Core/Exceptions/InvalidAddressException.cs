namespace Drop.Core.Exceptions
{
    public class InvalidAddressException : DomainException
    {
        public override string Code { get; } = "invalid_address";
        public string Address { get; }
        
        public InvalidAddressException(string address) : 
            base($"Invalid address: {address}")
        {
            Address = address;
        }
    }
}