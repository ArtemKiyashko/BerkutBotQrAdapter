using System;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Telegram.Bot.Types;

namespace BerkutQrAdapter.Infrastructure
{
    public class BarcodeDecoder : IBarcodeDecoder
    {
        private readonly ITgFileDownloader _tgFileDownloader;

        public BarcodeDecoder(ITgFileDownloader tgFileDownloader)
        {
            _tgFileDownloader = tgFileDownloader;
        }

        public async Task<string> Decode(PhotoSize photo)
        {
            using var stream = await _tgFileDownloader.GetStream(photo.FileId);
            using var image = await Image.LoadAsync<Rgba32>(stream);
            var reader = new ZXing.ImageSharp.BarcodeReader<Rgba32>();
            var result = reader.Decode(image);
            return result?.Text;
        }
    }
}

