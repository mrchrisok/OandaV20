using Newtonsoft.Json;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order
{
   [JsonConverter(typeof(PriceObjectConverter))]
   public abstract class EntryOrderRequest : Request, IOrderRequest, IHasPrices
   {
      public EntryOrderRequest(Instrument.Instrument oandaInstrument)
      {
         instrument = oandaInstrument.name;
         timeInForce = TimeInForce.FillOrKill;
         positionFill = OrderPositionFill.Default;
         priceInformation = new PriceInformation()
         {
            instrument = oandaInstrument,
            priceProperties = new List<string>() { "price", "priceBound" }
         };
      }

      public string type { get; set; }
      public string instrument { get; set; }   
      public decimal units { get; set; }
      public string timeInForce { get; set; }
      public decimal? price { get; set; }
      public decimal? priceBound { get; set; }
      public string positionFill { get; set; }
      public ClientExtensions clientExtensions { get; set; }
      public TakeProfitDetails takeProfitOnFill { get; set; }
      public StopLossDetails stopLossOnFill { get; set; }
      public TrailingStopLossDetails trailingStopLossOnFill { get; set; }
      public ClientExtensions tradeClientExtensions { get; set; }
      [JsonIgnore]
      public PriceInformation priceInformation { get; set; }
   }
}
