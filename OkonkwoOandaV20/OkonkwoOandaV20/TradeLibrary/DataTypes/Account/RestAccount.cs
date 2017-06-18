using OkonkwoOandaV20.TradeLibrary.DataTypes.Account;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Instrument;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkonkwoOandaV20
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/account-ep/
   /// </summary>
   public partial class Rest20
   {
      /// <summary>
      /// Retrieves all the accounts belonging to the user.
      ///  The user is identifed by the supplied bearer token. 
      /// </summary>
      /// <returns>list of accounts, including basic information about the accounts</returns>
      public static async Task<List<AccountProperties>> GetAccountListAsync()
      {
         string requestString = Server(EServer.Account) + "accounts";

         var response = await MakeRequestAsync<AccountsResponse>(requestString);
         return response.accounts;
      }

      /// <summary>
      /// Retrieves the details for a given account
      /// </summary>
      /// <param name="accountId">details will be retrieved for this account id</param>
      /// <returns>Account object containing the account details</returns>
      public static async Task<Account> GetAccountDetailsAsync(string accountId)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId;

         var response = await MakeRequestAsync<AccountResponse>(requestString);
         return response.account;
      }

      /// <summary>
      /// Retrieves the summary for a given account
      /// </summary>
      /// <param name="accountId">summary will be retrieved for this account id</param>
      /// <returns>AccountSummary object containing the account details</returns>
      public static async Task<AccountSummary> GetAccountSummaryAsync(string accountId)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/summary";

         var response = await MakeRequestAsync<AccountSummaryResponse>(requestString);
         return response.account;
      }

      /// <summary>
      /// Retrieves the tradeable instruments for a given account
      /// </summary>
      /// <param name="accountId">summary will be retrieved for this account id</param>
      /// <param name="instruments">a comma-separated list of instrument names to return</param>
      /// <returns>list of tradeable instruments the account details</returns>
      public static async Task<List<Instrument>> GetAccountInstrumentsAsync(string accountId, List<string> instruments = null)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/instruments";

         if (instruments != null)
         {
            string instrumentsParam = GetCommaSeparatedList(instruments);
            requestString += "?instruments=" + Uri.EscapeDataString(instrumentsParam);
         }

         var response = await MakeRequestAsync<AccountInstrumentsResponse>(requestString);
         return response.instruments;
      }

      /// <summary>
      /// Updates the client-configurable values for the given account
      /// </summary>
      /// <param name="accountId">AccountId to apply the updates to.</param>
      /// <param name="bodyParams">Name-value dictionary of updates to apply</param>
      /// <returns>The updated values that were applied to the account</returns>
      public static async Task<AccountConfigurationResponse> PatchAccountConfigurationAsync(string accountId, Dictionary<string, string> bodyParams)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/configuration";

         // api only accepts 'alias' and 'marginRate'
         foreach(var key in bodyParams.Keys)
         {
            if (key == "alias" || key == "marginRate")
               continue;
            bodyParams.Remove(key);
         }

         var response = await MakeRequestWithJSONBody<AccountConfigurationResponse, Dictionary<string, string>>("PATCH", bodyParams, requestString);

         return response;
      }

      /// <summary>
      /// Retrieves the current state of an account and changes since the give transaction ID
      /// </summary>
      /// <param name="accountId">summary will be retrieved for this account id</param>
      /// <param name="transactionId">the id of the first transaction to return changes from</param>
      /// <returns>list of tradeable instruments the account details</returns>
      public static async Task<AccountChangesResponse> GetAccountChangesAsync(string accountId, long transactionId)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/changes";

         requestString += "?sinceTransactionID=" + transactionId;

         var response = await MakeRequestAsync<AccountChangesResponse>(requestString);

         return response;
      }
   }
}
