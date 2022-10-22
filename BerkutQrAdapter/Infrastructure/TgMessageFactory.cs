using System;
using Telegram.Bot.Types;

namespace BerkutQrAdapter.Infrastructure
{
    public class TgMessageFactory : ITgMessageFactory
    {
        public Message GetMessage(Update incomingUpdate) => incomingUpdate?.Type switch
        {
            Telegram.Bot.Types.Enums.UpdateType.Message => incomingUpdate.Message,
            Telegram.Bot.Types.Enums.UpdateType.EditedMessage => incomingUpdate.EditedMessage,
            _ => null
        };
    }
}

