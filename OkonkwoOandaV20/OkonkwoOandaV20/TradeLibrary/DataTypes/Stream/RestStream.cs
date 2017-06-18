using OkonkwoOandaV20.TradeLibrary.DataTypes.Instrument;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace OkonkwoOandaV20
{
   public partial class Rest20
   {
      /// <summary>
      /// Initializes a streaming rates session with the given instruments on the given account
      /// </summary>
      /// <param name="instruments">list of instruments to stream rates for</param>
      /// <param name="accountId">the account ID you want to stream on</param>
      /// <returns>the WebResponse object that can be used to retrieve the rates as they stream</returns>
      public static async Task<WebResponse> StartPricingSession(string accountId, List<Instrument> instruments, bool snapshot = true)
      {
         string instrumentList = "";
         foreach (var instrument in instruments)
         {
            instrumentList += instrument.name + ",";
         }
         // Remove the extra ,
         instrumentList = instrumentList.TrimEnd(',');
         instrumentList = Uri.EscapeDataString(instrumentList);

         string requestString = Server(EServer.StreamingPrices) + "accounts/" + accountId + "/pricing/stream";
         requestString += "?instruments=" + instrumentList + "&snapshot=" + snapshot.ToString();

         HttpWebRequest request = WebRequest.CreateHttp(requestString);
         request.Method = "GET";
         request.Headers[HttpRequestHeader.Authorization] = "Bearer " + AccessToken;

         try
         {
            WebResponse response = await request.GetResponseAsync();
            return response;
         }
         catch (WebException ex)
         {
            var response = (HttpWebResponse)ex.Response;
            var stream = new StreamReader(response.GetResponseStream());
            var result = stream.ReadToEnd();
            throw new Exception(result);
         }
      }

      /// <summary>
      /// Initializes a streaming events session which will stream events for the given accounts
      /// </summary>
      /// <param name="accountId">the account IDs you want to stream on</param>
      /// <returns>the WebResponse object that can be used to retrieve the events as they stream</returns>
      public static async Task<WebResponse> StartTransactionsSession(string accountId)
      {
         string requestString = Server(EServer.StreamingTransactions) + "accounts/" + accountId + "/transactions/stream";

         HttpWebRequest request = WebRequest.CreateHttp(requestString);
         request.Method = "GET";
         request.Headers[HttpRequestHeader.Authorization] = "Bearer " + AccessToken;

         try
         {
            WebResponse response = await request.GetResponseAsync();
            return response;
         }
         catch (WebException ex)
         {
            var response = (HttpWebResponse)ex.Response;
            var stream = new StreamReader(response.GetResponseStream());
            var result = stream.ReadToEnd();
            throw new Exception(result);
         }
      }
   }
}
