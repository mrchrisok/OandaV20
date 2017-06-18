using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order
{
   public abstract class ExitOrderRequest : Request, IOrderRequest
   {
      public string type { get; set; }
      public long tradeID { get; set; }
      public string clientTradeID { get; set; }
      public string timeInForce { get; set; }
      public string gtdTime { get; set; }
      public string triggerCondition { get; set; }
      public ClientExtensions clientExtensions { get; set; }
   }
}
