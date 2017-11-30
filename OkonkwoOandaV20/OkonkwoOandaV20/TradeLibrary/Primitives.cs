namespace OkonkwoOandaV20.TradeLibrary.Primitives
{
   public enum AcceptDatetimeFormat
   {
      Unix,
      RFC3339
   }

   public class InstrumentName
   {
      public class Currency
      {
         public const string EURUSD = "EUR_USD";
         public const string USDJPY = "USD_JPY";
         public const string AUDUSD = "AUD_USD";
         public const string AUDJPY = "AUD_JPY";
         public const string GBPUSD = "GBP_USD";
         public const string NZDUSD = "NZD_USD";
         public const string USDCHF = "USD_CHF";
         public const string USDCAD = "USD_CAD";
      }
   }
}
