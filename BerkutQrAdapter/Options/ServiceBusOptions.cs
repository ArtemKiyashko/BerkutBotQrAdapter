using System;
namespace BerkutQrAdapter.Options
{
    public class ServiceBusOptions
    {
        public string TextMessageTopicName { get; set; }
        public string FullyQualifiedNamespace { get; set; }
        public string ConnectionString { get; set; }
    }
}

