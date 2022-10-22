using System;
using Telegram.Bot.Types;

namespace BerkutQrAdapter.Extensions
{
    public static class TelegramMessageExtensions
    {
        public static bool IsForQrProcessing(this Message tgMessage)
            => tgMessage is not null && tgMessage.Type == Telegram.Bot.Types.Enums.MessageType.Photo;
    }
}

