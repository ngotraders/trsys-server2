﻿using System;

namespace Trsys.CopyTrading.Abstractions
{
    public class SecretKeyUnregisteredEvent : IEvent
    {
        public SecretKeyUnregisteredEvent()
        {
        }

        public SecretKeyUnregisteredEvent(SecretKey secretKey)
        {
            Id = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            SecretKeyId = secretKey.Id;
            Key = secretKey.Key;
            KeyType = secretKey.KeyType;
        }

        public string Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Type => "SecretKeyUnregistered";
        public string SecretKeyId { get; set; }
        public string Key { get; set; }
        public string KeyType { get; set; }
    }
}
