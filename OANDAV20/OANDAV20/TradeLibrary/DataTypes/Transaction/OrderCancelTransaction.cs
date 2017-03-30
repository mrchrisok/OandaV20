namespace OANDAV20.TradeLibrary.DataTypes.Communications.Transaction
{
   public class OrderCancelTransaction : Transaction
   {
      public string type { get; set; }
      public long orderID { get; set; }
      public string clientOrderID { get; set; }
      public string reason { get; set; }
      public long? replacedByOrderID { get; set; }
   }
}
