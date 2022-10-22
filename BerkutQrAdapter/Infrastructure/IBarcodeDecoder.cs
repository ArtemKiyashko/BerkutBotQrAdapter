using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BerkutQrAdapter.Infrastructure
{
    public interface IBarcodeDecoder
    {
        public Task<string> Decode(PhotoSize photo);
    }
}

