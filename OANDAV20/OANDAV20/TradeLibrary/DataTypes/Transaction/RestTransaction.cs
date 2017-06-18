using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkonkwoOandaV20
{
   public partial class Rest20
   {
      /// <summary>
      /// retrieves a list of transactions in descending order
      /// </summary>
      /// <param name="account"></param>
      /// <param name="parameters"></param>
      /// <returns></returns>
      public static async Task<List<ITransaction>> GetTransactionsByDateRangeAsync(string accountId, Dictionary<string, string> parameters = null)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/transactions";

         var pagesResponse = await MakeRequestAsync<TransactionPagesResponse>(requestString, "GET", parameters);

         var transactions = new List<ITransaction>();
         foreach (string page in pagesResponse.pages)
         {
            var pageParams = new Dictionary<string, string>();
            pageParams.Add("page", page);

            if (parameters.ContainsKey("type"))
               pageParams.Add("type", parameters["type"]);

            transactions.AddRange(await GetTransactionsByIdRangeAsync(accountId, pageParams));

            await Task.Delay(500); // throttle these a bit
         }

         return transactions;
      }

      /// <summary>
      /// Retrieves all transactions since the given transactionId (inclusive)
      /// </summary>
      /// <param name="accountId">the id of the account to which the transaction belongs</param>
      /// <param name="parameters">parameters describing the range of transactions to retrieve</param>
      /// <returns>List of transaction objects</returns>
      public static async Task<List<ITransaction>> GetTransactionsByIdRangeAsync(string accountId, Dictionary<string, string> parameters = null)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/transactions/idrange";

         TransactionsResponse response;

         if (parameters.ContainsKey("page"))
         {
            requestString = parameters["page"];

            if (parameters.ContainsKey("type"))
               requestString += "&" + parameters["type"];

            response = await MakeRequestAsync<TransactionsResponse>(requestString);
         }
         else
         {
            response = await MakeRequestAsync<TransactionsResponse>(requestString, "GET", parameters);
         }

         return response.transactions;
      }

      /// <summary>
      /// Retrieves all transactions since the given transactionId (inclusive)
      /// </summary>
      /// <param name="accountId">the id of the account to which the transaction belongs</param>
      /// <param name="transactionId">the id of the first transaction to retrieve</param>
      /// <returns>List of transaction objects</returns>
      public static async Task<List<ITransaction>> GetTransactionsSinceIdAsync(string accountId, long transactionId)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/transactions/sinceid";
         requestString += "?id=" + transactionId;

         TransactionsResponse response = await MakeRequestAsync<TransactionsResponse>(requestString);

         return response.transactions;
      }

      /// <summary>
      /// Retrieves the details for a given transaction
      /// </summary>
      /// <param name="accountId">the id of the account to which the transaction belongs</param>
      /// <param name="transactionId">the id of the transaction to retrieve</param>
      /// <returns>Transaction object with the details of the transaction</returns>
      public static async Task<ITransaction> GetTransactionDetailsAsync(string accountId, long transactionId)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/transactions/" + transactionId;

         var response = await MakeRequestAsync<TransactionResponse>(requestString);

         return response.transaction;
      }
   }
}
