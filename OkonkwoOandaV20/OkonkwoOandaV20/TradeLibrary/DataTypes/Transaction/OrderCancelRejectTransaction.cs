namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class OrderCancelRejectTransaction : Transaction
   {
      public long orderID { get; set; }
      public string clientOrderID { get; set; }
      public string reason { get; set; }
   }
}
