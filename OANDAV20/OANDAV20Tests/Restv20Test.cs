using OANDAV20.REST20;
using OANDAV20.REST20.TradeLibrary.DataTypes.Account;
using OANDAV20.REST20.TradeLibrary.DataTypes.Instrument;
using OANDAV20.REST20.TradeLibrary.DataTypes.Pricing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MarketMiner.Api.Tests.OANDAv20
{
   [TestClass]
   public class Restv20Test : Restv20TestBase
   {
      #region Declarations
      static short _operationsRemaining = 3;
      static EEnvironment _testEnvironment;
      static string _testToken;
      static string _testAccount;

      static List<Instrument> _instruments;
      protected List<Price> _prices;
      protected Semaphore _tickReceived;
      protected Semaphore _eventReceived;
      private bool _marketHalted;
      private const string TestInstrument = "EUR_USD";
      #endregion

      #region Properties
      static string _accountId { get { return Credentials.GetDefaultCredentials().DefaultAccountId; } }
      #endregion

      #region Constructors
      public Restv20Test()
      {
         _testEnvironment = EEnvironment.Trade;
         _testToken = "5a0478f89da0cac4ee02ed60ff9329a6-0450b6274d7bbbc7fac532029be78d66";
         _testAccount = "001-001-432582-001";
      }
      #endregion

      [ClassInitialize]
      public static async void RunV20Tests(TestContext context)
      {
         try
         {
            Credentials.SetCredentials(_testEnvironment, _testToken, _testAccount);

            if (Credentials.GetDefaultCredentials() == null)
            {
               throw new Exception("Exception: RestV20Test - Credentials must be defined to run this test.");
            }

            if (Credentials.GetDefaultCredentials().HasServer(EServer.Account))
            {
               AccountsListTest(5);
               GetAccountsInstrumentsTest();
               GetSingleAccountInstrumentTest();

               //_marketHalted = await IsMarketHalted();
               //if (_marketHalted) _results.Add("***** Market is halted! *****\n");
               //// Rates
               //await RunRatesTest();
            }
         }
         catch (Exception ex)
         {
            _results.Add(ex.Message);
         }
      }

      [TestInitialize]
      public void CheckIfAllApiOperationsHaveCompleted()
      {
         while (_operationsRemaining != 0) Task.Delay(5000);
      }

      #region Test methods - Account
      [TestMethod]
      public void test_v20_Credentials_get_credentials()
      {
         Assert.IsTrue(Credentials.GetDefaultCredentials().Environment == _testEnvironment, "Credentials Environment is incorrect.");
         Assert.IsTrue(Credentials.GetDefaultCredentials().AccessToken == _testToken, "Credentials Token is incorrect.");
         Assert.IsTrue(Credentials.GetDefaultCredentials().DefaultAccountId == _testAccount, "Credentials AccountId is incorrect.");
      }

      [TestMethod]
      public void test_v20_Account_retrieve_accounts_list()
      {
         string key = "01.0";
         var result = _results.Items.FirstOrDefault(x => x.Key == key).Value as Restv20TestResult;

         Assert.IsTrue(result.Success, result.Success.ToString() + ": " + result.Details);
      }

      [TestMethod]
      public void test_v20_Account_retrieve_correct_number_of_accounts()
      {
         string key = "01.1";
         var result = _results.Items.FirstOrDefault(x => x.Key == key).Value as Restv20TestResult;

         Assert.IsTrue(result.Success, result.Success.ToString() + ": " + result.Details);
      }

      [TestMethod]
      public void test_v20_Account_account_numbers_have_correct_format()
      {
         string key = "01.";
         var results = _results.Items.Where(x => x.Key.StartsWith(key) && x.Key != "01.0" && x.Key != "01.1");
         var failure = results.FirstOrDefault(x => x.Value.Success == false);

         string message = failure.Key != null ? failure.Value.Success.ToString() + ": " + failure.Value.Details : "";

         Assert.IsTrue(failure.Key == null, failure.Key + ": " + message);
      }

      [TestMethod]
      public void test_v20_Account_retrieve_instruments_list()
      {
         string key = "02.0";
         var results = _results.Items.Where(x => x.Key == key);
         var failure = results.FirstOrDefault(x => x.Value.Success == false);

         Assert.IsTrue(failure.Key == null, failure.Key + ": " + failure.Value);
      }

      [TestMethod]
      public void test_v20_Account_retrieve_single_instrument_info()
      {
         string key = "03.0";
         var instrument = _results.Items.Where(x => x.Key == key);
         var failure = results.FirstOrDefault(x => x.Value.Success == false);

         Assert.IsTrue(failure.Key == null, failure.Key + ": " + failure.Value);
      }
      #endregion

      #region Test methods - Instrument
      //[TestMethod]
      //public void test_v20_Instrument_retrieve_instrument_details()
      //{
      //   string key = "01";
      //   var results = _results.Items.Where(x => x.Key.StartsWith(key) && x.Key != "01.0");
      //   var failure = results.FirstOrDefault(x => x.Value.Success = false);

      //   Assert.IsTrue(failure.Key == null, failure.Key + ": " + failure.Value);
      //}
      #endregion

      #region Api operations - Account
      /// <summary>
      /// Retrieve the list of accounts associated with the account token
      /// </summary>
      /// <param name="listCount"></param>
      private static async void AccountsListTest(short? listCount = null)
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

         _operationsRemaining--;
      }

      /// <summary>
      /// Retrieve the list of instruments associated with the given accountId
      /// </summary>
      private static async void GetAccountsInstrumentsTest()
      {
         // Get an instrument list (basic)
         List<Instrument> result = await Rest20.GetAccountInstrumentsAsync(_accountId);
         _results.Verify("02.0", result.Count > 0, "Instrument list received");

         // Store the instruments for other tests
         _instruments = result;

         _operationsRemaining--;
      }

      /// <summary>
      /// Retrieve the list of instruments associated with the given accountId
      /// </summary>
      private static async void GetSingleAccountInstrumentTest()
      {
         // Get an instrument list (basic)
         string instrument = "EUR_USD";
         string type = InstrumentType.Currency;
         List<string> eurusd = new List<string>() { instrument };
         List<Instrument> result = await Rest20.GetAccountInstrumentsAsync(_accountId, eurusd);
         _results.Verify("03.0", result.Count == 1, string.Format("{0} info received.", instrument));
         _results.Verify("03.1", result[0].type == type, string.Format("{0} type ({1}) is correct.", instrument, result[0].type));

         _operationsRemaining--;
      }
      #endregion

      //#region Public methods
      //[ClassInitialize]
      //public virtual async Task RunTests()
      //{
      //   try
      //   {
      //      Credentials.SetCredentials(_testEnvironment, _testToken, _testAccount);

      //      /////
      //      if (Credentials.GetDefaultCredentials() == null)
      //      {
      //         throw new Exception("Exception: RestTest - Credentials must be defined to run this test");
      //      }

      //      if (Credentials.GetDefaultCredentials().HasServer(EServer.Account))
      //      {
      //         await AccountsListTest();
      //         await AccountsInstrumentsTest();

      //         _marketHalted = await IsMarketHalted();
      //         if (_marketHalted) _results.Add("***** Market is halted! *****\n");
      //         // Rates
      //         await RunRatesTest();
      //      }

      //      Task eventsCheck = null;
      //      if (Credentials.GetDefaultCredentials().HasServer(EServer.StreamingTransactions))
      //      {
      //         // Streaming Notifications
      //         eventsCheck = RunStreamingNotificationsTest(); // start notifications session
      //      }

      //      if (Credentials.GetDefaultCredentials().HasServer(EServer.Account))
      //      {
      //         // Accounts
      //         await RunAccountsTest();

      //         // the following should generate notifications
      //         // Orders
      //         await RunOrdersTest();
      //         // Trades
      //         await RunTradesTest();
      //         // Positions
      //         await RunPositionsTest();
      //         // Transaction History
      //         await RunTransactionsTest();
      //      }

      //      if (Credentials.GetDefaultCredentials().HasServer(EServer.StreamingPrices))
      //      {
      //         // Streaming Rates
      //         RunStreamingRatesTest();
      //      }

      //      if (eventsCheck != null)
      //      {
      //         await eventsCheck; // stop notifications session
      //      }
      //   }
      //   catch (Exception ex)
      //   {
      //      _results.Add(ex.Message);
      //   }
      //}
      //#endregion

      //#region Account tests
      //private async Task AccountsListTest()
      //{
      //   short count = 0;
      //   // Get an instrument list (basic)
      //   List<AccountProperties> result = await Rest20.GetAccountListAsync();
      //   _results.Verify("01.0", result.Count > 0, "Account list received");

      //   foreach(var account in result)
      //   {
      //      count++;
      //      _results.Verify("01." + count.ToString(), account.id.Split('-').Length == 4, string.Format("Account id {0} has correct format.", account.id));
      //   }
      //}

      //private async Task AccountsInstrumentsTest()
      //{
      //   // Get an instrument list (basic)
      //   List<Instrument> result = await Rest20.GetAccountInstrumentsAsync(_accountId);
      //   _results.Verify("04.0", result.Count > 0, "Instrument list received");

      //   // Store the instruments for other tests
      //   _instruments = result;
      //}
      //#endregion

      //#region Streaming tests
      //protected virtual Task RunStreamingNotificationsTest()
      //{
      //   EventsSession session = new EventsSession(_accountId);
      //   _eventReceived = new Semaphore(0, 100);
      //   session.DataReceived += OnEventReceived;
      //   session.StartSession();
      //   _results.Add("Starting event stream test");

      //   // returns the Run() task of the anonymous task
      //   return Task.Run(() =>
      //   {
      //      // should have received order/trade/position/transaction notification by now
      //      bool success = _eventReceived.WaitOne(10000);
      //      session.StopSession();
      //      _results.Verify(success, "Streaming events successfully received");
      //   }
      //   );
      //}
      //protected virtual void OnEventReceived(Event data)
      //{
      //   // _results.Verify the event data
      //   _results.Verify(data.transaction != null, "Event transaction received");
      //   if (data.transaction != null)
      //   {
      //      _results.Verify(data.transaction.id != 0, "Event data received");
      //      _results.Verify(data.transaction.accountId != 0, "Account id received");
      //   }
      //   _eventReceived.Release();
      //}

      //protected virtual void RunStreamingRatesTest()
      //{
      //   RatesSession session = new RatesSession(_accountId, _instruments);
      //   _tickReceived = new Semaphore(0, 100);
      //   session.DataReceived += SessionOnDataReceived;
      //   session.StartSession();
      //   _results.Add("Starting rate stream test");
      //   bool success = _tickReceived.WaitOne(10000);
      //   session.StopSession();
      //   _results.Verify(success, "Streaming rates successfully received");
      //}
      //protected virtual void SessionOnDataReceived(RateStreamResponse data)
      //{
      //   // _results.Verify the tick data
      //   _results.Verify(data.tick != null, "Streaming Tick received");
      //   if (data.tick != null)
      //   {
      //      _results.Verify(data.tick.ask > 0 && data.tick.bid > 0, "Streaming tick has bid/ask");
      //      _results.Verify(!string.IsNullOrEmpty(data.tick.instrument), "Streaming tick has instrument");
      //   }
      //   _tickReceived.Release();
      //}
      //#endregion

      //#region Private methods
      //private async Task<bool> IsMarketHalted()
      //{
      //   //var eurusd = _prices.Where(x => x.instrument == TestInstrument).ToList();
      //   var eurusd = new List<string>() { "EUR_USD" };
      //   var prices = await Rest20.GetPriceListAsync(_accountId, eurusd);

      //   _results.Verify("02.0", prices.Count == 1, "Price data received.");
      //   _results.Verify("02.1", prices[0].bids.Count > 0, "Price bids received.");
      //   _results.Verify("02.2", prices[0].asks.Count > 0, "Price asks received.");

      //   return prices[0].tradeable == true && prices[0].bids.Count > 0 && prices[0].asks.Count > 0;
      //}

      ///// <summary>
      ///// Test transaction history retrieval. 
      ///// For more information, visit http://developer.oanda.com/rest-live/transaction-history/#getTransactionHistory
      ///// </summary>
      ///// <returns></returns>
      //private async Task RunTransactionsTest()
      //{
      //   var result = await Rest.GetTransactionListAsync(_accountId);
      //   _results.Verify(result.Count > 0, "Recent transactions retrieved");
      //   foreach (var transaction in result)
      //   {
      //      _results.Verify(transaction.id > 0, "Transaction has id");
      //      _results.Verify(!string.IsNullOrEmpty(transaction.type), "Transation has type");
      //   }
      //   var parameters = new Dictionary<string, string> { { "count", "500" } };
      //   result = await Rest.GetTransactionListAsync(_accountId, parameters);
      //   _results.Verify(result.Count == 500, "Recent transactions retrieved");
      //   foreach (var transaction in result)
      //   {
      //      _results.Verify(transaction.id > 0, "Transaction has id");
      //      _results.Verify(!string.IsNullOrEmpty(transaction.type), "Transation has type");
      //   }

      //   // Get details for a transaction
      //   var trans = await Rest.GetTransactionDetailsAsync(_accountId, result[0].id);
      //   _results.Verify(trans.id == result[0].id, "Transaction details retrieved");

      //   if (!Credentials.GetDefaultCredentials().IsSandbox)
      //   {	// Not available on sandbox
      //      // Get Full account history
      //      var fullHistory = await Rest.GetFullTransactionHistoryAsync(_accountId);
      //      _results.Verify(fullHistory.Count > 0, "Full transaction history retrieved");
      //   }
      //}

      //private async Task RunPositionsTest()
      //{
      //   if (!_marketHalted)
      //   {
      //      // Make sure there's a position to test
      //      await PlaceMarketOrder();

      //      // get list of open positions
      //      var positions = await Rest.GetPositionsAsync(_accountId);
      //      _results.Verify(positions.Count > 0, "Positions retrieved");
      //      foreach (var position in positions)
      //      {
      //         VerifyPosition(position);
      //      }

      //      // get position for a given instrument
      //      var onePosition = await Rest.GetPositionAsync(_accountId, TestInstrument);
      //      VerifyPosition(onePosition);

      //      // close a whole position
      //      var closePositionResponse = await Rest.DeletePositionAsync(_accountId, TestInstrument);
      //      _results.Verify(closePositionResponse.ids.Count > 0 && closePositionResponse.instrument == TestInstrument, "Position closed");
      //      _results.Verify(closePositionResponse.totalUnits > 0 && closePositionResponse.price > 0, "Position close response seems valid");
      //   }
      //   else
      //   {
      //      _results.Add("Skipping: Position test because market is halted");
      //   }
      //}

      //private void VerifyPosition(Position position)
      //{
      //   _results.Verify(position.units > 0, "Position has units");
      //   _results.Verify(position.avgPrice > 0, "Position has avgPrice");
      //   _results.Verify(!string.IsNullOrEmpty(position.side), "Position has direction");
      //   _results.Verify(!string.IsNullOrEmpty(position.instrument), "Position has instrument");
      //}

      //private async Task RunTradesTest()
      //{
      //   // trade tests
      //   await PlaceMarketOrder();

      //   // get list of open trades
      //   var openTrades = await Rest.GetTradeListAsync(_accountId);
      //   _results.Verify(openTrades.Count > 0 && openTrades[0].id > 0, "Trades list retrieved");
      //   if (openTrades.Count > 0)
      //   {
      //      // get details for a trade
      //      var tradeDetails = await Rest.GetTradeDetailsAsync(_accountId, openTrades[0].id);
      //      _results.Verify(tradeDetails.id > 0 && tradeDetails.price > 0 && tradeDetails.units != 0, "Trade details retrieved");

      //      // Modify an open trade
      //      var request = new Dictionary<string, string>
      //              {
      //                  {"stopLoss", "0.4"}
      //              };
      //      var modifiedDetails = await Rest.PatchTradeAsync(_accountId, openTrades[0].id, request);
      //      _results.Verify(modifiedDetails.id > 0 && Math.Abs(modifiedDetails.stopLoss - 0.4) < float.Epsilon, "Trade modified");

      //      if (!_marketHalted)
      //      {
      //         // close an open trade
      //         var closedDetails = await Rest.DeleteTradeAsync(_accountId, openTrades[0].id);
      //         _results.Verify(closedDetails.id > 0, "Trade closed");
      //         _results.Verify(!string.IsNullOrEmpty(closedDetails.time), "Trade close details time");
      //         _results.Verify(!string.IsNullOrEmpty(closedDetails.side), "Trade close details side");
      //         _results.Verify(!string.IsNullOrEmpty(closedDetails.instrument), "Trade close details instrument");
      //         _results.Verify(closedDetails.price > 0, "Trade close details price");
      //         _results.Verify(closedDetails.profit != 0, "Trade close details profit");
      //      }
      //      else
      //      {
      //         _results.Add("Skipping: Trade delete test because market is halted");
      //      }
      //   }
      //   else
      //   {
      //      _results.Add("Skipping: Trade details test because no trades were found");
      //      _results.Add("Skipping: Trade modify test because no trades were found");
      //      _results.Add("Skipping: Trade delete test because no trades were found");
      //   }
      //}

      //private async Task PlaceMarketOrder()
      //{
      //   if (!_marketHalted)
      //   {
      //      // create new market order
      //      var request = new Dictionary<string, string>
      //              {
      //                  {"instrument", TestInstrument},
      //                  {"units", "1"},
      //                  {"side", "buy"},
      //                  {"type", "market"},
      //                  {"price", "1.0"}
      //              };
      //      var response = await Rest.PostOrderAsync(_accountId, request);
      //      // We're assuming we don't already have a position on the sell side
      //      _results.Verify(response.tradeOpened != null && response.tradeOpened.id > 0, "Trade successfully placed");
      //   }
      //   else
      //   {
      //      _results.Add("Skipping: Market open test because market is halted");
      //   }
      //}

      //private async Task RunOrdersTest()
      //{

      //   // 2013-12-06T20:36:06Z
      //   var expiry = DateTime.Now.AddMonths(1);

      //   // XmlConvert.ToDateTime and ToString can be used for going to/from RCF3339
      //   //string expiryString = XmlConvert.ToString(expiry, XmlDateTimeSerializationMode.Utc);

      //   // oco: dunno if this works yet .. req'd due to portable class library
      //   string expiryString = System.Xml.XmlConvert.ToString(expiry, "yyyy-MM-ddTHH:mm:ssZ");

      //   // create new pending order
      //   var request = new Dictionary<string, string>
      //          {
      //              {"instrument", TestInstrument},
      //              {"units", "1"},
      //              {"side", "buy"},
      //              {"type", "marketIfTouched"},
      //              {"expiry", expiryString},
      //              {"price", "1.0"}
      //          };
      //   var response = await Rest.PostOrderAsync(_accountId, request);
      //   _results.Verify(response.orderOpened != null && response.orderOpened.id > 0, "Order successfully opened");
      //   // Get open orders
      //   var orders = await Rest.GetOrderListAsync(_accountId);

      //   // Get order details
      //   if (orders.Count == 0)
      //   {
      //      _results.Add("Error: No orders to request details for...");
      //   }
      //   else
      //   {
      //      var order = await Rest.GetOrderDetailsAsync(_accountId, orders[0].id);
      //      _results.Verify(order.id > 0, "Order details retrieved");
      //   }

      //   // Modify an Existing order
      //   request["units"] += 10;
      //   var patchResponse = await Rest.PatchOrderAsync(_accountId, orders[0].id, request);
      //   _results.Verify(patchResponse.id > 0 && patchResponse.id == orders[0].id && patchResponse.units.ToString() == request["units"], "Order patched");

      //   // close an order
      //   var deletedOrder = await Rest.DeleteOrderAsync(_accountId, orders[0].id);
      //   _results.Verify(deletedOrder.id > 0 && deletedOrder.units == patchResponse.units, "Order deleted");
      //}

      //private async Task RunAccountsTest()
      //{
      //   // Get Account List
      //   List<Account> result;
      //   if (Credentials.GetDefaultCredentials().IsSandbox)
      //   {
      //      result = await Rest.GetAccountListAsync(Credentials.GetDefaultCredentials().Username);
      //   }
      //   else
      //   {
      //      result = await Rest.GetAccountListAsync();
      //   }
      //   _results.Verify(result.Count > 0, "Accounts are returned");
      //   foreach (var account in result)
      //   {
      //      _results.Verify(VerifyDefaultData(account), "Checking account data for " + account.accountId);
      //      // Get Account Information
      //      var accountDetails = await Rest.GetAccountDetailsAsync(account.accountId);
      //      _results.Verify(VerifyAllData(accountDetails), "Checking account details data for " + account.accountId);
      //   }


      //}

      //private async Task RunRatesTest()
      //{
      //   await RunPricesTest();
      //   new CandlesTest(_results).Run();
      //}

      //private async Task RunPricesTest()
      //{
      //   short count = 0;

      //   // Get a price list (basic, all instruments)
      //   List<string> instruments = new List<string>();
      //   _instruments.ForEach(x => instruments.Add(x.name));

      //   List<Price> result = await Rest20.GetPriceListAsync(_accountId, instruments);
      //   _results.Verify("03.0", result.Count == _instruments.Count, "Price returned for all " + _instruments.Count + " instruments");

      //   foreach (var price in result)
      //   {
      //      count++;
      //      _results.Verify("03." + count.ToString(), !string.IsNullOrEmpty(price.instrument), "price has instrument");

      //      count++;
      //      _results.Verify("03." + count.ToString(), price.asks[0].price > 0 && price.bids[0].price > 0, "Seemingly valid rates for instrument " + price.instrument);
      //   }
      //}



      //private bool VerifyAllData<T>(T entry)
      //{
      //   var fields = entry.GetType().GetFields().Where(x => x.Name.StartsWith("Has") && x.FieldType == typeof(bool));
      //   foreach (var field in fields)
      //   {
      //      if ((bool)field.GetValue(entry) == false)
      //      {
      //         _results.Add("Fail: " + field.Name + " is missing.");
      //         return false;
      //      }
      //   }
      //   return true;
      //}

      //private bool VerifyDefaultData<T>(T entry)
      //{
      //   var fields = entry.GetType().GetFields().Where(x => x.Name.StartsWith("Has") && x.FieldType == typeof(bool));
      //   foreach (var field in fields)
      //   {
      //      bool isOptional = (null != field.GetCustomAttributes(typeof(IsOptionalAttribute), true));
      //      bool valueIsPresent = (bool)field.GetValue(entry);
      //      // Data should be present iff it is not optional
      //      if (isOptional == valueIsPresent)
      //      {
      //         return false;
      //      }
      //   }
      //   return true;
      //}
      //#endregion

      //#region Test methods

      //[TestMethod]
      //public void test_retrieve_price_details()
      //{
      //   string key = "02.0";
      //   var results = _results.Items.Where(x => x.Key == key);
      //   var failure = results.FirstOrDefault(x => x.Value.Success = false);

      //   Assert.IsTrue(failure.Key == null, failure.Key + ": " + failure.Value);
      //}

      //[TestMethod]
      //public void test_retrieve_market_depth_details()
      //{
      //   // bids
      //   string key = "02.1";
      //   var results = _results.Items.Where(x => x.Key == key);
      //   var failure = results.FirstOrDefault(x => x.Value.Success = false);
      //   Assert.IsTrue(failure.Key == null, failure.Key + ": " + failure.Value);

      //   // asks
      //   key = "02.2";
      //   results = _results.Items.Where(x => x.Key == key);
      //   failure = results.FirstOrDefault(x => x.Value.Success = false);
      //   Assert.IsTrue(failure.Key == null, failure.Key + ": " + failure.Value);
      //}
      //#endregion
   }
}
