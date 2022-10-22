using System;
using System.Linq;
using System.Threading.Tasks;
using BerkutQrAdapter.Extensions;
using Telegram.Bot.Types;

namespace BerkutQrAdapter.Infrastructure
{
    public class PhotoManager : IPhotoManager
    {
        private readonly ITgMessageFactory _tgMessageFactory;
        private readonly IBarcodeDecoder _barcodeDecoder;
        /// <summary>
        /// Telegram API limitation for bots - 20MB. <see href="https://core.telegram.org/bots/api#getfile">Documentation</see>
        /// </summary>
        private const int MAXIMUM_TG_PHOTO_FILE_SIZE_BYTES = 20971520; //20MB 

        public PhotoManager(ITgMessageFactory tgMessageFactory, IBarcodeDecoder barcodeDecoder)
        {
            _tgMessageFactory = tgMessageFactory;
            _barcodeDecoder = barcodeDecoder;
        }

        public async Task<string> DecodePicture(Update tgUpdate)
        {
            var tgMessage = _tgMessageFactory.GetMessage(tgUpdate);
            if (!tgMessage.IsForQrProcessing()) return default;
            var tgPhoto = tgMessage.Photo.OrderByDescending(photo => photo.FileSize).First(photo => photo.FileSize <= MAXIMUM_TG_PHOTO_FILE_SIZE_BYTES);
            return await _barcodeDecoder.Decode(tgPhoto);
        }
    }
}

