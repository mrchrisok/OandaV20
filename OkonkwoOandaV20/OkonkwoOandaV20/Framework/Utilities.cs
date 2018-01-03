using OkonkwoOandaV20.TradeLibrary.Primitives;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.Framework
{
   public class Utilities
   {
      /// <summary>
      /// Determines if trading is halted for the provided instrument.
      /// </summary>
      /// <param name="instrument">Instrument to check if halted. Default is EUR_USD.</param>
      /// <returns>True if trading is halted, false if trading is not halted.</returns>
      public static async Task<bool> IsMarketHalted(string instrument = InstrumentName.Currency.EURUSD)
      {
         var accountId = Credentials.GetDefaultCredentials().DefaultAccountId;
         var prices = await Rest20.GetPriceListAsync(accountId, new List<string>() { instrument });

         bool isTradeable = false, hasBids = false, hasAsks = false;

         if (prices[0] != null)
         {
            isTradeable = prices[0].tradeable;
            hasBids = prices[0].bids.Count > 0;
            hasAsks = prices[0].asks.Count > 0;
         }

         return !(isTradeable && hasBids && hasAsks);
      }
   }
}
