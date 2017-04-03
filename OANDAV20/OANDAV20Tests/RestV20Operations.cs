using Microsoft.VisualStudio.TestTools.UnitTesting;
using OANDAV20;
using OANDAV20.Framework;
using OANDAV20.TradeLibrary.DataTypes.Account;
using OANDAV20.TradeLibrary.DataTypes.Communications;
using OANDAV20.TradeLibrary.DataTypes.Instrument;
using OANDAV20.TradeLibrary.DataTypes.Pricing;
using OANDAV20.TradeLibrary.DataTypes.Stream;
using OANDAV20.TradeLibrary.DataTypes.Transaction;
using OANDAV20.TradeLibrary.Primitives;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Linq;

namespace OANDAv20Tests
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/introduction/
   /// </summary>
   public partial class Restv20Test
   {
      #region Declarations
      static bool _apiOperationsComplete = false;
      static EEnvironment _testEnvironment;
      static string _testToken;
      static string _testAccount;
      static short _tokenAccounts;
      static string _currency = "USD";
      static List<Transaction> _transactions;
      static List<Instrument> _instruments;
      static long _lastTransactionID;

      protected List<Price> _prices;
      protected Semaphore _tickReceived;
      static Semaphore _transactionReceived;
      #endregion

      static string _accountId { get { return Credentials.GetDefaultCredentials().DefaultAccountId; } }

      #region Constructors
      public Restv20Test()
      {
         _testEnvironment = EEnvironment.Trade;
         _testToken = "5a0478f89da0cac4ee02ed60ff9329a6-0450b6274d7bbbc7fac532029be78d66";
         _tokenAccounts = 5;
      }
      #endregion

      [ClassInitialize]
      public static async void RunApiOperations(TestContext context)
      {
         try
         {
            Credentials.SetCredentials(_testEnvironment, _testToken, null);

            if (Credentials.GetDefaultCredentials() == null)
            {
               throw new Exception("Exception: RestV20Test - Credentials must be defined to run this test.");
            }

            if (Credentials.GetDefaultCredentials().HasServer(EServer.Account))
            {
               await Account_GetAccountsList(_tokenAccounts);
               await Account_GetAccountDetails();
               await Account_GetAccountSummary();
               await Account_GetAccountsInstruments();
               await Account_GetSingleAccountInstrument();
               await Account_PatchAccountConfiguration();

               await Transaction_GetTransactionsSinceId();

               await Pricing_GetPricing();

               //if (!await IsMarketHalted())
               //{
               //   // start & stop the pricing stream
               //   if (Credentials.GetDefaultCredentials().HasServer(EServer.StreamingPrices))
               //   {
               //      await Stream_GetStreamingPrices();
               //   }

               //   // start transactions stream
               //   Task transactionsStreamCheck = null;
               //   if (Credentials.GetDefaultCredentials().HasServer(EServer.StreamingTransactions))
               //   {
               //      transactionsStreamCheck = Stream_GetStreamingTransactions();
               //   }

               //   // create stream traffic
               //   await RunOrderOperations();
               //   await RunTradeOperations();
               //   await RunPositionOperations();


               //   // review the stream traffic
               //   await Transaction_GetTransactionsByDateRange();
               //   await Account_GetAccountChanges();

               //   // stop transactions stream 
               //}
            }
         }
         catch (MarketHaltedException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            throw new Exception("An unhandled error occured during execution of REST V20 operations.", ex);
         }
         finally
         {
            _apiOperationsComplete = true;
         }
      }

      #region Account
      /// <summary>
      /// Retrieve the list of accounts associated with the account token
      /// </summary>
      /// <param name="listCount"></param>
      private static async Task Account_GetAccountsList(short? listCount = null)
      {
         short count = 0;

         List<AccountProperties> result = await Rest20.GetAccountListAsync();
         _results.Verify("01.0", result.Count > 0, "Account list received.");

         string message = "Correct number of accounts received.";
         bool correctCount = true;
         if (listCount.HasValue && listCount.Value > 0)
         {
            count++;
            correctCount = result.Count == listCount;
            message = string.Format("Correct number of accounts ({0}) received.", result.Count);
         }
         _results.Verify("01." + count.ToString(), correctCount, message);

         foreach (var account in result)
         {
            count++;
            string description = string.Format("Account id {0} has correct format.", account.id);
            _results.Verify("01." + count.ToString(), account.id.Split('-').Length == 4, description);
         }

         _testAccount = result[0].id;
         Credentials.SetCredentials(_testEnvironment, _testToken, _testAccount);
      }

      // <summary>
      // Retrieve the full details for the test accountId
      // </summary>
      private static async Task Account_GetAccountDetails()
      {
         Account result = await Rest20.GetAccountDetailsAsync(_accountId);

         _results.Verify("08.0", result != null, string.Format("Account {0} info received.", _accountId));
         _results.Verify("08.1", result.id == _accountId, string.Format("Account id ({0}) is correct.", result.id));
         _results.Verify("08.2", result.currency == _currency, string.Format("Account currency ({0}) is correct.", result.currency));
         _results.Verify("08.3", result.openTradeCount == 0, string.Format("Account openTradeCount ({0}) is correct.", 0));
         _results.Verify("08.4", result.trades.Count == result.openTradeCount, string.Format("Account trades count ({0}) is correct.", 0));
      }

      /// <summary>
      /// Retrieve the list of instruments associated with the given accountId
      /// </summary>
      private static async Task Account_GetAccountsInstruments()
      {
         // Get an instrument list (basic)
         List<Instrument> result = await Rest20.GetAccountInstrumentsAsync(_accountId);
         _results.Verify("02.0", result.Count > 0, "Instrument list received");

         // Store the instruments for other tests
         _instruments = result;
      }

      /// <summary>
      /// Retrieve the list of instruments associated with the given accountId
      /// </summary>
      private static async Task Account_GetSingleAccountInstrument()
      {
         // Get an instrument list (basic)
         string instrument = "EUR_USD";
         string type = InstrumentType.Currency;
         List<string> eurusd = new List<string>() { instrument };
         List<Instrument> result = await Rest20.GetAccountInstrumentsAsync(_accountId, eurusd);
         _results.Verify("03.0", result.Count == 1, string.Format("{0} info received.", instrument));
         _results.Verify("03.1", result[0].type == type, string.Format("{0} type ({1}) is correct.", instrument, result[0].type));
         _results.Verify("03.2", result[0].name == instrument, string.Format("{0} name ({1}) is correct.", instrument, result[0].name));
      }

      /// <summary>
      /// Retrieve summary information for the given accountId
      /// </summary>
      private static async Task Account_GetAccountSummary()
      {
         // 04
         AccountSummary result = await Rest20.GetAccountSummaryAsync(_accountId);
         _results.Verify("04.0", result != null, string.Format("Account {0} info received.", _accountId));
         _results.Verify("04.1", result.id == _accountId, string.Format("AccounSummary.id ({0}) is correct.", result.id));
         _results.Verify("04.2", result.currency == _currency, string.Format("AccountSummary.currency ({0}) is correct.", result.currency));
      }

      /// <summary>
      /// Retrieve summary information for the given accountId
      /// </summary>
      private static async Task Account_PatchAccountConfiguration()
      {
         // 05
         AccountSummary summary = await Rest20.GetAccountSummaryAsync(_accountId);

         // use this to test Transactionn_GetTransactionsSinceId()
         _lastTransactionID = summary.lastTransactionID;

         string alias = summary.alias;
         double? marginRate = summary.marginRate;

         string testAlias = "testAlias";
         double? testMarginRate = marginRate == null ? 0.5 : ((marginRate == 0.5) ? 0.4 : 0.5);
         Dictionary<string, string> accountConfig = new Dictionary<string, string>();
         accountConfig.Add("alias", testAlias);
         accountConfig.Add("marginRate", testMarginRate.ToString());

         AccountConfigurationResponse response = await Rest20.PatchAccountConfigurationAsync(_accountId, accountConfig);
         ClientConfigureTransaction newConfig = response.clientConfigureTransaction;

         _results.Verify("05.0", newConfig != null, string.Format("Account configuration retrieved successfully.", newConfig.alias));
         _results.Verify("05.1", newConfig.alias != alias, string.Format("Account alias {0} updated successfully.", newConfig.alias));
         _results.Verify("05.2", newConfig.marginRate != marginRate, string.Format("Account marginRate {0} updated succesfully.", newConfig.marginRate));

         accountConfig["alias"] = alias;
         accountConfig["marginRate"] = (marginRate ?? 1.0).ToString();
         AccountConfigurationResponse response2 = await Rest20.PatchAccountConfigurationAsync(_accountId, accountConfig);
         ClientConfigureTransaction newConfig2 = response2.clientConfigureTransaction;

         _results.Verify("05.3", newConfig2.alias == alias, string.Format("Account alias {0} reverted successfully.", newConfig2.alias));
         _results.Verify("05.4", newConfig2.marginRate == marginRate, string.Format("Account marginRate {0} reverted succesfully.", newConfig2.marginRate));
      }
      #endregion

      #region Transaction
      private static async Task Transaction_GetTransactionsByDateRange()
      {
         // 09
         Dictionary<string, string> parameters = new Dictionary<string, string>();
         parameters.Add("from", ConvertDateTimeToAcceptDateFormat(DateTime.UtcNow));

         List<ITransaction> result = await Rest20.GetTransactionsByDateRangeAsync(_accountId, parameters);

         //_results.Verify("09.0", result != null, string.Format("Account {0} info received.", _accountId));
         //_results.Verify("09.1", result.id == _accountId, string.Format("AccounSummary.id ({0}) is correct.", result.id));
         //_results.Verify("09.2", result.currency == _currency, string.Format("AccountSummary.currency ({0}) is correct.", result.currency));
      }

      private static async Task Transaction_GetTransactionsSinceId()
      {
         // 10
         List<ITransaction> results = await Rest20.GetTransactionsSinceIdAsync(_accountId, _lastTransactionID);
         results.OrderBy(x => x.id);

         _results.Verify("10.0", results != null, string.Format("Transactions info received.", _accountId));
         _results.Verify("10.1", results.First().id == _lastTransactionID + 1, string.Format("Id of first transaction is correct.", results.First().id));
         _results.Verify("10.2", results.Where(x => x.id <= _lastTransactionID).Count() == 0, "All Id's are greater than last transactionId.");
         _results.Verify("10.3", results.Where(x => x.type == TransactionType.ClientConfigure).Count() > 0, "Client configure transactions returned.");
      }
      #endregion

      #region Pricing
      private static async Task Pricing_GetPricing()
      {
         List<string> instruments = new List<string>();
         _instruments.ForEach(x => instruments.Add(x.name));

         List<Price> prices = await Rest20.GetPriceListAsync(_accountId, instruments);

         _results.Verify("06.0", prices != null, string.Format("Prices retrieved successfully."));
         _results.Verify("06.1", prices.Count == _instruments.Count, string.Format("Correct count ({0}) of prices retrieved.", prices.Count));
      }
      #endregion

      #region Stream
      protected static Task Stream_GetStreamingTransactions()
      {
         // 07
         TransactionsSession session = new TransactionsSession(_accountId);
         _transactionReceived = new Semaphore(0, 100);
         session.DataReceived += OnTransactionReceived;
         session.StartSession();

         return Task.Run(() =>
         {
            // block and wait 10sec or until an event is received
            bool success = _transactionReceived.WaitOne(10000);
            session.StopSession();
            _results.Verify("07.0", success, "Transaction events stream functioning successfully.");
         });
      }

      protected static void OnTransactionReceived(TransactionStreamResponse data)
      {
         // only testing the first transaction received
         bool gotData = false;

         if (!gotData)
         {
            _results.Verify("07.1", data.transaction != null, "Event transaction received");
            if (data.transaction != null)
            {
               _results.Verify("07.2", data.transaction.id != 0, "Transaction data received.");
               _results.Verify("07.3", data.transaction.accountID == _accountId, string.Format("Transaction has correct accountID: {()}.", _accountId));
            }

            gotData = true;
         }

         _transactionReceived.Release();
      }
      #endregion

      #region Utilities
      private static async Task<bool> IsMarketHalted()
      {
         var eurusd = new List<string>() { "EUR_USD" };
         var price = await Rest20.GetPriceListAsync(_accountId, eurusd);

         bool isTradeable = false, hasBids = false, hasAsks = false;

         if (price[0] != null)
         {
            isTradeable = price[0].tradeable;
            hasBids = price[0].bids.Count > 0;
            hasAsks = price[0].asks.Count > 0;
         }

         bool marketHalted = !(isTradeable && hasBids && hasAsks);

         if (marketHalted)
            throw new MarketHaltedException("OANDA Fx market is halted!");

         return false;
      }

      private static string ConvertDateTimeToAcceptDateFormat(DateTime time, AcceptDatetimeFormat format = AcceptDatetimeFormat.RFC3339)
      {
         if (format == AcceptDatetimeFormat.RFC3339)
            return XmlConvert.ToString(time, "yyyy-MM-ddTHH:mm:ssZ");
         else if (format == AcceptDatetimeFormat.Unix)
            return ((int)(time.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
         else
            throw new ArgumentException(string.Format("The value ({0}) of the format parameter is invalid.", (short)format));
      }
      #endregion
   }
}
