using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BerkutQrAdapter.Infrastructure
{
    public interface IPhotoManager
    {
        public Task<string> DecodePicture(Update tgUpdate);
    }
}

