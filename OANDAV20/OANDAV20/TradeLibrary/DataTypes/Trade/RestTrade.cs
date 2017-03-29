using OANDAV20.REST20.TradeLibrary.DataTypes.Communications;
using OANDAV20.REST20.TradeLibrary.DataTypes.Communications.Requests;
using OANDAV20.REST20.TradeLibrary.DataTypes.Trade;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OANDAV20.REST20
{
   public partial class Rest20
   {
      /// <summary>
      /// Retrieves the list of open trades belonging to the account
      /// </summary>
      /// <param name="account">the account to retrieve the list for</param>
      /// <param name="requestParams">optional additional parameters for the request (name, value pairs)</param>
      /// <returns>List of TradeData objects (or empty list, if no trades)</returns>
      public static async Task<List<Trade>> GetTradeListAsync(string account, Dictionary<string, string> requestParams = null)
      {
         string requestString = Server(EServer.Account) + "accounts/" + account + "/trades";
         TradesResponse tradeResponse = await MakeRequestAsync<TradesResponse>(requestString, "GET", requestParams);

         var trades = new List<Trade>();
         trades.AddRange(tradeResponse.trades);

         return trades;
      }

      /// <summary>
      /// Retrieves the list of all open trades belonging to the account
      /// </summary>
      /// <param name="account">the account to retrieve the list for</param>
      /// <returns>List of TradeData objects (or empty list, if no trades)</returns>
      public static async Task<List<Trade>> GetOpenTradeListAsync(string account)
      {
         string requestString = Server(EServer.Account) + "accounts/" + account + "/openTrades";
         TradesResponse tradeResponse = await MakeRequestAsync<TradesResponse>(requestString);

         var trades = new List<Trade>();
         trades.AddRange(tradeResponse.trades);

         return trades;
      }

      /// <summary>
      /// Retrieves the details for a given trade
      /// </summary>
      /// <param name="accountId">the account to which the trade belongs</param>
      /// <param name="tradeId">the ID of the trade to get the details</param>
      /// <returns>TradeData object containing the details of the trade</returns>
      public static async Task<Trade> GetTradeDetailsAsync(string accountId, long tradeId)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/trades/" + tradeId;
         TradeResponse tradeResponse = await MakeRequestAsync<TradeResponse>(requestString);

         var trade = tradeResponse.trade;

         return trade;
      }

      /// <summary>
      /// Close the trade specified
      /// </summary>
      /// <param name="accountId">the account that owns the trade</param>
      /// <param name="tradeId">the ID of the trade to close</param>
      /// <returns>DeleteTradeResponse containing the details of the close</returns>
      public static async Task<TradeCloseResponse> CloseTradeAsync(string accountId, long tradeId, string units = "ALL")
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/trades/" + tradeId + "/close";

         Dictionary<string, string> requestParams = new Dictionary<string, string>();
         requestParams.Add("units", units);

         return await MakeRequestWithJSONBody<TradeCloseResponse, Dictionary<string, string>>("PUT", requestParams, requestString);
      }

      /// <summary>
      /// Modify the specified trade, updating it with the parameters provided
      /// </summary>
      /// <param name="accountId">the account that owns the trade</param>
      /// <param name="tradeId">the id of the trade to update</param>
      /// <param name="requestParams">the parameters to update (name, value pairs)</param>
      /// <returns>TradeData for the trade post update</returns>
      public static async Task<TradeReplaceExitOrdersResponse> ReplaceExitOrders(string accountId, long tradeId, ReplaceExitOrdersRequest request)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/trades/" + tradeId + "/orders";
         return await MakeRequestWithJSONBody<TradeReplaceExitOrdersResponse, ReplaceExitOrdersRequest>("PUT", request, requestString);
      }
   }
}
