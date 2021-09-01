using System;

namespace Trsys.CopyTrading.Abstractions
{
    public class EaLogInitEvent : IEvent
    {
        public EaLogInitEvent()
        {
        }

        public EaLogInitEvent(DateTimeOffset timestamp, string key, string keyType, string version, string token)
        {
            Id = Guid.NewGuid().ToString();
            Timestamp = timestamp;
            Key = key;
            KeyType = keyType;
            Version = version;
            Token = token;
        }

        public string Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Type => "EaLogInit";
        public string Key { get; set; }
        public string KeyType { get; set; }
        public string Version { get; set; }
        public string Token { get; set; }
    }
}
