using OANDAV20.TradeLibrary.DataTypes.Transaction;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Requests
{
   public class TrailingStopLossOrderRequest
   {
      public string type { get; set; }
      public long tradeID { get; set; }
      public string clientTradeID { get; set; }
      public double distance { get; set; }
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public string triggerCondition { get; set; }
      public ClientExtensions clientExtensions { get; set; }
   }
}
