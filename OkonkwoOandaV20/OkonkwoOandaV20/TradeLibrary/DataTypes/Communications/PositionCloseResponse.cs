using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class PositionCloseResponse : Response
   {
      public MarketOrderTransaction longOrderCreateTransaction { get; set; }
      public OrderFillTransaction longOrderFillTransaction { get; set; }
      public OrderCancelTransaction longOrderCancelTransaction { get; set; }
      public MarketOrderTransaction shortOrderCreateTransaction { get; set; }
      public OrderFillTransaction shortOrderFillTransaction { get; set; }
      public OrderCancelTransaction shortOrderCancelTransaction { get; set; }
   }
}
