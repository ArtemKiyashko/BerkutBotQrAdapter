using System;
using Azure.Messaging.ServiceBus;
using BerkutQrAdapter.Extensions;
using Telegram.Bot.Types;

namespace BerkutQrAdapter.Infrastructure
{
    public class ServiceBusMessageFactory : IServiceBusMessageFactory
    {
        private readonly ITgMessageFactory _tgMessageFactory;

        public ServiceBusMessageFactory(ITgMessageFactory tgMessageFactory)
        {
            _tgMessageFactory = tgMessageFactory;
        }

        public ServiceBusMessage GetMessage(Update update, string qrResult)
        {
            if (string.IsNullOrEmpty(qrResult)) return default;

            var tgMessage = _tgMessageFactory.GetMessage(update);
            tgMessage.Photo = null;
            tgMessage.Text = qrResult;

            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(tgMessage.ToJson());
            serviceBusMessage.SessionId = tgMessage.Chat.Id.ToString();

            return serviceBusMessage;
        }
    }
}

