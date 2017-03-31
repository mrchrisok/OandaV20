using OANDAV20.TradeLibrary.DataTypes.Order;
using OANDAV20.TradeLibrary.DataTypes.Transaction;
using System.ComponentModel;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Requests
{
   public class OrderRequest : Request
   {
      [DefaultValue(OrderType.Market)]
      public string type { get; set; }

      public string instrument { get; set; }
      public double units { get; set; }

      [DefaultValue(TimeInForce.FillOrKill)]
      public string timeInForce { get; set; }

      public double? priceBound { get; set; }

      [DefaultValue(OrderPositionFill.Default)]
      public string positionFill { get; set; }

      public ClientExtensions clientExtensions { get; set; }
      public TakeProfitDetails takeProfitOnFill { get; set; }
      public StopLossDetails stopLossOnFill { get; set; }
      public TrailingStopLossDetails trailingStopLossOnFill { get; set; }
      public ClientExtensions tradeClientExtensions { get; set; }
   }
}
