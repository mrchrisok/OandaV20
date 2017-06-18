using OkonkwoOandaV20.TradeLibrary.DataTypes.Instrument;
using System.Collections.Generic;

namespace OkonkwoOandaV20.Framework
{
   public interface IHasPrices
   {
      PriceInformation priceInformation { get; set; }
   }

   public class PriceInformation
   {
      public Instrument instrument { get; set; }
      public List<string> priceProperties { get; set; }
   }
}
