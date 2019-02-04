using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KrakenCoreClient.Models;

namespace KrakenCoreClient.Api.Kraken
{
    /// <summary>
    /// Interface for Kraken API client
    /// </summary>
    public interface IKrakenApiClient
    {
        /// <summary>
        /// Get the server time - This is to aid in approximating the skew time between the server and client.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetServerTime(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the asset info for the given assets
        /// </summary>
        /// <param name="assets">List of assets to get info on (optional. default = all for given asset class)</param>
        /// <param name="info">Info to retrieve (optional):
        /// Info = all info (default)</param>
        /// <param name="assetClass">Asset class (optional):
        /// Currency (default)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetAssetInfo(IEnumerable<string> assets, AssetInfo info = default,
            AssetClass assetClass = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the tradable asset pairs for the given pairs
        /// </summary>
        /// <param name="pairs">List of asset pairs to get info on (optional. default = all)</param>
        /// <param name="info">Info to retrieve (optional):
        /// Info = all info(default)
        /// Leverage = leverage info
        /// Fees = fees schedule
        /// Margin = margin info</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetTradableAssetPairs(IEnumerable<string> pairs,
            TradableAssetPairInfo info = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the ticker information for the given pairs
        /// </summary>
        /// <param name="pairs">List of asset pairs to get info on</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetTickerInformation(IEnumerable<string> pairs,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get OHLC data of a given pair
        /// </summary>
        /// <param name="pair">Asset pair to get OHLC data for</param>
        /// <param name="interval">Time frame interval in minutes (optional): 1 (default), 5, 15, 30, 60, 240, 1440, 10080, 21600</param>
        /// <param name="since">Return committed OHLC data since given id (optional. exclusive)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetOhlcData(string pair, int interval = 1, long? since = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the order book of a given pair
        /// </summary>
        /// <param name="pair">Asset pair to get market depth for</param>
        /// <param name="count">Maximum number of asks/bids (optional)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetOrderBook(string pair, int count = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the recent trade data of a given pair
        /// </summary>
        /// <param name="pair">Asset pair to get trade data for</param>
        /// <param name="since">Return trade data since given id (optional. exclusive)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetRecentTrades(string pair, long? since = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the recent spread data of a given pair
        /// </summary>
        /// <param name="pair">Asset pair to get spread data for</param>
        /// <param name="since">Return spread data since given id (optional. inclusive)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetRecentSpreadData(string pair, long? since = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of asset names and balance amount
        /// </summary>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetAccountBalance(int? otp = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of trade balance info
        /// </summary>
        /// <param name="assetClass">Asset class (optional):
        /// Currency (default)</param>
        /// <param name="asset">Base asset used to determine balance (default = ZUSD)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetTradeBalance(AssetClass assetClass = default, string asset = default,
            int? otp = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of order info with txid as the key
        /// </summary>
        /// <param name="trades">Whether or not to include trades related to position in output (optional. default = false)</param>
        /// <param name="userRef">Restrict results to given user reference id (optional)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetOpenOrders(bool trades = false, string userRef = default, int? otp = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of order info with txid as the key
        /// </summary>
        /// <param name="trades">Whether or not to include trades related to position in output (optional. default = false)</param>
        /// <param name="userRef">Restrict results to given user reference id (optional)</param>
        /// <param name="start">Starting unix timestamp or trade tx id of results (optional. exclusive)</param>
        /// <param name="end">Ending unix timestamp or trade tx id of results (optional. inclusive)</param>
        /// <param name="offset">Result offset</param>
        /// <param name="closeTime">Which time to use (optional)
        /// Open
        /// Close
        /// Both (default)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetClosedOrders(bool trades = false, string userRef = default, string start = default,
            string end = default, string offset = default, CloseTime closeTime = default, int? otp = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of order based on the given query
        /// </summary>
        /// <param name="trades">Whether or not to include trades related to position in output (optional. default = false)</param>
        /// <param name="userRef">Restrict results to given user reference id (optional)</param>
        /// <param name="txIds">List of transaction ids to query info about (20 maximum)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetQueryOrdersInfo(bool trades = false, string userRef = default,
            IEnumerable<string> txIds = default, int? otp = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of trade info
        /// </summary>
        /// <param name="tradeType">Type of trade (optional)
        /// All = all types (default)
        /// Any position = any position (open or closed)
        /// Closed position = positions that have been closed
        /// Closing position = any trade closing all or part of a position
        /// No position = non - positional trades</param>
        /// <param name="trades">Whether or not to include trades related to position in output (optional. default = false)</param>
        /// <param name="start">Starting unix timestamp or trade tx id of results (optional. exclusive)</param>
        /// <param name="end">Ending unix timestamp or trade tx id of results (optional. inclusive)</param>
        /// <param name="offset">Result offset</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetTradesHistory(TradeType tradeType = default, bool trades = false, string start = default,
            string end = default, string offset = default, int? otp = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of trades info
        /// </summary>
        /// <param name="txIds">List of transaction ids to query info about (20 maximum)</param>
        /// <param name="trades">Whether or not to include trades related to position in output (optional. default = false)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetQueryTradesInfo(IEnumerable<string> txIds, bool trades = false, int? otp = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of open position info
        /// </summary>
        /// <param name="txIds">List of transaction ids to restrict output to</param>
        /// <param name="doCalculations">Whether or not to include profit/loss calculations (optional. default = false)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetOpenPositions(IEnumerable<string> txIds, bool doCalculations = false, int? otp = default,
            CancellationToken cancellationToken = default);

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
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetLedgersInfo(AssetClass assetClass = default, IEnumerable<string> assets = default,
            LedgerType ledgerType = default, string start = default, string end = default, string offset = default,
            int? otp = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of ledgers info
        /// </summary>
        /// <param name="ids">List of ledger ids to query info about (20 maximum)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetQueryLedgers(IEnumerable<string> ids, int? otp = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get trade volume per given pair
        /// </summary>
        /// <param name="pairs">List of asset pairs to get fee info on (optional)</param>
        /// <param name="feeInfo">Whether or not to include fee info in results (optional)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> GetTradeVolume(IEnumerable<string> pairs = default, bool feeInfo = false, int? otp = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Add a standard order
        /// Note:
        /// See Get tradable asset pairs for specifications on asset pair prices, lots, and leverage.
        /// Prices can be preceded by +, -, or # to signify the price as a relative amount (with the exception of trailing stops, which are always relative). + adds the amount to the current offered price. - subtracts the amount from the current offered price. # will either add or subtract the amount to the current offered price, depending on the type and order type used. Relative prices can be suffixed with a % to signify the relative amount as a percentage of the offered price.
        /// For orders using leverage, 0 can be used for the volume to auto-fill the volume needed to close out your position.
        /// If you receive the error "EOrder:Trading agreement required", refer to your API key management page for further details.
        /// </summary>
        /// <param name="order">Standard order, which contains all parameters needed to add a standard order</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> AddStandardOrder(StandardOrder order, int? otp = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancel an open order
        /// Note: txid may be a user reference id.
        /// </summary>
        /// <param name="txId">Transaction id</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>KrakenResult</returns>
        Task<KrakenResult> CancelOpenOrder(string txId, int? otp = default,
            CancellationToken cancellationToken = default);
    }
}