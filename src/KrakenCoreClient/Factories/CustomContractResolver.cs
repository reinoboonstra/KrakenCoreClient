using System.Collections.Generic;
using Newtonsoft.Json.Serialization;

namespace KrakenCoreClient.Factories
{
    public class CustomContractResolver : DefaultContractResolver
    {
        private Dictionary<string, string> PropertyMappings { get; }

        public CustomContractResolver()
        {
            PropertyMappings = new Dictionary<string, string>
            {
                {"DisplayDecimals", "display_decimals"},
                {"AClassBase", "aclass_base"},
                {"AClassQuote", "aclass_quote"},
                {"PairDecimals", "pair_decimals"},
                {"LotDecimals", "lot_decimals"},
                {"LotMultiplier", "lot_multiplier"},
                {"LeverageBuy", "leverage_buy"},
                {"LeverageSell", "leverage_sell"},
                {"FeesMaker", "fees_maker"},
                {"FeeVolumeCurrency", "fee_volume_currency"},
                {"MarginCall", "margin_call"},
                {"MarginStop", "margin_stop"},
                {"MarginLevel", "margin_level"},
                {"Description", "descr"},
                {"Volume", "vol"},
                {"CurrentVolume", "volume"},
                {"VolumeExecuted", "vol_exec"},
                {"VolumeClosed", "vol_closed"},
                {"Miscellaneous", "misc"},
                {"OrderFlags", "oflags"},
                {"PositionStatus", "posstatus"},
                {"ClosedPrice", "cprice"},
                {"ClosedCost", "ccost"},
                {"ClosedFee", "cfee"},
                {"ClosedVolume", "cvol"},
                {"ClosedMargin", "cmargin"},
                {"MinimumFee", "minfee"},
                {"MaximumFee", "maxfee"},
                {"TxIds", "txid"}
            };
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            var resolved = PropertyMappings.TryGetValue(propertyName, out var resolvedName);

            return resolved ? resolvedName : base.ResolvePropertyName(propertyName);
        }
    }
}