using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KrakenCoreClient.Models;

namespace KrakenCoreClient
{
    public interface IKrakenClient
    {
        /// <summary>
        /// Get the server time
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>ServerTime</returns>
        Task<ServerTime> GetServerTime(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of asset names and their info
        /// </summary>
        /// <param name="assets">List of assets to get info on (optional. default = all for given asset class)</param>
        /// <param name="info">Info to retrieve (optional):
        /// Info = all info(default)</param>
        /// <param name="assetClass">Asset class (optional):
        /// Currency (default)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of assets</returns>
        Task<IEnumerable<Asset>> GetAssetInfo(IEnumerable<string> assets, AssetInfo info = default,
            AssetClass assetClass = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of pair names and their info
        /// </summary>
        /// <param name="pairs">List of asset pairs to get info on (optional.  default = all)</param>
        /// <param name="info">Info to retrieve (optional):
        /// Info = all info(default)
        /// Leverage = leverage info
        /// Fees = fees schedule
        /// Margin = margin info</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of tradable asset pairs</returns>
        Task<IEnumerable<ITradableAssetPair>> GetTradableAssetPairs(IEnumerable<string> pairs,
            TradableAssetPairInfo info = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of pair names and their ticker info
        /// </summary>
        /// <param name="pairs">List of asset pairs to get info on</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of ticker information</returns>
        Task<IEnumerable<TickerInformation>> GetTickerInformation(IEnumerable<string> pairs,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of pair name and OHLC data
        /// </summary>
        /// <param name="pair">The asset pair to get OHLC data for</param>
        /// <param name="interval">time frame interval in minutes (optional): 1 (default), 5, 15, 30, 60, 240, 1440, 10080, 21600</param>
        /// <param name="since">Return committed OHLC data since given id (optional. exclusive)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>OHLC data</returns>
        Task<OhlcData> GetOhlcData(string pair, int interval = 1, long? since = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of pair name and market depth
        /// </summary>
        /// <param name="pair">Asset pair to get market depth for</param>
        /// <param name="count">maximum number of asks/bids (optional)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Order book</returns>
        Task<OrderBook> GetOrderBook(string pair, int count = 100, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of pair name and recent trade data
        /// </summary>
        /// <param name="pair">Asset pair to get trade data for</param>
        /// <param name="since">Return trade data since given id (optional. exclusive)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Recent trades</returns>
        Task<RecentTrades> GetRecentTrades(string pair, long? since = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of pair name and recent spread data
        /// </summary>
        /// <param name="pair">Asset pair to get spread data for</param>
        /// <param name="since">Return spread data since given id (optional. inclusive)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Recent spread data</returns>
        Task<RecentSpreadData> GetRecentSpreadData(string pair, long? since = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of asset names and balance amount
        /// </summary>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of asset balance</returns>
        Task<IEnumerable<AssetBalance>> GetAccountBalance(int? otp = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of trade balance info
        /// </summary>
        /// <param name="assetClass">Asset class (optional):
        /// Currency (default)</param>
        /// <param name="asset">Base asset used to determine balance (default = ZUSD)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Trade balance info</returns>
        Task<TradeBalanceInfo> GetTradeBalance(AssetClass assetClass = default, string asset = default,
            int? otp = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of order info in open array with txid as the key
        /// </summary>
        /// <param name="trades">Whether or not to include trades in output (optional. default = false)</param>
        /// <param name="userRef">Restrict results to given user reference id (optional)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of order</returns>
        Task<IEnumerable<Order>> GetOpenOrders(bool trades = false, string userRef = default, int? otp = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of order info
        /// </summary>
        /// <param name="trades">Whether or not to include trades in output (optional. default = false)</param>
        /// <param name="userRef">Restrict results to given user reference id (optional)</param>
        /// <param name="start">Starting unix timestamp or order tx id of results (optional. exclusive)</param>
        /// <param name="end">Ending unix timestamp or order tx id of results (optional. inclusive)</param>
        /// <param name="offset">Result offset</param>
        /// <param name="closeTime">Which time to use (optional)
        /// Open
        /// Close
        /// Both (default)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of order</returns>
        Task<PageResult<Order>> GetClosedOrders(bool trades = false, string userRef = default,
            string start = default, string end = default, string offset = default, CloseTime closeTime = default,
            int? otp = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of orders info
        /// </summary>
        /// <param name="trades">Whether or not to include trades in output (optional. default = false)</param>
        /// <param name="userRef">Restrict results to given user reference id (optional)</param>
        /// <param name="txIds">List of transaction ids to query info about (20 maximum)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of order</returns>
        Task<IEnumerable<Order>> GetQueryOrdersInfo(bool trades = false, string userRef = default,
            IEnumerable<string> txIds = default, int? otp = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of of trade info
        /// </summary>
        /// <param name="tradeType">Type of trade (optional)
        /// All = all types(default)
        /// Any position = any position(open or closed)
        /// Closed position = positions that have been closed
        /// Closing position = any trade closing all or part of a position
        /// No position = non - positional trades</param>
        /// <param name="trades">Whether or not to include trades related to position in output (optional. default = false)</param>
        /// <param name="start">Starting unix timestamp or trade tx id of results (optional. exclusive)</param>
        /// <param name="end">Ending unix timestamp or trade tx id of results (optional. inclusive)</param>
        /// <param name="offset">Result offset</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of trade</returns>
        Task<PageResult<Trade>> GetTradesHistory(TradeType tradeType, bool trades = false, string start = default,
            string end = default, string offset = default, int? otp = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of trades info
        /// </summary>
        /// <param name="txIds">List of transaction ids to query info about (20 maximum)</param>
        /// <param name="trades">Whether or not to include trades related to position in output (optional. default = false)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of trade</returns>
        Task<IEnumerable<Trade>> GetQueryTradesInfo(IEnumerable<string> txIds, bool trades = false, int? otp = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of open position info
        /// </summary>
        /// <param name="txIds">List of transaction ids to restrict output to</param>
        /// <param name="doCalculations">Whether or not to include profit/loss calculations (optional. default = false)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of position</returns>
        Task<IEnumerable<Position>> GetOpenPositions(IEnumerable<string> txIds, bool doCalculations = false,
            int? otp = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of ledgers info
        /// </summary>
        /// <param name="assetClass">Asset class (optional):
        /// Currency (default)</param>
        /// <param name="assets">List of assets to restrict output to (optional. default = all)</param>
        /// <param name="ledgerType">Type of ledger to retrieve (optional):
        /// All (default)
        /// Deposit
        /// Withdrawal
        /// Trade
        /// Margin</param>
        /// <param name="start">Starting unix timestamp or ledger id of results (optional. exclusive)</param>
        /// <param name="end">Ending unix timestamp or ledger id of results (optional. inclusive)</param>
        /// <param name="offset">Result offset</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of ledger</returns>
        Task<IEnumerable<Ledger>> GetLedgersInfo(AssetClass assetClass = default, IEnumerable<string> assets = default,
            LedgerType ledgerType = default, string start = default, string end = default, string offset = default,
            int? otp = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of ledgers info
        /// </summary>
        /// <param name="ids">List of ledger ids to query info about (20 maximum)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of ledger</returns>
        Task<IEnumerable<Ledger>> GetQueryLedgers(IEnumerable<string> ids, int? otp = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get trade volume
        /// </summary>
        /// <param name="pairs">List of asset pairs to get fee info on (optional)</param>
        /// <param name="feeInfo">Whether or not to include fee info in results (optional)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Trade volume</returns>
        Task<TradeVolume> GetTradeVolume(IEnumerable<string> pairs = default, bool feeInfo = false,
            int? otp = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add standard order
        /// </summary>
        /// <param name="order">Standard order, see: StandardOrder</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Order result</returns>
        Task<AddOrderResult> AddStandardOrder(StandardOrder order, int? otp = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancel an open order
        /// </summary>
        /// <param name="txId">Transaction Id</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Cancel order result</returns>
        Task<CancelOrderResult> CancelOpenOrder(string txId, int? otp = default,
            CancellationToken cancellationToken = default);
    }
}