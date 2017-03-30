using Microsoft.VisualStudio.TestTools.UnitTesting;
using OANDAV20;
using OANDAV20.TradeLibrary.DataTypes.Communications;
using OANDAV20.TradeLibrary.DataTypes.Communications.Account;
using OANDAV20.TradeLibrary.DataTypes.Communications.Instrument;
using OANDAV20.TradeLibrary.DataTypes.Communications.Pricing;
using OANDAV20.TradeLibrary.DataTypes.Communications.Transaction;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OANDAv20Tests
{
   public partial class Restv20Test
   {
      #region Declarations
      static bool _apiOperationsComplete = false;
      static EEnvironment _testEnvironment;
      static string _testToken;
      static string _testAccount;
      static string _currency = "USD";

      static List<Instrument> _instruments;
      protected List<Price> _prices;
      protected Semaphore _tickReceived;
      protected Semaphore _eventReceived;
      private bool _marketHalted;
      private const string TEST_INSTRUMENT = "EUR_USD";
      #endregion

      #region Properties
      static string _accountId { get { return Credentials.GetDefaultCredentials().DefaultAccountId; } }
      #endregion

      #region Constructors
      public Restv20Test()
      {
         // Practice or Trade
         _testEnvironment = EEnvironment.Trade;

         // token should correspond to environment
         // For token help, see "Getting Started" at http://developer.oanda.com/rest-live-v20/introduction/
         _testToken = "5a0478f89da0cac4ee02ed60ff9329a6-0450b6274d7bbbc7fac532029be78d66";
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
               await Account_AccountsListTest(5);
               await Account_GetAccountSummaryTest();
               await Account_GetAccountsInstrumentsTest();
               await Account_GetSingleAccountInstrumentTest();
               await Account_PatchAccountConfigurationTest();

               //await Pricing_GetPricingInformationTest();



               // start transactions stream

               // after creating a market order and limit order
               // returns orders, trades, positions
               //await Account_GetAccountDetailsTest();

               // stop transactions stream 
            }

            _apiOperationsComplete = true;
         }
         catch (Exception ex)
         {
            _results.Add(ex.Message);
         }
      }

      #region Api operations
      #region Api operations - Account
      /// <summary>
      /// Retrieve the list of accounts associated with the account token
      /// </summary>
      /// <param name="listCount"></param>
      private static async Task Account_AccountsListTest(short? listCount = null)
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
            _results.Verify("01." + count.ToString(), account.id.Split('-').Length == 4, string.Format("Account id {0} has correct format.", account.id));
         }

         _testAccount = result[0].id;
         Credentials.SetCredentials(_testEnvironment, _testToken, _testAccount);
      }

      /// <summary>
      /// Retrieve the list of instruments associated with the given accountId
      /// </summary>
      //private static async Task Account_GetAccountDetailsTest()
      //{
      //   // Get an instrument list (basic)
      //   Account result = await Rest20.GetAccountDetailsAsync(_accountId);
      //   _results.Verify("03.0", result.Count == 1, string.Format("{0} info received.", instrument));
      //   _results.Verify("03.1", result[0].type == type, string.Format("{0} type ({1}) is correct.", instrument, result[0].type));

      //   _operationsRemaining--;
      //}

      /// <summary>
      /// Retrieve the list of instruments associated with the given accountId
      /// </summary>
      private static async Task Account_GetAccountsInstrumentsTest()
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
      private static async Task Account_GetSingleAccountInstrumentTest()
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
      private static async Task Account_GetAccountSummaryTest()
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
      private static async Task Account_PatchAccountConfigurationTest()
      {
         // 05
         AccountSummary summary = await Rest20.GetAccountSummaryAsync(_accountId);

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

      #region Pricing
      //public void test_Pricing_get_pricing_info()
      //{
      //   string key = "04.0";
      //   var results = _results.Items.Where(x => x.Key == key);
      //   var failure = results.FirstOrDefault(x => x.Value.Success == false);

      //   Assert.IsTrue(failure.Key == null, failure.Key + ": " + failure.Value);
      //}
      #endregion
      #endregion
   }
}
