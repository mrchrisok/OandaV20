namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction
{
   public abstract class EntryOrderRejectTransaction : EntryOrderTransaction
   {
      public string rejectReason { get; set; }
   }
}
