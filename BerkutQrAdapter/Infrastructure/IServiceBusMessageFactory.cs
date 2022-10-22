using System;
using Azure.Messaging.ServiceBus;
using Telegram.Bot.Types;

namespace BerkutQrAdapter.Infrastructure
{
    public interface IServiceBusMessageFactory
    {
        public ServiceBusMessage GetMessage(Update update, string qrResult);
    }
}

