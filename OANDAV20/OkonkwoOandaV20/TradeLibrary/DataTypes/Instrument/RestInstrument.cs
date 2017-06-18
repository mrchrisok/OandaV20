using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Instrument;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkonkwoOandaV20
{
   public partial class Rest20
   {
      /// <summary>
      /// Retrieves the list of candles available for the given instrument
      /// </summary>
      /// <param name="instrument">the instrument to retrieve candles for</param>
      /// <param name="requestParams">the parameters for the request</param>
      /// <returns>List of Candlestick objects (or empty list) </returns>
      public static async Task<List<CandlestickPlus>> GetCandlesAsync(string instrument, Dictionary<string, string> requestParams)
      {
         string requestString = Server(EServer.Account) + "instruments/" + instrument + "/candles";

         CandlesResponse response = await MakeRequestAsync<CandlesResponse>(requestString, "GET", requestParams);

         var candles = new List<CandlestickPlus>();
         foreach (var candle in response.candles)
         {
            candles.Add(new CandlestickPlus(candle) { instrument = instrument, granularity = response.granularity });
         }
         return candles;
      }
   }
}
