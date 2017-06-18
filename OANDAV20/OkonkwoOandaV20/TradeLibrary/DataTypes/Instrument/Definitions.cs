namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Instrument
{
   public class CandleStickGranularity
   {
      public const string Seconds05 = "S5";
      public const string Seconds10 = "S10";
      public const string Seconds15 = "S15";
      public const string Seconds30 = "S30";

      public const string Minutes01 = "M1";
      public const string Minutes02 = "M2";
      public const string Minutes04 = "M4";
      public const string Minutes05 = "M5";
      public const string Minutes10 = "M10";
      public const string Minutes15 = "M15";
      public const string Minutes30 = "M30";

      public const string Hours01 = "H1";
      public const string Hours02 = "H2";
      public const string Hours03 = "H3";
      public const string Hours04 = "H4";
      public const string Hours06 = "H6";
      public const string Hours08 = "H8";
      public const string Hours12 = "H12";

      public const string Daily = "D";
      public const string Weekly = "W";
      public const string Monthly = "M";
   }

   public class InstrumentType
   {
      public const string Currency = "CURRENCY";
      public const string ContractForDifference = "CFD";
      public const string Metal = "METAL";
   }

   public class WeeklyAlignment
   {
      public const string Monday = "Monday";
      public const string Tuesday = "Tuesday";
      public const string Wednesday = "Wednesday";
      public const string Thursday = "Thursday";
      public const string Friday = "Friday";
      public const string Saturday = "Saturday";
      public const string Sunday = "Sunday";
   }

   public class CandleStickPriceType
   {
      public const string Ask = "A";
      public const string Bid = "B";
      public const string Midpoint = "M";
   }
}
