using System;

namespace Drop.Application.Services
{
    internal class MessengerV2 : IMessenger
    {
        private readonly Guid _id = Guid.NewGuid();
        
        public string GetMessage() => $"Hello V2 {_id}";
    }
}