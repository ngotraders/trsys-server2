﻿using System;
using Trsys.Events.Abstractions;

namespace Trsys.CopyTrading.Abstractions
{
    public class SubscriberOrderCloseDeliveredEvent : IEvent
    {
        public SubscriberOrderCloseDeliveredEvent()
        {
        }

        public SubscriberOrderCloseDeliveredEvent(SubscriberOrder subscriberOrder)
        {
            Id = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
            SubscriberKey = subscriberOrder.SubscriberKey;
            PublisherKey = subscriberOrder.PublisherKey;
            Text = subscriberOrder.Text;
            TicketNo = subscriberOrder.TicketNo;
            Symbol = subscriberOrder.Symbol;
            OrderType = subscriberOrder.OrderType;
            Price = subscriberOrder.Price;
            Lots = subscriberOrder.Lots;
            Time = subscriberOrder.Time;
        }

        public string Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Type => "SubscriberOrderCloseDelivered";
        public string SubscriberKey { get; set; }
        public string PublisherKey { get; set; }
        public string Text { get; set; }
        public int TicketNo { get; set; }
        public string Symbol { get; set; }
        public OrderType OrderType { get; set; }
        public decimal Price { get; set; }
        public decimal Lots { get; set; }
        public long Time { get; set; }
    }
}