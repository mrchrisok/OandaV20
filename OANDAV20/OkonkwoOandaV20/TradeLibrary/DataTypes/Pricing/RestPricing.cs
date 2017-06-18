using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Pricing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkonkwoOandaV20
{
   public partial class Rest20
   {
      /// <summary>
      /// Retrieves the list of prices of the provided instrument names
      /// http://developer.oanda.com/rest-live-v20/pricing-ep/
      /// </summary>
      /// <param name="account">the account to retrieve the list for</param>
      /// <param name="instruments">the instruments to retrieve prices for</param>
      /// <param name="requestParams">optional additional parameters for the request (name, value pairs)</param>
      /// <returns>List of Price objects (or empty list, if no orders)</returns>
      public static async Task<List<Price>> GetPriceListAsync(string account, List<string> instruments, Dictionary<string, string> requestParams = null)
      {
         string requestString = Server(EServer.Account) + "accounts/" + account + "/pricing";

         // instruments should only be in the list
         if (requestParams != null)
         {
            if (requestParams.Count > 0)
               if (requestParams.ContainsKey("instruments")) requestParams.Remove("instruments");
         }

         if (instruments == null)
            throw new ArgumentException("List of instruments cannot be null.");

         string instrumentsParam = GetCommaSeparatedList(instruments);
         requestString += "?instruments=" + Uri.EscapeDataString(instrumentsParam);

         PricingResponse response = await MakeRequestAsync<PricingResponse>(requestString, "GET", requestParams);

         var prices = new List<Price>();
         prices.AddRange(response.prices);

         return prices;
      }
   }
}
