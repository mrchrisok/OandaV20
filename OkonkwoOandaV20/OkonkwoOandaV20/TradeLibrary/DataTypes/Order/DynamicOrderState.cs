namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Order
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/order-df/#DynamicOrderState
   /// </summary>
   public class DynamicOrderState
   {
      public long id { get; set; }
      public decimal trailingStopValue { get; set; }
      public decimal? triggerDistance { get; set; }
      public bool? isTriggerDistanceExact { get; set; }
   }
}
