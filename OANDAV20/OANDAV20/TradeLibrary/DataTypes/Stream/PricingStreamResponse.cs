using OANDAV20.TradeLibrary.DataTypes.Communications.Pricing;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Stream
{
   /// <summary>
   /// Events are authorized transactions posted to the subject account.
   /// For more information, visit: http://developer.oanda.com/rest-live-v20/transaction-ep/
   /// </summary>
   public class PricingStreamResponse : IHeartbeat
   {
      public PricingHeartbeat heartbeat { get; set; }
      public Pricing.Price price { get; set; }

      public bool IsHeartbeat()
      {
         return (heartbeat != null);
      }
   }
}
