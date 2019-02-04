using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using KrakenCoreClient.Api.Kraken;
using KrakenCoreClient.Factories;
using KrakenCoreClient.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;

namespace KrakenCoreClient.UnitTests
{
    [TestClass]
    public class KrakenClientTests
    {
        private Mock<IKrakenApiClient> krakenApiClientMock;
        private ModelFactory modelFactory;

        [TestInitialize]
        public void Initialize()
        {
            krakenApiClientMock = new Mock<IKrakenApiClient>();
            modelFactory = new ModelFactory();
        }

        [TestMethod]
        public async Task GetServerTime_ApiReturnsNull_ResultsInNull()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetServerTime(It.IsAny<CancellationToken>())).ReturnsAsync(krakenResult);
            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            ServerTime result = await krakenClient.GetServerTime();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task GetServerTime_ApiReturnsResult_ResultsInServerTime()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("unixtime", 1547240383),
                    new JProperty("rfc1123", "Fri, 11 Jan 19 20:59:43 +0000")
                )
            };

            krakenApiClientMock.Setup(x => x.GetServerTime(It.IsAny<CancellationToken>())).ReturnsAsync(krakenResult);
            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            ServerTime result = await krakenClient.GetServerTime();

            // Assert
            result.UnixTime.Should().Be(1547240383);
            result.Rfc1123.Should().Be("Fri, 11 Jan 19 20:59:43 +0000");
        }

        [TestMethod]
        public async Task GetAssetInfo_ApiReturnsNull_ResultsInEmpty()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetAssetInfo(
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<AssetInfo>(),
                It.IsAny<AssetClass>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            var result = await krakenClient.GetAssetInfo(new List<string> {"XRP"}, AssetInfo.Info, AssetClass.Currency,
                CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetAssetInfo_ApiReturnsResult_ResultsInListOfAsset()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("ADA", new JObject(
                        new JProperty("aclass", "currency"),
                        new JProperty("altname", "ADA"),
                        new JProperty("decimals", 8),
                        new JProperty("display_decimals", 6))
                    ),
                    new JProperty("BCH", new JObject(
                        new JProperty("aclass", "currency"),
                        new JProperty("altname", "BCH"),
                        new JProperty("decimals", 10),
                        new JProperty("display_decimals", 5))
                    )
                )
            };

            krakenApiClientMock.Setup(x => x.GetAssetInfo(
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<AssetInfo>(),
                It.IsAny<AssetClass>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            IEnumerable<Asset> result = await krakenClient.GetAssetInfo(null, AssetInfo.Info, AssetClass.Currency,
                CancellationToken.None);

            // Assert
            var list = result.ToList();
            list.Count.Should().Be(2);
            list[0].Name.Should().Be("ADA");
            list[0].Decimals.Should().Be(8);
            list[1].AltName.Should().Be("BCH");
            list[1].DisplayDecimals.Should().Be(5);
        }

        [TestMethod]
        public async Task GetTradableAssetPairs_ApiReturnsNull_ResultsInEmpty()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetTradableAssetPairs(
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<TradableAssetPairInfo>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            var result = await krakenClient.GetTradableAssetPairs(new List<string> {"XRPUSD"},
                TradableAssetPairInfo.Info, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetTradableAssetPairs_ApiReturnsResult_ResultsInListOfAssetPair()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("XXBTZEUR", new JObject(
                            new JProperty("altname", "XBTEUR"),
                            new JProperty("aclass_base", "currency"),
                            new JProperty("base", "XXBT"),
                            new JProperty("aclass_quote", "currency"),
                            new JProperty("quote", "ZEUR"),
                            new JProperty("lot", "unit"),
                            new JProperty("pair_decimals", 1),
                            new JProperty("lot_decimals", 8),
                            new JProperty("lot_multiplier", 1),
                            new JProperty("leverage_buy", new[] {2, 3, 4, 5}),
                            new JProperty("leverage_sell", new[] {2, 3, 4, 5}),
                            new JProperty("fees", new JArray(
                                new JArray(new JValue(0), new JValue(0.26)),
                                new JArray(new JValue(50000), new JValue(0.24))
                            )),
                            new JProperty("fees_maker", new JArray(
                                new JArray(new JValue(0), new JValue(0.16)),
                                new JArray(new JValue(50000), new JValue(0.14))
                            )),
                            new JProperty("fee_volume_currency", "ZUSD"),
                            new JProperty("margin_call", 80),
                            new JProperty("margin_stop", 40)
                        )
                    )
                )
            };

            krakenApiClientMock.Setup(x => x.GetTradableAssetPairs(
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<TradableAssetPairInfo>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            IEnumerable<ITradableAssetPair> result = await krakenClient.GetTradableAssetPairs(
                new List<string> {"XBTEUR"}, TradableAssetPairInfo.Info, CancellationToken.None);

            // Assert
            var list = result.ToList();
            list.Count.Should().Be(1);
            list[0].Should().BeOfType<TradableAssetPair>();
            var item = (TradableAssetPair) list[0];
            item.Pair.Should().Be("XXBTZEUR");
            item.AltName.Should().Be("XBTEUR");
            item.AClassBase.Should().Be("currency");
            item.Base.Should().Be("XXBT");
            item.AClassQuote.Should().Be("currency");
            item.Quote.Should().Be("ZEUR");
            item.Lot.Should().Be("unit");
            item.PairDecimals.Should().Be(1);
            item.LotDecimals.Should().Be(8);
            item.LotMultiplier.Should().Be(1);
            item.LeverageBuy.Should().BeEquivalentTo(new[] {2, 3, 4, 5});
            item.LeverageSell.Should().BeEquivalentTo(new[] {2, 3, 4, 5});
            item.Fees.Should().BeEquivalentTo(new Fee {Percentage = 0.26M, Volume = 0},
                new Fee {Percentage = 0.24M, Volume = 50000});
            item.FeesMaker.Should().BeEquivalentTo(new Fee {Percentage = 0.16M, Volume = 0},
                new Fee {Percentage = 0.14M, Volume = 50000});
            item.FeeVolumeCurrency.Should().Be("ZUSD");
            item.MarginCall.Should().Be(80);
            item.MarginStop.Should().Be(40);
        }

        [TestMethod]
        public async Task GetTickerInformation_ApiReturnsNull_ResultsInEmpty()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetTickerInformation(
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            var result = await krakenClient.GetTickerInformation(new List<string> {"XRPUSD"},
                CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetTickerInformation_ApiReturnsResult_ResultsInListOfTickerInformation()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("XXBTZEUR", new JObject(
                            new JProperty("a",
                                new JArray(new JValue("5587.50000"), new JValue("1"), new JValue("1.000"))),
                            new JProperty("b",
                                new JArray(new JValue("5586.10000"), new JValue("2"), new JValue("2.000"))),
                            new JProperty("c", new JArray(new JValue("5587.50000"), new JValue("0.00837600"))),
                            new JProperty("v", new JArray(new JValue("875.63592145"), new JValue("966.13127300"))),
                            new JProperty("p", new JArray(new JValue("5587.34273"), new JValue("5589.03167"))),
                            new JProperty("t", new JArray(new JValue(6593), new JValue(7157))),
                            new JProperty("l", new JArray(new JValue("5541.90000"), new JValue("5541.90000"))),
                            new JProperty("h", new JArray(new JValue("5611.50000"), new JValue("5611.50000"))),
                            new JProperty("o", "5596.80000")
                        )
                    )
                )
            };

            krakenApiClientMock.Setup(x => x.GetTickerInformation(
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            IEnumerable<TickerInformation> result = await krakenClient.GetTickerInformation(new List<string> {"XRPUSD"},
                CancellationToken.None);

            // Assert
            var list = result.ToList();
            list.Count.Should().Be(1);
            list[0].Pair.Should().Be("XXBTZEUR");
            list[0].Ask.Price.Should().Be(5587.5M);
            list[0].Ask.WholeLotVolume.Should().Be(1);
            list[0].Ask.LotVolume.Should().Be(1);
            list[0].Bid.Price.Should().Be(5586.1M);
            list[0].Bid.WholeLotVolume.Should().Be(2);
            list[0].Bid.LotVolume.Should().Be(2);
            list[0].LastTradeClosed.Price.Should().Be(5587.5M);
            list[0].LastTradeClosed.LotVolume.Should().Be(0.008376M);
            list[0].Volume.Today.Should().Be(875.63592145M);
            list[0].Volume.Last24Hours.Should().Be(966.13127300M);
            list[0].VolumeWeightedAveragePrice.Today.Should().Be(5587.34273M);
            list[0].VolumeWeightedAveragePrice.Last24Hours.Should().Be(5589.03167M);
            list[0].NumberOfTrades.Today.Should().Be(6593);
            list[0].NumberOfTrades.Last24Hours.Should().Be(7157);
            list[0].Low.Today.Should().Be(5541.9M);
            list[0].Low.Last24Hours.Should().Be(5541.9M);
            list[0].High.Today.Should().Be(5611.5M);
            list[0].High.Last24Hours.Should().Be(5611.5M);
            list[0].OpeningPriceToday.Should().Be(5596.8M);
        }

        [TestMethod]
        public async Task GetOhlcData_ApiReturnsNull_ResultsInNull()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetOhlcData(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<long?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            var result = await krakenClient.GetOhlcData("XRPUSD", 1, null, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task GetOhlcData_ApiReturnsResult_ResultsInOhlcData()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("XXBTZEUR", new JArray(
                            new JArray(new JValue(1541408400), new JValue("5612.9"), new JValue("5612.9"),
                                new JValue("5612.3"), new JValue("5612.9"), new JValue("5612.8"),
                                new JValue("2.18613410"), new JValue(8)),
                            new JArray(new JValue(1541408460), new JValue("5612.9"), new JValue("5613.5"),
                                new JValue("5612.9"), new JValue("5613.5"), new JValue("5613.4"),
                                new JValue("1.01039730"), new JValue(11))
                        )
                    ),
                    new JProperty("last", 1541451480)
                )
            };

            krakenApiClientMock.Setup(x => x.GetOhlcData(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<long?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            OhlcData result = await krakenClient.GetOhlcData("XBTEUR", 1, null, CancellationToken.None);

            // Assert
            result.Pair.Should().Be("XXBTZEUR");
            result.Last.Should().Be(1541451480);
            result.Entries.Count().Should().Be(2);
            var list = result.Entries.ToList();
            list[0].Time.Should().Be(1541408400);
            list[0].Open.Should().Be(5612.9M);
            list[0].High.Should().Be(5612.9M);
            list[0].Low.Should().Be(5612.3M);
            list[0].Close.Should().Be(5612.9M);
            list[0].VSwap.Should().Be(5612.8M);
            list[0].Volume.Should().Be(2.1861341M);
            list[0].Count.Should().Be(8);
        }

        [TestMethod]
        public async Task GetOrderBook_ApiReturnsNull_ResultsInNull()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetOrderBook(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            var result = await krakenClient.GetOrderBook("XRPUSD", 100, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task GetOrderBook_ApiReturnsResult_ResultsInOrderBook()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("XXBTZEUR", new JObject(
                            new JProperty("asks", new JArray(
                                    new JArray(new JValue("5610"), new JValue("1.891"), new JValue(1541927221)),
                                    new JArray(new JValue("5610.8"), new JValue("1.268"), new JValue(1541927181))
                                )
                            ),
                            new JProperty("bids", new JArray(
                                    new JArray(new JValue("5609.4"), new JValue("0.061"), new JValue(1541927226)),
                                    new JArray(new JValue("5609.3"), new JValue("0.313"), new JValue(1541927218))
                                )
                            )
                        )
                    )
                )
            };

            krakenApiClientMock.Setup(x => x.GetOrderBook(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            OrderBook result = await krakenClient.GetOrderBook("XBTEUR", 100, CancellationToken.None);

            // Assert
            result.Pair.Should().Be("XXBTZEUR");
            var asks = result.Asks.ToList();
            var bids = result.Bids.ToList();
            asks[0].Price.Should().Be(5610);
            asks[0].Volume.Should().Be(1.891M);
            asks[0].Time.Should().Be(1541927221);
            asks[1].Price.Should().Be(5610.8M);
            asks[1].Volume.Should().Be(1.268M);
            asks[1].Time.Should().Be(1541927181);
            bids[0].Price.Should().Be(5609.4M);
            bids[0].Volume.Should().Be(0.061M);
            bids[0].Time.Should().Be(1541927226);
            bids[1].Price.Should().Be(5609.3M);
            bids[1].Volume.Should().Be(0.313M);
            bids[1].Time.Should().Be(1541927218);
        }

        [TestMethod]
        public async Task GetRecentTrades_ApiReturnsNull_ResultsInNull()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetRecentTrades(
                It.IsAny<string>(),
                It.IsAny<long?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            var result = await krakenClient.GetRecentTrades("XRPUSD", null, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task GetRecentTrades_ApiReturnsResult_ResultsInRecentTrades()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("XXBTZEUR", new JArray(
                            new JArray(
                                new JValue("5579.7"), new JValue("0.01855925"), new JValue(1541959740.9836),
                                new JValue("b"), new JValue("m"), new JValue("record 1")
                            ),
                            new JArray(
                                new JValue("5579.7"), new JValue("0.00790000"), new JValue(1541959751.1274),
                                new JValue("b"), new JValue("l"), new JValue("record 2")
                            )
                        )
                    ),
                    new JProperty("last", new JValue("1541971123812228024"))
                )
            };

            krakenApiClientMock.Setup(x => x.GetRecentTrades(
                It.IsAny<string>(),
                It.IsAny<long?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            RecentTrades result = await krakenClient.GetRecentTrades("XBTEUR", null, CancellationToken.None);

            // Assert
            result.Pair.Should().Be("XXBTZEUR");
            result.Last.Should().Be(1541971123812228024);
            var trades = result.Trades.ToList();
            trades[0].Price.Should().Be(5579.7M);
            trades[0].Volume.Should().Be(0.01855925M);
            trades[0].Time.Should().Be(1541959740.9836F);
            trades[0].BuySell.Should().Be(BuyOrSell.Buy);
            trades[0].MarketLimit.Should().Be(MarketOrLimit.Market);
            trades[0].Miscellaneous.Should().Be("record 1");
            trades[1].Price.Should().Be(5579.7M);
            trades[1].Volume.Should().Be(0.00790000M);
            trades[1].Time.Should().Be(1541959751.1274F);
            trades[1].BuySell.Should().Be(BuyOrSell.Buy);
            trades[1].MarketLimit.Should().Be(MarketOrLimit.Limit);
            trades[1].Miscellaneous.Should().Be("record 2");
        }

        [TestMethod]
        public async Task GetRecentSpreadData_ApiReturnsNull_ResultsInNull()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetRecentSpreadData(
                It.IsAny<string>(),
                It.IsAny<long?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            var result = await krakenClient.GetRecentSpreadData("XRPEUR", null, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task GetRecentSpreadData_ApiReturnsResult_ResultsInRecentSpreadData()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("XXBTZEUR", new JArray(
                            new JArray(
                                new JValue(1541951020), new JValue("5555.1"), new JValue("5556.9")
                            ),
                            new JArray(
                                new JValue(1541951021), new JValue("5555.2"), new JValue("5556.9")
                            )
                        )
                    ),
                    new JProperty("last", new JValue("1541951806"))
                )
            };

            krakenApiClientMock.Setup(x => x.GetRecentSpreadData(
                It.IsAny<string>(),
                It.IsAny<long?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            RecentSpreadData result = await krakenClient.GetRecentSpreadData("XBTEUR", null, CancellationToken.None);

            // Assert
            result.Pair.Should().Be("XXBTZEUR");
            result.Last.Should().Be(1541951806);
            var spreadData = result.Entries.ToList();
            spreadData[0].Time.Should().Be(1541951020);
            spreadData[0].Bid.Should().Be(5555.1M);
            spreadData[0].Ask.Should().Be(5556.9M);
            spreadData[1].Time.Should().Be(1541951021);
            spreadData[1].Bid.Should().Be(5555.2M);
            spreadData[1].Ask.Should().Be(5556.9M);
        }

        [TestMethod]
        public async Task GetAccountBalance_ApiReturnsNull_ResultsInEmpty()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetAccountBalance(
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            var result = await krakenClient.GetAccountBalance(null, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetAccountBalance_ApiReturnsResult_ResultsInAccountBalance()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("ZEUR", new JValue("-37.1429")),
                    new JProperty("XXRP", new JValue("112.00000051")),
                    new JProperty("XLTC", new JValue("1.0000011800")),
                    new JProperty("XXLM", new JValue("108.83178117")),
                    new JProperty("BCH", new JValue("0.0000036410"))
                )
            };

            krakenApiClientMock.Setup(x => x.GetAccountBalance(
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            var result = await krakenClient.GetAccountBalance(null, CancellationToken.None);

            // Assert
            var balance = result.ToList();
            balance[0].Name.Should().Be("ZEUR");
            balance[0].Balance.Should().Be(-37.1429M);
            balance[1].Name.Should().Be("XXRP");
            balance[1].Balance.Should().Be(112.00000051M);
            balance[2].Name.Should().Be("XLTC");
            balance[2].Balance.Should().Be(1.0000011800M);
            balance[3].Name.Should().Be("XXLM");
            balance[3].Balance.Should().Be(108.83178117M);
            balance[4].Name.Should().Be("BCH");
            balance[4].Balance.Should().Be(0.0000036410M);
        }

        [TestMethod]
        public async Task GetTradeBalance_ApiReturnsNull_ResultsInNull()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetTradeBalance(
                It.IsAny<AssetClass>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            var result = await krakenClient.GetTradeBalance(AssetClass.Currency, "XXRP", null, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task GetTradeBalance_ApiReturnsResult_ResultsInTradeBalanceInfo()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("eb", new JValue("-18.1507")),
                    new JProperty("tb", new JValue("-42.4431")),
                    new JProperty("m", new JValue("0.0000")),
                    new JProperty("n", new JValue("0.0000")),
                    new JProperty("c", new JValue("0.0000")),
                    new JProperty("v", new JValue("0.0000")),
                    new JProperty("e", new JValue("-42.4431")),
                    new JProperty("mf", new JValue("-42.4431"))
                )
            };

            krakenApiClientMock.Setup(x => x.GetTradeBalance(
                It.IsAny<AssetClass>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            TradeBalanceInfo result =
                await krakenClient.GetTradeBalance(AssetClass.Currency, "XXRP", null, CancellationToken.None);

            // Assert
            result.EquivalentBalance.Should().Be(-18.1507M);
            result.TradeBalance.Should().Be(-42.4431M);
            result.MarginAmount.Should().Be(0);
            result.UnrealizedNetResult.Should().Be(0);
            result.CostBasis.Should().Be(0);
            result.CurrentFloatingValuation.Should().Be(0);
            result.Equity.Should().Be(-42.4431M);
            result.FreeMargin.Should().Be(-42.4431M);
            result.MarginLevel.Should().Be(0);
        }

        [TestMethod]
        public async Task GetOpenOrders_ApiReturnsNull_ResultsInEmpty()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetOpenOrders(
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            var result = await krakenClient.GetOpenOrders(true, "123", null, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetOpenOrders_ApiReturnsResult_ResultsInOpenOrders()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("open", new JObject(
                            new JProperty("OHKQFZ-ALIP3-SBVVCF", new JObject(
                                    new JProperty("refid", null),
                                    new JProperty("userref", "123"),
                                    new JProperty("status", "open"),
                                    new JProperty("opentm", 1388937107.352),
                                    new JProperty("starttm", 0),
                                    new JProperty("expiretm", 0),
                                    new JProperty("descr", new JObject(
                                            new JProperty("pair", "XBTEUR"),
                                            new JProperty("type", "buy"),
                                            new JProperty("ordertype", "limit"),
                                            new JProperty("price", "0.75000"),
                                            new JProperty("price2", "0"),
                                            new JProperty("leverage", "none"),
                                            new JProperty("order", "buy 2.00000000 XBTEUR @ limit 0.75000")
                                        )
                                    ),
                                    new JProperty("vol", "2.00000000"),
                                    new JProperty("vol_exec", "0.00000000"),
                                    new JProperty("cost", "0.00000"),
                                    new JProperty("fee", "0.00000"),
                                    new JProperty("price", "0.00000"),
                                    new JProperty("misc", "test1,test2"),
                                    new JProperty("oflags", "")
                                )
                            )
                        )
                    )
                )
            };

            krakenApiClientMock.Setup(x => x.GetOpenOrders(
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            var result = await krakenClient.GetOpenOrders(true, "123", null, CancellationToken.None);

            // Assert
            var items = result.ToList();
            items[0].TxId.Should().Be("OHKQFZ-ALIP3-SBVVCF");
            items[0].RefId.Should().BeNull();
            items[0].UserRef.Should().Be("123");
            items[0].Status.Should().Be(OrderStatus.Open);
            items[0].OpenTm.Should().Be(1388937107.352M);
            items[0].StartTm.Should().Be(0);
            items[0].ExpireTm.Should().Be(0);
            items[0].Description.Pair.Should().Be("XBTEUR");
            items[0].Description.Type.Should().Be(BuyOrSell.Buy);
            items[0].Description.OrderType.Should().Be(OrderType.Limit);
            items[0].Description.Price.Should().Be(0.75M);
            items[0].Description.Price2.Should().Be(0);
            items[0].Description.Leverage.Should().Be("none");
            items[0].Description.Order.Should().Be("buy 2.00000000 XBTEUR @ limit 0.75000");
            items[0].Volume.Should().Be(2);
            items[0].VolumeExecuted.Should().Be(0);
            items[0].Cost.Should().Be(0);
            items[0].Fee.Should().Be(0);
            items[0].Price.Should().Be(0);
            items[0].Miscellaneous.First().Should().Be("test1");
            items[0].OrderFlags.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetClosedOrders_ApiReturnsNull_ResultsInEmpty()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetClosedOrders(
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CloseTime>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            PageResult<Order> result = await krakenClient.GetClosedOrders(true, "123", null, null, null, CloseTime.Both, null,
                CancellationToken.None);

            // Assert
            result.Count.Should().Be(0);
            result.Items.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetClosedOrders_ApiReturnsResult_ResultsInPageResultWithOrders()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("closed", new JObject(
                            new JProperty("OB4AOS-ORFJ5-7XTKIW", new JObject(
                                    new JProperty("refid", null),
                                    new JProperty("userref", "456"),
                                    new JProperty("status", "canceled"),
                                    new JProperty("reason", "Insufficient funds"),
                                    new JProperty("opentm", 1515428424.7618),
                                    new JProperty("closetm", 1515428427.0137),
                                    new JProperty("starttm", 0),
                                    new JProperty("expiretm", 0),
                                    new JProperty("descr", new JObject(
                                            new JProperty("pair", "XLMXBT"),
                                            new JProperty("type", "buy"),
                                            new JProperty("ordertype", "market"),
                                            new JProperty("price", "0"),
                                            new JProperty("price2", "0"),
                                            new JProperty("leverage", "none"),
                                            new JProperty("order", "buy 9900.00000000 XLMXBT @ market")
                                        )
                                    ),
                                    new JProperty("vol", "9900.00000000"),
                                    new JProperty("vol_exec", "9445.70570765"),
                                    new JProperty("cost", "0.40710991"),
                                    new JProperty("fee", "0.00105848"),
                                    new JProperty("price", "0.00004310"),
                                    new JProperty("misc", "Canceled for a reason"),
                                    new JProperty("oflags", "fiqc")
                                )
                            )
                        )
                    ),
                    new JProperty("count", 1)
                )
            };

            krakenApiClientMock.Setup(x => x.GetClosedOrders(
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CloseTime>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            PageResult<Order> result = await krakenClient.GetClosedOrders(true, "456", null, null, null,
                CloseTime.Close, null, CancellationToken.None);

            // Assert
            result.Count.Should().Be(1);
            result.Items.Count().Should().Be(1);
            var items = result.Items.ToList();
            items[0].TxId.Should().Be("OB4AOS-ORFJ5-7XTKIW");
            items[0].RefId.Should().BeNull();
            items[0].UserRef.Should().Be("456");
            items[0].Status.Should().Be(OrderStatus.Canceled);
            items[0].Reason.Should().Be("Insufficient funds");
            items[0].OpenTm.Should().Be(1515428424.7618M);
            items[0].CloseTm.Should().Be(1515428427.0137M);
            items[0].StartTm.Should().Be(0);
            items[0].ExpireTm.Should().Be(0);
            items[0].Description.Pair.Should().Be("XLMXBT");
            items[0].Description.Type.Should().Be(BuyOrSell.Buy);
            items[0].Description.OrderType.Should().Be(OrderType.Market);
            items[0].Description.Price.Should().Be(0);
            items[0].Description.Price2.Should().Be(0);
            items[0].Description.Leverage.Should().Be("none");
            items[0].Description.Order.Should().Be("buy 9900.00000000 XLMXBT @ market");
            items[0].Volume.Should().Be(9900);
            items[0].VolumeExecuted.Should().Be(9445.70570765M);
            items[0].Cost.Should().Be(0.40710991M);
            items[0].Fee.Should().Be(0.00105848M);
            items[0].Price.Should().Be(0.00004310M);
            items[0].Miscellaneous.First().Should().Be("Canceled for a reason");
            items[0].OrderFlags.First().Should().Be("fiqc");
        }

        [TestMethod]
        public async Task GetQueryOrdersInfo_ApiReturnsNull_ResultsInEmpty()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetQueryOrdersInfo(
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            var result = await krakenClient.GetQueryOrdersInfo(true, "111", null, null, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetQueryOrdersInfo_ApiReturnsResult_ResultsInResultWithOrders()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("OTIPZL-FY6PK-3SC3K4", new JObject(
                            new JProperty("refid", null),
                            new JProperty("userref", "111"),
                            new JProperty("status", "canceled"),
                            new JProperty("reason", "Insufficient funds"),
                            new JProperty("opentm", 1515428250.7142),
                            new JProperty("closetm", 1515428253.2732),
                            new JProperty("starttm", 0),
                            new JProperty("expiretm", 0),
                            new JProperty("descr", new JObject(
                                    new JProperty("pair", "XBTEUR"),
                                    new JProperty("type", "buy"),
                                    new JProperty("ordertype", "market"),
                                    new JProperty("price", "0"),
                                    new JProperty("price2", "0"),
                                    new JProperty("leverage", "none"),
                                    new JProperty("order", "buy 0.42000000 XBTEUR @ market")
                                )
                            ),
                            new JProperty("vol", "0.42000000"),
                            new JProperty("vol_exec", "0.40816385"),
                            new JProperty("cost", "5037.1"),
                            new JProperty("fee", "13.0"),
                            new JProperty("price", "12340.9"),
                            new JProperty("misc", "Canceled for a reason"),
                            new JProperty("oflags", "fciq"),
                            new JProperty("trades", new JArray(
                                new JValue("TEBBQT-2G6ZJ-SKN23V")))
                        )
                    )
                )
            };

            krakenApiClientMock.Setup(x => x.GetQueryOrdersInfo(
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            var result = await krakenClient.GetQueryOrdersInfo(true, "111", null, null, CancellationToken.None);

            // Assert
            var items = result.ToList();
            items[0].TxId.Should().Be("OTIPZL-FY6PK-3SC3K4");
            items[0].RefId.Should().BeNull();
            items[0].UserRef.Should().Be("111");
            items[0].Status.Should().Be(OrderStatus.Canceled);
            items[0].Reason.Should().Be("Insufficient funds");
            items[0].OpenTm.Should().Be(1515428250.7142M);
            items[0].CloseTm.Should().Be(1515428253.2732M);
            items[0].StartTm.Should().Be(0);
            items[0].ExpireTm.Should().Be(0);
            items[0].Description.Pair.Should().Be("XBTEUR");
            items[0].Description.Type.Should().Be(BuyOrSell.Buy);
            items[0].Description.OrderType.Should().Be(OrderType.Market);
            items[0].Description.Price.Should().Be(0);
            items[0].Description.Price2.Should().Be(0);
            items[0].Description.Leverage.Should().Be("none");
            items[0].Description.Order.Should().Be("buy 0.42000000 XBTEUR @ market");
            items[0].Volume.Should().Be(0.42M);
            items[0].VolumeExecuted.Should().Be(0.40816385M);
            items[0].Cost.Should().Be(5037.1M);
            items[0].Fee.Should().Be(13.0M);
            items[0].Price.Should().Be(12340.9M);
            items[0].Miscellaneous.First().Should().Be("Canceled for a reason");
            items[0].OrderFlags.First().Should().Be("fciq");
            items[0].Trades.First().Should().Be("TEBBQT-2G6ZJ-SKN23V");
        }

        [TestMethod]
        public async Task GetTradesHistory_ApiReturnsNull_ResultsInEmpty()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetTradesHistory(
                It.IsAny<TradeType>(),
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            PageResult<Trade> result = await krakenClient.GetTradesHistory(TradeType.All, false, null, null, null, null,
                CancellationToken.None);

            // Assert
            result.Count.Should().Be(0);
            result.Items.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetTradesHistory_ApiReturnsResult_ResultsInPageResultWithTrades()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("trades", new JObject(
                            new JProperty("TIUUA3-FZ35T-XHG6IK", new JObject(
                                    new JProperty("ordertxid", new JValue("OB4AOS-ORFJ5-7XTKIK")),
                                    new JProperty("pair", new JValue("XXLMXXBT")),
                                    new JProperty("time", new JValue(1515428427.0047)),
                                    new JProperty("type", new JValue("buy")),
                                    new JProperty("ordertype", new JValue("market")),
                                    new JProperty("price", new JValue("0.000043100")),
                                    new JProperty("cost", new JValue("0.407109916")),
                                    new JProperty("fee", new JValue("0.001058486")),
                                    new JProperty("vol", new JValue("9445.70570765")),
                                    new JProperty("margin", new JValue("0.000000000")),
                                    new JProperty("misc", new JValue("test"))
                                )
                            )
                        )
                    ),
                    new JProperty("count", new JValue(1))
                )
            };

            krakenApiClientMock.Setup(x => x.GetTradesHistory(
                It.IsAny<TradeType>(),
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            PageResult<Trade> result = await krakenClient.GetTradesHistory(TradeType.All, false, null, null, null, null,
                CancellationToken.None);

            // Assert
            result.Count.Should().Be(1);
            result.Items.Count().Should().Be(1);
            var items = result.Items.ToList();
            items[0].TxId.Should().Be("TIUUA3-FZ35T-XHG6IK");
            items[0].OrderTxId.Should().Be("OB4AOS-ORFJ5-7XTKIK");
            items[0].Pair.Should().Be("XXLMXXBT");
            items[0].Time.Should().Be(1515428427.0047M);
            items[0].Type.Should().Be(BuyOrSell.Buy);
            items[0].OrderType.Should().Be(OrderType.Market);
            items[0].Price.Should().Be(0.000043100M);
            items[0].Cost.Should().Be(0.407109916M);
            items[0].Fee.Should().Be(0.001058486M);
            items[0].Volume.Should().Be(9445.70570765M);
            items[0].Margin.Should().Be(0);
            items[0].Miscellaneous.First().Should().Be("test");
        }

        [TestMethod]
        public async Task GetQueryTradesInfo_ApiReturnsNull_ResultsInEmpty()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetQueryTradesInfo(
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<bool>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            IEnumerable<Trade> result =
                await krakenClient.GetQueryTradesInfo(new[] { "XBTEUR"}, false, null, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetQueryTradesInfo_ApiReturnsResult_ResultsInResultWithTrades()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("T4DTFO-NTSE6-SGXR7B", new JObject(
                            new JProperty("ordertxid", new JValue("OBNNPJ-DRXYC-KCRCAL")),
                            new JProperty("pair", new JValue("XETHZEUR")),
                            new JProperty("time", new JValue(1513626595.0437)),
                            new JProperty("type", new JValue("sell")),
                            new JProperty("ordertype", new JValue("market")),
                            new JProperty("price", new JValue("653.91000")),
                            new JProperty("cost", new JValue("122.70621")),
                            new JProperty("fee", new JValue("0.31904")),
                            new JProperty("vol", new JValue("0.18765000")),
                            new JProperty("margin", new JValue("0.000000000")),
                            new JProperty("misc", new JValue("test"))
                        )
                    )
                )
            };

            krakenApiClientMock.Setup(x => x.GetQueryTradesInfo(
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<bool>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            IEnumerable<Trade> result = await krakenClient.GetQueryTradesInfo(new[] { "T4DTFO-NTSE6-SGXR7B"},
                false, null, CancellationToken.None);

            // Assert
            var items = result.ToList();
            items[0].TxId.Should().Be("T4DTFO-NTSE6-SGXR7B");
            items[0].OrderTxId.Should().Be("OBNNPJ-DRXYC-KCRCAL");
            items[0].Pair.Should().Be("XETHZEUR");
            items[0].Time.Should().Be(1513626595.0437M);
            items[0].Type.Should().Be(BuyOrSell.Sell);
            items[0].OrderType.Should().Be(OrderType.Market);
            items[0].Price.Should().Be(653.91000M);
            items[0].Cost.Should().Be(122.70621M);
            items[0].Fee.Should().Be(0.31904M);
            items[0].Volume.Should().Be(0.18765000M);
            items[0].Margin.Should().Be(0);
            items[0].Miscellaneous.First().Should().Be("test");
        }

        [TestMethod]
        public async Task GetOpenPositions_ApiReturnsNull_ResultsInEmpty()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetOpenPositions(
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<bool>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            IEnumerable<Position> result =
                await krakenClient.GetOpenPositions(new[] { "XBTEUR"}, false, null, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetOpenPositions_ApiReturnsResult_ResultsInResultWithPositions()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("T4DTFO-KLED5-SGXR7B", new JObject(
                            new JProperty("ordertxid", new JValue("O5F3SX-LL35L-53GJOI")),
                            new JProperty("pair", new JValue("XXLMXXBT")),
                            new JProperty("time", new JValue(1511249270.9252)),
                            new JProperty("type", new JValue("buy")),
                            new JProperty("ordertype", new JValue("market")),
                            new JProperty("cost", new JValue("0.175725421")),
                            new JProperty("fee", new JValue("0.000456886")),
                            new JProperty("vol", new JValue("31212.33062934")),
                            new JProperty("vol_closed", new JValue("31213.33062934")),
                            new JProperty("margin", new JValue("0.000000000")),
                            new JProperty("value", new JValue("0.000000001")),
                            new JProperty("net", new JValue("0.000000002")),
                            new JProperty("misc", new JValue("test")),
                            new JProperty("oflags", new JValue("fiqc"))
                        )
                    )
                )
            };

            krakenApiClientMock.Setup(x => x.GetOpenPositions(
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<bool>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            IEnumerable<Position> result =
                await krakenClient.GetOpenPositions(new[] { "TQGPQB-YPVPM-AD6IAD" }, false, null, CancellationToken.None);

            // Assert
            var items = result.ToList();
            items[0].TxId.Should().Be("T4DTFO-KLED5-SGXR7B");
            items[0].OrderTxId.Should().Be("O5F3SX-LL35L-53GJOI");
            items[0].Pair.Should().Be("XXLMXXBT");
            items[0].Time.Should().Be(1511249270.9252M);
            items[0].Type.Should().Be(BuyOrSell.Buy);
            items[0].OrderType.Should().Be(OrderType.Market);
            items[0].Cost.Should().Be(0.175725421M);
            items[0].Fee.Should().Be(0.000456886M);
            items[0].Volume.Should().Be(31212.33062934M);
            items[0].VolumeClosed.Should().Be(31213.33062934M);
            items[0].Margin.Should().Be(0);
            items[0].Value.Should().Be(0.000000001M);
            items[0].Net.Should().Be(0.000000002M);
            items[0].Miscellaneous.First().Should().Be("test");
            items[0].OrderFlags.First().Should().Be("fiqc");
        }

        [TestMethod]
        public async Task GetLedgersInfo_ApiReturnsNull_ResultsInEmpty()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetLedgersInfo(
                It.IsAny<AssetClass>(),
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<LedgerType>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            IEnumerable<Ledger> result = await krakenClient.GetLedgersInfo(AssetClass.Currency, new[] {"XBT"},
                LedgerType.All, null, null, null, null, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetLedgersInfo_ApiReturnsResult_ResultsInResultWithLedgers()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("1", new JObject(
                            new JProperty("refid", new JValue("123")),
                            new JProperty("time", new JValue(1511149270.1252)),
                            new JProperty("type", new JValue("withdrawal")),
                            new JProperty("aclass", new JValue("currency")),
                            new JProperty("asset", new JValue("XBT")),
                            new JProperty("amount", new JValue("1.243761")),
                            new JProperty("fee", new JValue("0.000004000")),
                            new JProperty("balance", new JValue("1.440000"))
                        )
                    )
                )
            };

            krakenApiClientMock.Setup(x => x.GetLedgersInfo(
                It.IsAny<AssetClass>(),
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<LedgerType>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            IEnumerable<Ledger> result = await krakenClient.GetLedgersInfo(AssetClass.Currency, new[] { "XBT" },
                LedgerType.All, null, null, null, null, CancellationToken.None);

            // Assert
            var items = result.ToList();
            items[0].Id.Should().Be("1");
            items[0].RefId.Should().Be("123");
            items[0].Time.Should().Be(1511149270.1252M);
            items[0].Type.Should().Be(LedgerType.Withdrawal);
            items[0].AssetClass.Should().Be(AssetClass.Currency);
            items[0].Asset.Should().Be("XBT");
            items[0].Amount.Should().Be(1.243761M);
            items[0].Fee.Should().Be(0.000004000M);
            items[0].Balance.Should().Be(1.440000M);
        }

        [TestMethod]
        public async Task GetQueryLedgers_ApiReturnsNull_ResultsInEmpty()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetQueryLedgers(
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            IEnumerable<Ledger> result =
                await krakenClient.GetQueryLedgers(new[] {"XBT"}, null, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetQueryLedgers_ApiReturnsResult_ResultsInResultWithLedgers()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                        new JProperty("2", new JObject(
                                new JProperty("refid", new JValue("456")),
                                new JProperty("time", new JValue(1513149270.3252)),
                                new JProperty("type", new JValue("trade")),
                                new JProperty("aclass", new JValue("currency")),
                                new JProperty("asset", new JValue("XBT")),
                                new JProperty("amount", new JValue("2.143862")),
                                new JProperty("fee", new JValue("0.000004200")),
                                new JProperty("balance", new JValue("4.567800"))
                            )
                        )
                    )
                };

            krakenApiClientMock.Setup(x => x.GetQueryLedgers(
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            IEnumerable<Ledger> result =
                await krakenClient.GetQueryLedgers(new[] { "XBT" }, null, CancellationToken.None);

            // Assert
            var items = result.ToList();
            items[0].Id.Should().Be("2");
            items[0].RefId.Should().Be("456");
            items[0].Time.Should().Be(1513149270.3252M);
            items[0].Type.Should().Be(LedgerType.Trade);
            items[0].AssetClass.Should().Be(AssetClass.Currency);
            items[0].Asset.Should().Be("XBT");
            items[0].Amount.Should().Be(2.143862M);
            items[0].Fee.Should().Be(0.000004200M);
            items[0].Balance.Should().Be(4.567800M);
        }

        [TestMethod]
        public async Task GetTradeVolume_ApiReturnsNull_ResultsInNull()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.GetTradeVolume(
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<bool>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            TradeVolume result =
                await krakenClient.GetTradeVolume(new[] {"XLM"}, false, null, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task GetTradeVolume_ApiReturnsResult_ResultsInTradeVolume()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("currency", new JValue("ZUSD")),
                    new JProperty("volume", new JValue("1.0000")),
                    new JProperty("fees", new JObject(
                            new JProperty("XXBTZEUR", new JObject(
                                    new JProperty("fee", new JValue("0.2600")),
                                    new JProperty("minfee", new JValue("0.1000")),
                                    new JProperty("maxfee", new JValue("0.2600")),
                                    new JProperty("nextfee", new JValue("0.2400")),
                                    new JProperty("nextvolume", new JValue("50000.0000")),
                                    new JProperty("tiervolume", new JValue("0.0000"))
                                )
                            )
                        )
                    ),
                    new JProperty("fees_maker", new JObject(
                            new JProperty("XXBTZEUR", new JObject(
                                    new JProperty("fee", new JValue("0.1600")),
                                    new JProperty("minfee", new JValue("0.0000")),
                                    new JProperty("maxfee", new JValue("0.1600")),
                                    new JProperty("nextfee", new JValue("0.1400")),
                                    new JProperty("nextvolume", new JValue("50000.0000")),
                                    new JProperty("tiervolume", new JValue("0.0000"))
                                )
                            )
                        )
                    )
                )
            };

            krakenApiClientMock.Setup(x => x.GetTradeVolume(
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<bool>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            TradeVolume result =
                await krakenClient.GetTradeVolume(new[] { "XRP" }, false, null, CancellationToken.None);

            // Assert
            result.Currency.Should().Be("ZUSD");
            result.CurrentVolume.Should().Be(1);
            var fees = result.Fees.ToList();
            fees[0].Pair.Should().Be("XXBTZEUR");
            fees[0].Fee.Should().Be(0.26M);
            fees[0].MinimumFee.Should().Be(0.1M);
            fees[0].MaximumFee.Should().Be(0.26M);
            fees[0].NextFee.Should().Be(0.24M);
            fees[0].NextVolume.Should().Be(50000);
            fees[0].TierVolume.Should().Be(0);
            var feesMaker = result.FeesMaker.ToList();
            feesMaker[0].Pair.Should().Be("XXBTZEUR");
            feesMaker[0].Fee.Should().Be(0.16M);
            feesMaker[0].MinimumFee.Should().Be(0);
            feesMaker[0].MaximumFee.Should().Be(0.16M);
            feesMaker[0].NextFee.Should().Be(0.14M);
            feesMaker[0].NextVolume.Should().Be(50000);
            feesMaker[0].TierVolume.Should().Be(0);
        }

        [TestMethod]
        public async Task AddStandardOrder_ApiReturnsNull_ResultsInNull()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.AddStandardOrder(
                It.IsAny<StandardOrder>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            AddOrderResult result =
                await krakenClient.AddStandardOrder(null, null, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task AddStandardOrder_ApiReturnsResult_ResultsInAddOrderResult()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("descr", new JObject(
                            new JProperty("order", new JValue("buy 2000.00000000 XRPXBT @ market"))
                        )
                    )
                )
            };

            var standardOrder = new StandardOrder
            {
                Pair = "XBTXRP",
                Type = BuyOrSell.Buy,
                OrderType = OrderType.Market,
                Price = 2000,
                Price2 = 2002,
                Volume = 0.45M,
                Leverage = "none",
                OrderFlags = new[] {"fciq"},
                StartTm = 0,
                ExpireTm = 0,
                UserRef = Guid.NewGuid().ToString(),
                Validate = true
            };

            krakenApiClientMock.Setup(x => x.AddStandardOrder(
                It.IsAny<StandardOrder>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            AddOrderResult result =
                await krakenClient.AddStandardOrder(standardOrder, null, CancellationToken.None);

            // Assert
            result.Description.Close.Should().BeNull();
            result.Description.Order.Should().Be("buy 2000.00000000 XRPXBT @ market");
        }

        [TestMethod]
        public async Task CancelOpenOrder_ApiReturnsNull_ResultsInNull()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = null
            };

            krakenApiClientMock.Setup(x => x.CancelOpenOrder(
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            CancelOrderResult result = await krakenClient.CancelOpenOrder(null, null, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task CancelOpenOrder_ApiReturnsResult_ResultsInCancelOrderResult()
        {
            // Arrange
            var krakenResult = new KrakenResult
            {
                Result = new JObject(
                    new JProperty("count", new JValue("1"))
                )
            };

            krakenApiClientMock.Setup(x => x.CancelOpenOrder(
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(krakenResult);

            var krakenClient = new KrakenClient(krakenApiClientMock.Object, modelFactory);

            // Act
            CancelOrderResult result =
                await krakenClient.CancelOpenOrder("TEBBQT-2G6ZJ-SKN23V", null, CancellationToken.None);

            // Assert
            result.Count.Should().Be(1);
            result.Pending.Should().Be(0);
        }
    }
}