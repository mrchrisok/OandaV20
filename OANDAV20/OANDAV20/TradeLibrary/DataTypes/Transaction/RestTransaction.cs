using OANDAV20.TradeLibrary.DataTypes.Communications;
using OANDAV20.TradeLibrary.DataTypes.Transaction;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace OANDAV20
{
   public partial class Rest20
   {
      /// <summary>
      /// retrieves a list of transactions in descending order
      /// </summary>
      /// <param name="account"></param>
      /// <param name="parameters"></param>
      /// <returns></returns>
      public static async Task<List<Transaction>> GetTransactionListByDateRangeAsync(string accountId, Dictionary<string, string> parameters = null)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/transactions";

         var pagesResponse = await MakeRequestAsync<TransactionPagesResponse>(requestString, "GET", parameters);

         var transactions = new List<Transaction>();
         foreach (string page in pagesResponse.pages)
         {
            var pageParams = new Dictionary<string, string>();
            pageParams.Add("page", page);

            if (parameters.ContainsKey("type"))
               pageParams.Add("type", parameters["type"]);

            transactions.AddRange(await GetTransactionListByIdRangeAsync(accountId, pageParams));
         }

         return transactions;
      }

      /// <summary>
      /// retrieves a list of transactions in descending order
      /// </summary>
      /// <param name="account"></param>
      /// <param name="parameters"></param>
      /// <returns></returns>
      public static async Task<List<Transaction>> GetTransactionListByIdRangeAsync(string accountId, Dictionary<string, string> parameters = null)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/transactions/idrange";

         TransactionsResponse response;
         var transactions = new List<Transaction>();

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

         transactions.AddRange(response.transactions);

         return transactions;
      }

      /// <summary>
      /// Retrieves the details for a given transaction
      /// </summary>
      /// <param name="accountId">the id of the account to which the transaction belongs</param>
      /// <param name="transId">the id of the transaction to retrieve</param>
      /// <returns>Transaction object with the details of the transaction</returns>
      public static async Task<Transaction> GetTransactionDetailsAsync(string accountId, long transId)
      {
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/transactions/" + transId;

         var transaction = await MakeRequestAsync<Transaction>(requestString);

         return transaction;
      }

      /// <summary>
      /// Expensive request to retrieve the entire transaction history for a given account
      /// This request may take some time
      /// This request is heavily rate limited
      /// This request does not work on sandbox
      /// </summary>
      /// <param name="accountId">the id of the account for which to retrieve the history</param>
      /// <returns>List of Transaction objects with the details of all transactions</returns>
      public static async Task<List<Transaction>> GetFullTransactionHistoryAsync(string accountId)
      {  // NOTE: this does not work on sandbox
         string requestString = Server(EServer.Account) + "accounts/" + accountId + "/alltransactions";

         HttpWebRequest request = WebRequest.CreateHttp(requestString);
         request.Headers[HttpRequestHeader.Authorization] = "Bearer " + AccessToken;
         request.Method = "GET";
         string location;
         // Phase 1: request and get the location
         try
         {
            using (WebResponse response = await request.GetResponseAsync())
            {
               location = response.Headers["Location"];
            }
         }
         catch (WebException ex)
         {
            var response = (HttpWebResponse)ex.Response;
            var stream = new StreamReader(response.GetResponseStream());
            var result = stream.ReadToEnd();
            throw new Exception(result);
         }

         // Phase 2: wait for and retrieve the actual data
         HttpClient Account = new HttpClient();

         //request = WebRequest.CreateHttp(location);
         for (int retries = 0; retries < 20; retries++)
         {
            try
            {
               var response = await Account.GetAsync(location);
               if (response.IsSuccessStatusCode)
               {
                  var serializer = new DataContractJsonSerializer(typeof(List<Transaction>));
                  var archive = new ZipArchive(await response.Content.ReadAsStreamAsync());
                  return (List<Transaction>)serializer.ReadObject(archive.Entries[0].Open());
               }
               else if (response.StatusCode == HttpStatusCode.NotFound)
               {  // Not found is expected until the resource is ready
                  // Delay a bit to wait for the response
                  await Task.Delay(500);
               }
               else
               {
                  var stream = new StreamReader(await response.Content.ReadAsStreamAsync());
                  var result = stream.ReadToEnd();
                  throw new Exception(result);
               }
            }
            catch (WebException ex)
            {
               var response = (HttpWebResponse)ex.Response;
               var stream = new StreamReader(response.GetResponseStream());
               var result = stream.ReadToEnd();
               throw new Exception(result);
            }
         }
         return null;
      }
   }
}
