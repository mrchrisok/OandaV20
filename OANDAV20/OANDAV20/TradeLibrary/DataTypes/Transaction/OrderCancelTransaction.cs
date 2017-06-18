namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class OrderCancelTransaction : Transaction
   {
      public long orderID { get; set; }
      public string clientOrderID { get; set; }
      public string reason { get; set; }
      public long? replacedByOrderID { get; set; }
   }
}
