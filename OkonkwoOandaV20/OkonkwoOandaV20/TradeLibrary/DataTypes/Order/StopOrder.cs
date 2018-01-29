namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Order
{
   public class StopOrder : EntryOrder
   {
      public decimal price { get; set; }
      public decimal? priceBound { get; set; }
      public string gtdTime { get; set; }
      public string triggerCondition { get; set; }
      public long? replacesOrderID { get; set; }
      public long? replacedByOrderID { get; set; }
   }
}
