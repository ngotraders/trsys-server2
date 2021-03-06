using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Trsys.CopyTrading.EaLogs;

namespace Trsys.CopyTrading.Tests
{
    [TestClass]
    public class EaLogParserTest
    {
        [TestMethod]
        public void PublisherTypicalLog_ShouldParsed()
        {
            var events = EaLogParser
                .Parse(
                    DateTimeOffset.Parse("2021-08-20T16:55:54.950Z"),
                    "KEY",
                    "Publisher",
                    "TOKEN",
                    "VERSION",
                    string.Join("\r\n", new[] {
                        "1629456515:DEBUG:Init",
                        "1629456515:DEBUG:Local order opened. LocalOrder = 0/27636811/GBPJPY.oj1m/1",
                        "1629456688:DEBUG:Local order closed. LocalOrder = 0/27636811/GBPJPY.oj1m/1",
                        "1629456952:DEBUG:Deinit. Reason = 3"
                    }))
                .ToArray();
            Assert.AreEqual(4, events.Count());
            Assert.AreEqual("InitLog", events[0].Type);
            Assert.AreEqual("LocalOrderOpenedLog", events[1].Type);
            Assert.AreEqual("LocalOrderClosedLog", events[2].Type);
            Assert.AreEqual("DeinitLog", events[3].Type);
        }
        [TestMethod]
        public void SubscriberTypicalLog_ShouldParsed()
        {
            var events = EaLogParser
                .Parse(
                    DateTimeOffset.Parse("2021-08-23T13:33:23.927Z"),
                    "KEY",
                    "Subscriber",
                    "TOKEN",
                    "VERSION",
                    string.Join("\r\n", new[] {
                        "1629381997:DEBUG:Init",
                        "1629456138:DEBUG:Server order opened. ServerOrder = 27636811/GBPJPY/1",
                        "1629456138:DEBUG:CalculateVolume: Symbol = GBPJPY.oj1m, Margin for a lot = 746305.00000000, Step = 0.01000000",
                        "1629456138:DEBUG:CalculateVolume: Free margin = 1965498.00000000, Leverage = 25, Percentage = 98.00000000, Calculated volume = 2.58000000",
                        "1629456138:DEBUG:OrderSend executing: ServerOrder = 27636811/GBPJPY/1, Calculated lots = 2.58000000",
                        "1629456139:INFO:OrderSend succeeded: 27636811, OrderTicket = 27636816",
                        "1629456139:DEBUG:Local order opened. LocalOrder = 27636811/27636816/GBPJPY.oj1m/1",
                        "1629456139:DEBUG:OPEN:27636811:GBPJPY.oj1m:1:27636816:27636816:GBPJPY.oj1m:1:149.24900000:2.58000000:1629456139",
                        "1629456688:DEBUG:Server order closed. ServerOrder = 27636811/GBPJPY/1",
                        "1629456688:DEBUG:OrderClose executing: 27636811, OrderTicket = 27636816",
                        "1629456688:INFO:OrderClose succeeded: 27636811, OrderTicket = 27636816",
                        "1629456688:DEBUG:Local order closed. LocalOrder = 27636811/27636816/GBPJPY.oj1m/1",
                        "1629456688:DEBUG:CLOSE:27636811:GBPJPY.oj1m:1:27636816:27636816:GBPJPY.oj1m:1:149.36700000:2.58000000:1629456689:-30444.00000000",
                        "1629704001:DEBUG:Deinit. Reason = 9"
                    }))
                .ToArray();
            Assert.AreEqual(14, events.Count());
            Assert.AreEqual("InitLog", events[0].Type);
            Assert.AreEqual("ServerOrderOpenedLog", events[1].Type);
            Assert.AreEqual("OrderSetupCurrencyInfoFetchedLog", events[2].Type);
            Assert.AreEqual("OrderSetupMarginCalculatedLog", events[3].Type);
            Assert.AreEqual("OrderSendExecutingLog", events[4].Type);
            Assert.AreEqual("OrderSendExecutionSuccessLog", events[5].Type);
            Assert.AreEqual("LocalOrderOpenedLog", events[6].Type);
            Assert.AreEqual("OrderOpenedLog", events[7].Type);
            Assert.AreEqual("ServerOrderClosedLog", events[8].Type);
            Assert.AreEqual("OrderCloseExecutingLog", events[9].Type);
            Assert.AreEqual("OrderCloseExecutionSuccessLog", events[10].Type);
            Assert.AreEqual("LocalOrderClosedLog", events[11].Type);
            Assert.AreEqual("OrderClosedLog", events[12].Type);
            Assert.AreEqual("DeinitLog", events[13].Type);
        }
    }
}
