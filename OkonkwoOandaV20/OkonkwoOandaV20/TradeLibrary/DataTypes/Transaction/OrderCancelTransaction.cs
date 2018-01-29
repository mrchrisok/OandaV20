namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   /// <summary>
   /// An OrderCancelTransaction represents the cancellation of an Order in the client’s Account.
   /// </summary>
   public class OrderCancelTransaction : Transaction
   {
      /// <summary>
      /// The ID of the Order cancelled
      /// </summary>
      public long orderID { get; set; }

      /// <summary>
      /// The client ID of the Order cancelled (only provided if the Order has a client Order ID).
      /// </summary>
      public string clientOrderID { get; set; }

      /// <summary>
      /// The reason that the Order was cancelled.
      /// </summary>
      public string reason { get; set; }

      /// <summary>
      /// The ID of the Order that replaced this Order (only provided if this Order was cancelled for replacement).
      /// </summary>
      public long? replacedByOrderID { get; set; }
   }
}
