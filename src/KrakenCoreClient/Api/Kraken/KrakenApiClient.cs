using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using KrakenCoreClient.Models;
using Microsoft.Extensions.Configuration;

namespace KrakenCoreClient.Api.Kraken
{
    /// <inheritdoc cref="ApiClientBase" />
    /// <summary>
    /// Kraken API client implementation
    /// </summary>
    public sealed class KrakenApiClient : ApiClientBase, IKrakenApiClient
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="configuration"></param>
        public KrakenApiClient(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration) : base(
            httpClientFactory,
            configuration)
        {
            var version = configuration["KrakenApi:Version"];
            var baseUrl = configuration["KrakenApi:BaseUrl"];
            ContentType = configuration["KrakenApi:ContentType"];
            BaseUrl = $"{baseUrl}/{version}";
        }

        /// <inheritdoc />
        /// <summary>
        /// Get the server time - This is to aid in approximating the skew time between the server and client.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetServerTime(CancellationToken cancellationToken = default)
        {
            return await CallPublic("/public/Time", cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get the asset info for the given assets
        /// </summary>
        /// <param name="assets">List of assets to get info on (optional.  default = all for given asset class)</param>
        /// <param name="info">Info to retrieve (optional):
        /// Info = all info(default)</param>
        /// <param name="assetClass">Asset class (optional):
        /// Currency(default)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetAssetInfo(IEnumerable<string> assets = default, AssetInfo info = default,
            AssetClass assetClass = default, CancellationToken cancellationToken = default)
        {
            var queryStringList = new QueryStringList();
            queryStringList.Add("asset", assets);
            queryStringList.Add("info", info);
            queryStringList.Add("aclass", assetClass);

            return await CallPublic($"/public/Assets{queryStringList}", cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get the tradable asset pairs for the given pairs
        /// </summary>
        /// <param name="pairs">List of asset pairs to get info on (optional. default = all)</param>
        /// <param name="info">Info to retrieve (optional):
        /// Info = all info (default)
        /// Leverage = leverage info
        /// Fees = fees schedule
        /// Margin = margin info</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetTradableAssetPairs(IEnumerable<string> pairs = default,
            TradableAssetPairInfo info = default, CancellationToken cancellationToken = default)
        {
            var queryStringList = new QueryStringList();
            queryStringList.Add("pair", pairs);
            queryStringList.Add("info", info);

            return await CallPublic($"/public/AssetPairs{queryStringList}", cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get the ticker information for the given pairs
        /// </summary>
        /// <param name="pairs">List of asset pairs to get info on</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetTickerInformation(IEnumerable<string> pairs,
            CancellationToken cancellationToken = default)
        {
            var queryStringList = new QueryStringList();
            queryStringList.Add("pair", pairs);

            return await CallPublic($"/public/Ticker{queryStringList}", cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get an OHLC data of a given pair
        /// </summary>
        /// <param name="pair">Asset pair to get OHLC data for</param>
        /// <param name="interval">Time frame interval in minutes (optional): 1 (default), 5, 15, 30, 60, 240, 1440, 10080, 21600</param>
        /// <param name="since">Return committed OHLC data since given id (optional. exclusive)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetOhlcData(string pair, int interval = 1, long? since = default,
            CancellationToken cancellationToken = default)
        {
            var queryStringList = new QueryStringList();
            queryStringList.Add("pair", pair);
            queryStringList.Add("interval", interval);
            queryStringList.Add("since", since);

            return await CallPublic($"/public/OHLC{queryStringList}", cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get the order book of a given pair
        /// </summary>
        /// <param name="pair">Asset pair to get market depth for</param>
        /// <param name="count">Maximum number of asks/bids (optional)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetOrderBook(string pair, int count = 100,
            CancellationToken cancellationToken = default)
        {
            var queryStringList = new QueryStringList();
            queryStringList.Add("pair", pair);
            queryStringList.Add("count", count);

            return await CallPublic($"/public/Depth{queryStringList}", cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get the recent trade data of a given pair
        /// </summary>
        /// <param name="pair">Asset pair to get trade data for</param>
        /// <param name="since">Return trade data since given id (optional. exclusive)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetRecentTrades(string pair, long? since = default,
            CancellationToken cancellationToken = default)
        {
            var queryStringList = new QueryStringList();
            queryStringList.Add("pair", pair);
            queryStringList.Add("since", since);

            return await CallPublic($"/public/Trades{queryStringList}", cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get the recent spread data of a given pair
        /// </summary>
        /// <param name="pair">Asset pair to get spread data for</param>
        /// <param name="since">Return spread data since given id (optional. inclusive)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetRecentSpreadData(string pair, long? since = default,
            CancellationToken cancellationToken = default)
        {
            var queryStringList = new QueryStringList();
            queryStringList.Add("pair", pair);
            queryStringList.Add("since", since);

            return await CallPublic($"/public/Spread{queryStringList}", cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get an array of asset names and balance amount
        /// </summary>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetAccountBalance(int? otp = default,
            CancellationToken cancellationToken = default)
        {
            var postValueList = new PostValueList();
            postValueList.Add("otp", otp);

            return await CallPrivate("/private/Balance", postValueList, cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get an array of trade balance info
        /// </summary>
        /// <param name="assetClass">Asset class (optional):
        /// Currency(default)</param>
        /// <param name="asset">Base asset used to determine balance (default = ZUSD)</param>
        /// <param name="otp">Two-factor password (if two-factor enabled, otherwise not required)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetTradeBalance(AssetClass assetClass = default,
            string asset = default, int? otp = default, CancellationToken cancellationToken = default)
        {
            var postValueList = new PostValueList();
            postValueList.Add("aclass", assetClass);
            postValueList.Add("asset", asset);
            postValueList.Add("otp", otp);

            return await CallPrivate("/private/TradeBalance", postValueList, cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// Array of order info with txid as the key
        /// </summary>
        /// <param name="trades">Whether or not to include trades in output (optional.  default = false)</param>
        /// <param name="userRef">Restrict results to given user reference id (optional)</param>
        /// <param name="otp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetOpenOrders(bool trades = false, string userRef = default, int? otp = default,
            CancellationToken cancellationToken = default)
        {
            var postValueList = new PostValueList();
            postValueList.Add("trades", trades);
            postValueList.Add("userref", userRef);
            postValueList.Add("otp", otp);

            return await CallPrivate("/private/OpenOrders", postValueList, cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// Array of order info with txid as the key
        /// </summary>
        /// <param name="trades">Whether or not to include trades in output (optional.  default = false)</param>
        /// <param name="userRef">Restrict results to given user reference id (optional)</param>
        /// <param name="start">Starting unix timestamp or order tx id of results (optional. exclusive)</param>
        /// <param name="end">Ending unix timestamp or order tx id of results (optional. inclusive)</param>
        /// <param name="offset">Result offset</param>
        /// <param name="closeTime">Which time to use (optional)
        /// Open
        /// Close
        /// Both (default)</param>
        /// <param name="otp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetClosedOrders(bool trades = false, string userRef = default,
            string start = default, string end = default, string offset = default, CloseTime closeTime = default,
            int? otp = default, CancellationToken cancellationToken = default)
        {
            var postValueList = new PostValueList();
            postValueList.Add("trades", trades);
            postValueList.Add("userref", userRef);
            postValueList.Add("start", start);
            postValueList.Add("end", end);
            postValueList.Add("ofs", offset);
            postValueList.Add("closetime", closeTime);
            postValueList.Add("otp", otp);

            return await CallPrivate("/private/ClosedOrders", postValueList, cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// List of orders info
        /// </summary>
        /// <param name="trades">Whether or not to include trades in output (optional. default = false)</param>
        /// <param name="userRef">Restrict results to given user reference id (optional)</param>
        /// <param name="txIds">List of transaction ids to query info about (20 maximum)</param>
        /// <param name="otp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetQueryOrdersInfo(bool trades = false, string userRef = default,
            IEnumerable<string> txIds = default, int? otp = default, CancellationToken cancellationToken = default)
        {
            var postValueList = new PostValueList();
            postValueList.Add("trades", trades);
            postValueList.Add("userref", userRef);
            postValueList.Add("txid", txIds);
            postValueList.Add("otp", otp);

            return await CallPrivate("/private/QueryOrders", postValueList, cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// List of trade info
        /// </summary>
        /// <param name="tradeType">type of trade (optional)
        /// All = all types(default)
        /// Any position = any position(open or closed)
        /// Closed position = positions that have been closed
        /// Closing position = any trade closing all or part of a position
        /// No position = non - positional trades</param>
        /// <param name="trades">Whether or not to include trades related to position in output (optional. default = false)</param>
        /// <param name="start">Starting unix timestamp or trade tx id of results (optional. exclusive)</param>
        /// <param name="end">Ending unix timestamp or trade tx id of results (optional. inclusive)</param>
        /// <param name="offset">Result offset</param>
        /// <param name="otp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetTradesHistory(TradeType tradeType = default, bool trades = false,
            string start = default, string end = default, string offset = default, int? otp = default,
            CancellationToken cancellationToken = default)
        {
            var postValueList = new PostValueList();
            postValueList.Add("type", tradeType);
            postValueList.Add("trades", trades);
            postValueList.Add("start", start);
            postValueList.Add("end", end);
            postValueList.Add("ofs", offset);
            postValueList.Add("otp", otp);

            return await CallPrivate("/private/TradesHistory", postValueList, cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// List of trades info
        /// </summary>
        /// <param name="txIds">List of transaction ids to query info about (20 maximum)</param>
        /// <param name="trades">Whether or not to include trades related to position in output (optional. default = false)</param>
        /// <param name="otp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetQueryTradesInfo(IEnumerable<string> txIds, bool trades = false,
            int? otp = default, CancellationToken cancellationToken = default)
        {
            var postValueList = new PostValueList();
            postValueList.Add("txid", txIds);
            postValueList.Add("trades", trades);
            postValueList.Add("otp", otp);

            return await CallPrivate("/private/QueryTrades", postValueList, cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// List of open position info
        /// </summary>
        /// <param name="txIds">List of transaction ids to restrict output to</param>
        /// <param name="doCalculations">Whether or not to include profit/loss calculations (optional. default = false)</param>
        /// <param name="otp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetOpenPositions(IEnumerable<string> txIds, bool doCalculations = false,
            int? otp = default, CancellationToken cancellationToken = default)
        {
            var postValueList = new PostValueList();
            postValueList.Add("txid", txIds);
            postValueList.Add("docalcs", doCalculations);
            postValueList.Add("otp", otp);

            return await CallPrivate("/private/OpenPositions", postValueList, cancellationToken);
        }

        /// <inheritdoc />
        /// <summary>
        /// List of ledgers info
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
        /// <param name="otp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<KrakenResult> GetLedgersInfo(AssetClass assetClass = default,
            IEnumerable<string> assets = default, LedgerType ledgerType = default,
            string start = default, string end = default, string offset = default, int? otp = default,
            CancellationToken cancellationToken = default)
        {
            var postValueList = new PostValueList();
            postValueList.Add("aclass", assetClass);
            postValueList.Add("asset", assets);
            postValueList.Add("type", ledgerType);
            postValueList.Add("start", start);
            postValueList.Add("end", end);
            postValueList.Add("ofs", offset);
            postValueList.Add("otp", otp);

            return await CallPrivate("/private/Ledgers", postValueList, cancellationToken);
        }

        public async Task<KrakenResult> GetQueryLedgers(IEnumerable<string> ids, int? otp = default,
            CancellationToken cancellationToken = default)
        {
            var postValueList = new PostValueList();
            postValueList.Add("id", ids);
            postValueList.Add("otp", otp);

            return await CallPrivate("/private/QueryLedgers", postValueList, cancellationToken);
        }

        public async Task<KrakenResult> GetTradeVolume(IEnumerable<string> pairs = default, bool feeInfo = false,
            int? otp = default, CancellationToken cancellationToken = default)
        {
            var postValueList = new PostValueList();
            postValueList.Add("pair", pairs);
            postValueList.Add("fee-info", feeInfo);
            postValueList.Add("otp", otp);

            return await CallPrivate("/private/TradeVolume", postValueList, cancellationToken);
        }

        public async Task<KrakenResult> AddStandardOrder(StandardOrder order, int? otp = default,
            CancellationToken cancellationToken = default)
        {
            var postValueList = new PostValueList();
            postValueList.Add("pair", order.Pair);
            postValueList.Add("type", order.Type);
            postValueList.Add("ordertype", order.OrderType);
            postValueList.Add("price", order.Price);
            postValueList.Add("price2", order.Price2);
            postValueList.Add("volume", order.Volume);
            postValueList.Add("leverage", order.Leverage);
            postValueList.Add("oflags", order.OrderFlags);
            postValueList.Add("starttm", order.StartTm);
            postValueList.Add("expiretm", order.ExpireTm);
            postValueList.Add("userref", order.UserRef);
            postValueList.Add("validate", order.Validate);
            postValueList.Add("otp", otp);

            return await CallPrivate("/private/AddOrder", postValueList, cancellationToken);
        }

        public async Task<KrakenResult> CancelOpenOrder(string txId, int? otp = default,
            CancellationToken cancellationToken = default)
        {
            var postValueList = new PostValueList();
            postValueList.Add("txid", txId);
            postValueList.Add("otp", otp);

            return await CallPrivate("/private/CancelOrder", postValueList, cancellationToken);
        }

        #region Private methods

        private async Task<KrakenResult> CallPublic(string url, CancellationToken cancellationToken)
        {
            using (var request = GetRequestMessage("GET", url))
            {
                var response = await GetResponseMessage(request, cancellationToken);

                return await HandleResponseMessage(response);
            }
        }

        private async Task<KrakenResult> CallPrivate(string url, PostValueList postData,
            CancellationToken cancellationToken)
        {
            using (var request = GetPrivateRequestMessage("POST", url, postData.ToString()))
            {
                var response = await GetResponseMessage(request, cancellationToken);

                return await HandleResponseMessage(response);
            }
        }

        #endregion
    }
}