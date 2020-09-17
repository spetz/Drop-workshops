using System;
using Microsoft.Extensions.Logging;

namespace Drop.Application.Services
{
    internal class Messenger : IMessenger
    {
        private readonly ILogger<Messenger> _logger;
        private readonly Guid _id = Guid.NewGuid();

        public Messenger(ILogger<Messenger> logger)
        {
            _logger = logger;
            _logger.LogInformation($"Created an instance of messenger with ID {_id}");
        }

        public string GetMessage() => $"Hello {_id}";
    }
}