using System.Collections.Generic;
using KrakenCoreClient.Api.Kraken;
using KrakenCoreClient.Models;

namespace KrakenCoreClient.Factories
{
    public interface IModelFactory
    {
        ServerTime BuildServerTime(KrakenResult source);

        IEnumerable<Asset> BuildAssetInfo(KrakenResult source);

        IEnumerable<ITradableAssetPair> BuildTradableAssetPairs(KrakenResult source, TradableAssetPairInfo info);

        IEnumerable<TickerInformation> BuildTickerInformation(KrakenResult source);

        OhlcData BuildOhlcData(KrakenResult source);

        OrderBook BuildOrderBook(KrakenResult source);

        RecentTrades BuildRecentTrades(KrakenResult source);

        RecentSpreadData BuildRecentSpreadData(KrakenResult source);

        IEnumerable<AssetBalance> BuildAccountBalance(KrakenResult source);

        TradeBalanceInfo BuildTradeBalance(KrakenResult source);

        IEnumerable<Order> BuildOpenOrders(KrakenResult source);

        PageResult<Order> BuildClosedOrders(KrakenResult source);

        IEnumerable<Order> BuildQueryOrders(KrakenResult source);

        PageResult<Trade> BuildTradesHistory(KrakenResult source);

        IEnumerable<Trade> BuildQueryTradesInfo(KrakenResult source);

        IEnumerable<Position> BuildOpenPositions(KrakenResult source);

        IEnumerable<Ledger> BuildLedgersInfo(KrakenResult source);

        TradeVolume BuildTradeVolume(KrakenResult source);

        AddOrderResult BuildAddOrderResult(KrakenResult source);

        CancelOrderResult BuildCancelOrderResult(KrakenResult source);
    }
}