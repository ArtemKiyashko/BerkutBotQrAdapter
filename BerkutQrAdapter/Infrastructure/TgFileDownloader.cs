using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;

namespace BerkutQrAdapter.Infrastructure
{
    public class TgFileDownloader : ITgFileDownloader
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public TgFileDownloader(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        public async Task<Stream> GetStream(string fileId)
        {
            var streamResult = new MemoryStream();
            var file = await _telegramBotClient.GetFileAsync(fileId);
            await _telegramBotClient.DownloadFileAsync(file.FilePath, streamResult);
            streamResult.Seek(0, System.IO.SeekOrigin.Begin);
            return streamResult;
        }
    }
}

