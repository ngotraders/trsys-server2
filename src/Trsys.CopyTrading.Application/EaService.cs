﻿using System;
using System.Threading.Tasks;
using Trsys.CopyTrading.Abstractions;
using Trsys.CopyTrading.EaLogs;

namespace Trsys.CopyTrading.Application
{
    public class EaService : IEaService
    {
        private readonly IEaStore eaStore;
        private readonly IEaLogAnalyzer eaLogAnalyzer;
        private readonly OrderNotificationBus orderBus;

        public EaService(IEaStore eaStore, IEaLogAnalyzer eaLogAnalyzer, OrderNotificationBus orderBus)
        {
            this.eaStore = eaStore;
            this.eaLogAnalyzer = eaLogAnalyzer;
            this.orderBus = orderBus;
        }

        public Task AddSecretKeyAsync(string key, string keyType)
        {
            eaStore.Add(key, keyType);
            return Task.CompletedTask;
        }

        public Task RemvoeSecretKeyAsync(string key, string keyType)
        {
            eaStore.Remove(key, keyType);
            return Task.CompletedTask;
        }

        public Task<EaSession> GenerateSessionTokenAsync(string key, string keyType)
        {
            var ea = eaStore.Find(key, keyType);
            if (ea == null)
            {
                return Task.FromResult(default(EaSession));
            }
            return Task.FromResult(ea.GenerateSession());
        }

        public Task DiscardSessionTokenAsync(string token, string key, string keyType)
        {
            var ea = eaStore.Find(key, keyType);
            if (ea == null)
            {
                throw new EaSessionTokenNotFoundException();
            }
            ea.DiscardSession(token);
            return Task.CompletedTask;
        }

        public Task ValidateSessionTokenAsync(string token, string key, string keyType)
        {
            var ea = eaStore.Find(key, keyType);
            if (ea == null)
            {
                throw new EaSessionTokenNotFoundException();
            }
            ea.ValidateSession(token);
            return Task.CompletedTask;
        }

        public Task PublishOrderTextAsync(string key, string text)
        {
            var publisher = eaStore.Find(key, "Publisher") as Publisher;
            if (publisher == null)
            {
                throw new EaSessionTokenNotFoundException();
            }
            publisher.UpdateOrderText(text);
            return Task.CompletedTask;
        }

        public Task<OrderText> GetOrderTextAsync(string key)
        {
            var subscriber = eaStore.Find(key, "Subscriber") as Subscriber;
            if (subscriber == null)
            {
                throw new EaSessionTokenNotFoundException();
            }
            return Task.FromResult(subscriber.GetOrderText());
        }

        public Task ReceiveLogAsync(DateTimeOffset timestamp, string key, string keyType, string version, string token, string text)
        {
            eaLogAnalyzer.AnalyzeLog(timestamp, key, keyType, version, token, text);
            return Task.CompletedTask;
        }

        public Task ReceiveLogAsync(DateTimeOffset serverTimestamp, long eaTimestamp, string key, string keyType, string version, string token, string text)
        {
            eaLogAnalyzer.AnalyzeLog(serverTimestamp, eaTimestamp, key, keyType, version, token, text);
            return Task.CompletedTask;
        }

        public void SubscribeSubscriberOrderUpdate(Action<string, OrderText> handler)
        {
            orderBus.AddSubscriberOrderUpdateHandler(handler);
        }

        public void UnsubscribeSubscriberOrderUpdate(Action<string, OrderText> handler)
        {
            orderBus.RemoveSubscriberOrderUpdateHandler(handler);
        }
    }
}
