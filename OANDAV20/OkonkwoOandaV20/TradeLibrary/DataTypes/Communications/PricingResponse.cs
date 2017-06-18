using OkonkwoOandaV20.TradeLibrary.DataTypes.Pricing;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class PricingResponse : Response
   {
      public List<Price> prices;
   }
}
