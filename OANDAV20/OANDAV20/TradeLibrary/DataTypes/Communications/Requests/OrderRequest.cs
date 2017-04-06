using OANDAV20.TradeLibrary.DataTypes.Order;
using OANDAV20.TradeLibrary.DataTypes.Transaction;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Requests
{
   public abstract class OrderRequest : Request
   {
      public OrderRequest()
      {
         timeInForce = TimeInForce.FillOrKill;
         positionFill = OrderPositionFill.Default;
      }

      public string type { get; set; }
      public string instrument { get; set; }
      public double units { get; set; }
      public string timeInForce { get; set; }
      public double? priceBound { get; set; }
      public string positionFill { get; set; }
      public ClientExtensions clientExtensions { get; set; }
      public TakeProfitDetails takeProfitOnFill { get; set; }
      public StopLossDetails stopLossOnFill { get; set; }
      public TrailingStopLossDetails trailingStopLossOnFill { get; set; }
      public ClientExtensions tradeClientExtensions { get; set; }
   }
}
