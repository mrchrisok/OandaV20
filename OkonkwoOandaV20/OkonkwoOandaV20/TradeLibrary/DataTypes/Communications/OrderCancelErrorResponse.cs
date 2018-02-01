using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/order-ep
   /// </summary>
   public class OrderCancelErrorResponse : ErrorResponse
   {
      public OrderCancelRejectTransaction orderCancelRejectTransaction { get; set; }
   }
}
