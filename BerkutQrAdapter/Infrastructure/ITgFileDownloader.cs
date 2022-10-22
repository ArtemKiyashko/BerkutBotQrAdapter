using System;
using System.IO;
using System.Threading.Tasks;

namespace BerkutQrAdapter.Infrastructure
{
    public interface ITgFileDownloader
    {
        public Task<Stream> GetStream(string fileId);
    }
}

