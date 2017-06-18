/// <summary>
/// http://developer.oanda.com/rest-live-v20/order-df/
/// </summary>
namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Order
{
   public class OrderPositionFill
   {
      public const string OpenOnly = "OPEN_ONLY";
      public const string ReduceFirst = "REDUCE_FIRST";
      public const string ReduceOnly = "REDUCE_ONLY";
      public const string Default = "DEFAULT";
   }

   public class OrderState
   {
      public const string Pending = "PENDING";
      public const string Filled = "FILLED";
      public const string Triggered = "TRIGGERED";
      public const string Cancelled = "CANCELLED";
   }

   public class OrderTriggerCondition
   {
      public const string Default = "DEFAULT";
      public const string Inverse = "INVERSE";
      public const string Bid = "BID";
      public const string Ask = "ASK";
      public const string Mid = "MID";
   }

   public class OrderType
   {
      public const string Market = "MARKET";
      public const string Limit = "LIMIT";
      public const string Stop = "STOP";
      public const string MarketIfTouched = "MARKET_IF_TOUCHED";
      public const string TakeProfit = "TAKE_PROFIT";
      public const string StopLoss = "STOP_LOSS";
      public const string TrailingStopLoss = "TRAILING_STOP_LOSS";
   }

   public class TimeInForce
   {
      public const string GoodUntilCancelled = "GTC";
      public const string GoodUntilDate = "GTD";
      public const string GoodForDay = "GFD";
      public const string FillOrKill = "FOK";
      public const string ImmediatelyOrCancelled = "IOC";
   }
}
