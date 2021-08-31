﻿using System;

namespace Trsys.CopyTrading.Abstractions
{
    public class EaSessionGeneratedEvent : IEvent
    {
        public EaSessionGeneratedEvent()
        {
        }

        public EaSessionGeneratedEvent(EaSession session)
        {
            Id = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            EaSessionId = session.Id;
            Key = session.Key;
            KeyType = session.KeyType;
            Token = session.Token;
        }

        public string Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Type => "EaSessionGenerated";
        public string EaSessionId { get; set; }
        public string Key { get; set; }
        public string KeyType { get; set; }
        public string Token { get; set; }
    }
}