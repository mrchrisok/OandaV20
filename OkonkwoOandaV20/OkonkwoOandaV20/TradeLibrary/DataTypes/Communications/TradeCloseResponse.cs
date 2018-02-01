using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class TradeCloseResponse : Response
   {
      public MarketOrderTransaction orderCreateTransaction { get; set; }
      public OrderFillTransaction orderFillTransaction { get; set; }
      public OrderCancelTransaction orderCancelTransaction { get; set; }
   }
}
