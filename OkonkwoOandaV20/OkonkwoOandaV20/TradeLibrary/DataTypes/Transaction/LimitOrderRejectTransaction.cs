namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public class LimitOrderRejectTransaction : EntryOrderRejectTransaction
   {
      public decimal price { get; set; }
      public string gtdTime { get; set; }
      public string triggerCondition { get; set; }
      public long? intendedReplacesOrderID { get; set; }
   }
}
 