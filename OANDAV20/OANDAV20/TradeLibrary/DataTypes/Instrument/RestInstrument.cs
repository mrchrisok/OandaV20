using OANDAV20.TradeLibrary.DataTypes.Communications.Communications;
using OANDAV20.TradeLibrary.DataTypes.Communications.Communications.Requests;
using OANDAV20.TradeLibrary.DataTypes.Communications.Instrument;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OANDAV20
{
   public partial class Rest20
   {
      /// <summary>
      /// Retrieves the list of candles available for the given instrument
      /// </summary>
      /// <param name="instrument">the instrument to retrieve candles for</param>
      /// <param name="requestParams">the parameters for the request</param>
      /// <returns>List of Candlestick objects (or empty list) </returns>
      public static async Task<List<Candlestick>> GetCandlesAsync(string instrument, Dictionary<string, string> requestParams)
      {
         string requestString = Server(EServer.Account) + "instruments/" + instrument + "/candles";

         CandlesResponse response = await MakeRequestAsync<CandlesResponse>(requestString, "GET", requestParams);

         var candles = new List<Candlestick>();
         candles.AddRange(response.candles);

         return candles;
      }

      /// <summary>
      /// More detailed request to retrieve candles
      /// </summary>
      /// <param name="request">the request data to use when retrieving the candles</param>
      /// <returns>List of Candlestick received (or empty list)</returns>
      public static async Task<List<Candlestick>> GetCandlesAsync(string instrument, CandlesRequest request)
      {
         string requestString = Server(EServer.Account) + "instruments/" + instrument + "/candles" + request.GetRequestString();

         CandlesResponse response = await MakeRequestAsync<CandlesResponse>(requestString);

         var candles = new List<Candlestick>();
         candles.AddRange(response.candles);

         return candles;
      }
   }
}
