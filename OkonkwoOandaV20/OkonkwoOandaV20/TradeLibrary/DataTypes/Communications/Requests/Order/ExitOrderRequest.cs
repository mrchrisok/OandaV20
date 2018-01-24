using Newtonsoft.Json;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order
{
   [JsonConverter(typeof(PriceObjectConverter))]
   public abstract class ExitOrderRequest : Request, IOrderRequest, IHasPrices
   {
      public ExitOrderRequest(Instrument.Instrument oandaInstrument)
      {
         priceInformation = new PriceInformation()
         {
            instrument = oandaInstrument,
            priceProperties = new List<string>() { "price" }
         };
      }

      public string type { get; set; }
      public decimal? price { get; set; }
      public long tradeID { get; set; }
      public string clientTradeID { get; set; }
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public string triggerCondition { get; set; }
      public ClientExtensions clientExtensions { get; set; }
      [JsonIgnore]
      public PriceInformation priceInformation { get ; set; }
   }
}
