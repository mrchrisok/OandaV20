using OANDAV20.REST20.TradeLibrary.DataTypes.Account;
using OANDAV20.REST20.TradeLibrary.DataTypes.Communications;
using OANDAV20.REST20.TradeLibrary.DataTypes.Instrument;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OANDAV20.REST20
{
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
         return response.accountSummary;
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
   }
}
