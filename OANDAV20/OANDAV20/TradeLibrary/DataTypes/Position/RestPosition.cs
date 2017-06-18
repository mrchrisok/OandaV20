using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Position;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkonkwoOandaV20
{
   public partial class Rest20
   {
      /// <summary>
      /// Retrieves all positions for a given account
      /// </summary>
      /// <param name="accountId">positions will be retrieved for this account id</param>
      /// <returns>List of Position objects with the details for each position (or empty list iff no positions)</returns>
      public static async Task<List<Position>> GetPositionsAsync(string accountId)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/positions";

         var positionResponse = await MakeRequestAsync<PositionsResponse>(requestString);
         var positions = new List<Position>();
         positions.AddRange(positionResponse.positions);

         return positions;
      }

      /// <summary>
      /// Retrieves the current open positions for a given account
      /// </summary>
      /// <param name="accountId">positions will be retrieved for this account id</param>
      /// <returns>List of Position objects with the details for each position (or empty list iff no positions)</returns>
      public static async Task<List<Position>> GetOpenPositionsAsync(string accountId)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/openPositions";

         var response = await MakeRequestAsync<PositionsResponse>(requestString);
         var positions = new List<Position>();
         positions.AddRange(response.positions);

         return positions;
      }

      /// <summary>
      /// Retrieves the current position for the given instrument and account
      ///   This will cause an error if there is no position for that instrument 
      /// </summary>
      /// <param name="accountId">the account for which to get the position</param>
      /// <param name="instrument">the instrument for which to get the position</param>
      /// <returns>Position object with the details of the position</returns>
      public static async Task<Position> GetPositionAsync(string accountId, string instrument)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/positions/" + instrument;

         var response = await MakeRequestAsync<PositionResponse>(requestString);

         return response.position;
      }

      /// <summary>
      /// Close the given position
      /// This will close all trades on the provided account/instrument
      /// </summary>
      /// <param name="accountId">the account to close trades on</param>
      /// <param name="instrument">the instrument for which to close all trades</param>
      /// <returns>DeletePositionResponse object containing details about the actions taken</returns>
      public static async Task<PositionCloseResponse> ClosePositionAsync(string accountId, string instrument, ClosePositionRequest request)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/positions/" + instrument + "/close";

         var requestBody = ConvertToJSON(request);

         var response = await MakeRequestWithJSONBody<PositionCloseResponse>("PUT", requestBody, requestString);

         return response;
      }
   }
}
