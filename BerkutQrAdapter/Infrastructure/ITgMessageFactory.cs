using System;
using Telegram.Bot.Types;

namespace BerkutQrAdapter.Infrastructure
{
    public interface ITgMessageFactory
    {
        public Message GetMessage(Update incomingUpdate);
    }
}

