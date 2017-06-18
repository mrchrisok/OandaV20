using System;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Pricing
{
   public class Price
   {
      public string type { get; set; }
      public string instrument { get; set; }
      public string time { get; set; }
      [Obsolete("Deprecated: Will be removed in a future API update.", true)]
      public string status { get; set; }
      public bool tradeable { get; set; }
      public List<PriceBucket> bids { get; set; }
      public List<PriceBucket> asks { get; set; }
      public double closeoutBid { get; set; }
      public double closeoutAsk { get; set; }
      [Obsolete("Deprecated: Will be removed in a future API update.", true)]
      public QuoteHomeConversionFactors quoteHomeConversionFactors { get; set; }
      [Obsolete("Deprecated: Will be removed in a future API update.", true)]
      public UnitsAvailable unitsAvailable { get; set; }
   }
}
