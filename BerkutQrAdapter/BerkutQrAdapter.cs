using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using ZXing;
using ZXing.QrCode;
using BerkutQrAdapter.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using BerkutQrAdapter.Options;

namespace BerkutQrAdapter
{
    public class BerkutQrAdapter
    {
        private readonly ILogger<BerkutQrAdapter> _logger;
        private readonly IPhotoManager _photoManager;
        private readonly IServiceBusMessageFactory _serviceBusMessageFactory;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusOptions _options;

        public BerkutQrAdapter(
            ILogger<BerkutQrAdapter> log,
            IPhotoManager photoManager,
            IServiceBusMessageFactory serviceBusMessageFactory,
            ServiceBusClient serviceBusClient,
            IOptions<ServiceBusOptions> options)
        {
            _logger = log;
            _photoManager = photoManager;
            _serviceBusMessageFactory = serviceBusMessageFactory;
            _serviceBusClient = serviceBusClient;
            _options = options.Value;
        }

        [FunctionName("BerkutQrAdapter")]
        public async Task Run([ServiceBusTrigger("alltgmessages", "qradapter", Connection = "ServiceBusOptions", IsSessionsEnabled = true)] Update tgUpdate)
        {
            var decodeResult = await _photoManager.DecodePicture(tgUpdate);
            var sbMessage = _serviceBusMessageFactory.GetMessage(tgUpdate, decodeResult);
            if (sbMessage is null) return;

            await using ServiceBusSender sender = _serviceBusClient.CreateSender(_options.TextMessageTopicName);
            await sender.SendMessageAsync(sbMessage);
        }
    }
}

