using System;
using System.Collections.Generic;
using KrakenCoreClient.Api.Kraken;
using KrakenCoreClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KrakenCoreClient.Factories
{
    public class ModelFactory : IModelFactory
    {
        #region Private fields

        private readonly JsonSerializer serializer = new JsonSerializer
        {
            ContractResolver = new CustomContractResolver()
        };

        #endregion

        public ServerTime BuildServerTime(KrakenResult source)
        {
            return source.Result?.ToObject<ServerTime>(serializer);
        }

        public IEnumerable<Asset> BuildAssetInfo(KrakenResult source)
        {
            if (source.Result == null)
            {
                yield break;
            }

            foreach (var (key, value) in source.Result)
            {
                var asset = value.ToObject<Asset>(serializer);
                asset.Name = key;

                yield return asset;
            }
        }

        public IEnumerable<ITradableAssetPair> BuildTradableAssetPairs(KrakenResult source, TradableAssetPairInfo info)
        {
            if (source.Result == null)
            {
                yield break;
            }

            serializer.Converters.Add(new CustomJsonFeesConverter());

            foreach (var (key, value) in source.Result)
            {
                ITradableAssetPair pair;

                switch (info)
                {
                    case TradableAssetPairInfo.Fees:
                        pair = value.ToObject<TradableAssetPairFee>(serializer);
                        break;
                    case TradableAssetPairInfo.Leverage:
                        pair = value.ToObject<TradableAssetPairLeverage>(serializer);
                        break;
                    case TradableAssetPairInfo.Margin:
                        pair = value.ToObject<TradableAssetPairMargin>(serializer);
                        break;
                    default:
                        pair = value.ToObject<TradableAssetPair>(serializer);
                        break;
                }

                pair.Pair = key;

                yield return pair;
            }
        }

        public IEnumerable<TickerInformation> BuildTickerInformation(KrakenResult source)
        {
            if (source.Result == null)
            {
                yield break;
            }

            serializer.Converters.Add(new CustomJsonTickerInformationConverter());

            foreach (var (key, value) in source.Result)
            {
                var tickerInformation = value.ToObject<TickerInformation>(serializer);
                tickerInformation.Pair = key;

                yield return tickerInformation;
            }
        }

        public OhlcData BuildOhlcData(KrakenResult source)
        {
            if (source.Result == null)
            {
                return null;
            }

            serializer.Converters.Add(new CustomJsonOhlcDataConverter());

            return source.Result.ToObject<OhlcData>(serializer);
        }

        public OrderBook BuildOrderBook(KrakenResult source)
        {
            if (source.Result == null)
            {
                return null;
            }

            serializer.Converters.Add(new CustomJsonOrderConverter());

            foreach (var (key, value) in source.Result)
            {
                var orderBook = value.ToObject<OrderBook>(serializer);
                orderBook.Pair = key;

                return orderBook;
            }

            return null;
        }

        public RecentTrades BuildRecentTrades(KrakenResult source)
        {
            if (source.Result == null)
            {
                return null;
            }

            serializer.Converters.Add(new CustomJsonRecentTradesConverter());

            return source.Result.ToObject<RecentTrades>(serializer);
        }

        public RecentSpreadData BuildRecentSpreadData(KrakenResult source)
        {
            if (source.Result == null)
            {
                return null;
            }

            serializer.Converters.Add(new CustomJsonRecentSpreadDataConverter());

            return source.Result.ToObject<RecentSpreadData>(serializer);
        }

        public IEnumerable<AssetBalance> BuildAccountBalance(KrakenResult source)
        {
            if (source.Result == null)
            {
                yield break;
            }

            foreach (var (key, value) in source.Result)
            {
                yield return new AssetBalance
                {
                    Name = key,
                    Balance = Convert.ToDecimal(value)
                };
            }
        }

        public TradeBalanceInfo BuildTradeBalance(KrakenResult source)
        {
            if (source.Result == null)
            {
                return null;
            }

            return new TradeBalanceInfo
            {
                EquivalentBalance = Convert.ToDecimal(source.Result["eb"]),
                TradeBalance = Convert.ToDecimal(source.Result["tb"]),
                MarginAmount = Convert.ToDecimal(source.Result["m"]),
                UnrealizedNetResult = Convert.ToDecimal(source.Result["n"]),
                CostBasis = Convert.ToDecimal(source.Result["c"]),
                CurrentFloatingValuation = Convert.ToDecimal(source.Result["v"]),
                Equity = Convert.ToDecimal(source.Result["e"]),
                FreeMargin = Convert.ToDecimal(source.Result["mf"]),
                MarginLevel = Convert.ToDecimal(source.Result["ml"])
            };
        }

        public IEnumerable<Order> BuildOpenOrders(KrakenResult source)
        {
            if (source.Result == null)
            {
                yield break;
            }

            serializer.Converters.Add(new CustomJsonOrderDescriptionInfoConverter());
            serializer.Converters.Add(new CustomJsonListConverter());

            foreach (var item in source.Result.First.First)
            {
                var transaction = (JProperty) item;
                var order = transaction.Value.ToObject<Order>(serializer);
                order.TxId = transaction.Name;

                yield return order;
            }
        }

        public PageResult<Order> BuildClosedOrders(KrakenResult source)
        {
            if (source.Result == null)
            {
                return new PageResult<Order>();
            }

            serializer.Converters.Add(new CustomJsonOrderDescriptionInfoConverter());
            serializer.Converters.Add(new CustomJsonListConverter());
            var list = new List<Order>();
            var count = Convert.ToInt64(source.Result["count"]);

            foreach (var item in source.Result.First.First)
            {
                var transaction = (JProperty)item;
                var order = transaction.Value.ToObject<Order>(serializer);
                order.TxId = transaction.Name;
                list.Add(order);
            }

            return new PageResult<Order> {Items = list, Count = count};
        }

        public IEnumerable<Order> BuildQueryOrders(KrakenResult source)
        {
            if (source.Result == null)
            {
                yield break;
            }

            serializer.Converters.Add(new CustomJsonOrderDescriptionInfoConverter());
            serializer.Converters.Add(new CustomJsonListConverter());

            foreach (var (key, value) in source.Result)
            {
                var order = value.ToObject<Order>(serializer);
                order.TxId = key;

                yield return order;
            }
        }

        public PageResult<Trade> BuildTradesHistory(KrakenResult source)
        {
            if (source.Result == null)
            {
                return new PageResult<Trade>();
            }

            serializer.Converters.Add(new CustomJsonListConverter());
            var list = new List<Trade>();
            var count = Convert.ToInt64(source.Result["count"]);

            foreach (var item in source.Result.First.First)
            {
                var transaction = (JProperty)item;
                var trade = transaction.Value.ToObject<Trade>(serializer);
                trade.TxId = transaction.Name;
                list.Add(trade);
            }

            return new PageResult<Trade> {Items = list, Count = count};
        }

        public IEnumerable<Trade> BuildQueryTradesInfo(KrakenResult source)
        {
            if (source.Result == null)
            {
                yield break;
            }

            serializer.Converters.Add(new CustomJsonListConverter());

            foreach (var (key, value) in source.Result)
            {
                var trade = value.ToObject<Trade>(serializer);
                trade.TxId = key;

                yield return trade;
            }
        }

        public IEnumerable<Position> BuildOpenPositions(KrakenResult source)
        {
            if (source.Result == null)
            {
                yield break;
            }

            serializer.Converters.Add(new CustomJsonListConverter());

            foreach (var (key, value) in source.Result)
            {
                var position = value.ToObject<Position>(serializer);
                position.TxId = key;

                yield return position;
            }
        }

        public IEnumerable<Ledger> BuildLedgersInfo(KrakenResult source)
        {
            if (source.Result == null)
            {
                yield break;
            }

            foreach (var (key, value) in source.Result)
            {
                var ledger = value.ToObject<Ledger>(serializer);
                ledger.Id = key;

                yield return ledger;
            }
        }

        public TradeVolume BuildTradeVolume(KrakenResult source)
        {
            if (source.Result == null)
            {
                return null;
            }

            serializer.Converters.Add(new CustomJsonPairFeesConverter());

            return source.Result.ToObject<TradeVolume>(serializer);
        }

        public AddOrderResult BuildAddOrderResult(KrakenResult source)
        {
            if (source.Result == null)
            {
                return null;
            }

            serializer.Converters.Add(new CustomJsonListConverter());

            return source.Result.ToObject<AddOrderResult>(serializer);
        }

        public CancelOrderResult BuildCancelOrderResult(KrakenResult source)
        {
            return source.Result?.ToObject<CancelOrderResult>(serializer);
        }
    }
}