using System;

namespace Drop.Application.Services
{
    internal class Messenger : IMessenger
    {
        private readonly Guid _id = Guid.NewGuid();

        public string GetMessage() => $"Hello {_id}";
    }
}