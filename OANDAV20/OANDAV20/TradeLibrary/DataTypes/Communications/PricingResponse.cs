using OANDAV20.TradeLibrary.DataTypes.Pricing;
using System.Collections.Generic;

namespace OANDAV20.TradeLibrary.DataTypes.Communications
{
   public class PricingResponse : Response
   {
      public List<Price> prices;
   }
}
