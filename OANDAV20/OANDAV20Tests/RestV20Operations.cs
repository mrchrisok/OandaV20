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
using OANDAV20.TradeLibrary.DataTypes.Communications.Requests;
using OANDAV20.TradeLibrary.DataTypes.Order;

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
      static string _lastTransactionTime;

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

               await Transaction_GetTransactionsByDateRange();
               await Transaction_GetTransactionsSinceId();

               await Pricing_GetPricing();

               await Instrument_GetInstrumentCandles();

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
               await Order_RunOrderOperations();
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

         _lastTransactionID = summary.lastTransactionID;

         string alias = summary.alias;
         double? marginRate = summary.marginRate;

         string testAlias = "testAlias";
         double? testMarginRate = marginRate == null ? 0.5 : ((marginRate == 0.5) ? 0.4 : 0.5);
         Dictionary<string, string> accountConfig = new Dictionary<string, string>();
         accountConfig.Add("alias", testAlias);
         accountConfig.Add("marginRate", testMarginRate.ToString());

         _lastTransactionTime = ConvertDateTimeToAcceptDateFormat(DateTime.UtcNow, AcceptDatetimeFormat.RFC3339);

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

      #region Instrument
      // <summary>
      // Retrieve the full details for the test accountId
      // </summary>
      private static async Task Instrument_GetInstrumentCandles()
      {
         short count = 500;
         string price = "MBA";
         string instrument = InstrumentName.Currency.EURUSD;
         string granularity = CandleStickGranularity.Hours01;

         var parameters = new Dictionary<string, string>();
         parameters.Add("price", price);
         parameters.Add("granularity", granularity);
         parameters.Add("count", count.ToString());

         List<CandlestickPlus> result = await Rest20.GetCandlesAsync(instrument, parameters);
         CandlestickPlus candle = result.FirstOrDefault();

         _results.Verify("19.0", result != null, "Candles list received.");
         _results.Verify("19.1", result.Count() == count, "Candles count is correct.");
         _results.Verify("19.2", candle.instrument == instrument, "Candles instrument is correct.");
         _results.Verify("19.3", candle.granularity == granularity, "Candles granularity is correct.");
         _results.Verify("19.4", candle.mid != null && candle.mid.o > 0, "Candle has mid prices");
         _results.Verify("19.5", candle.bid != null && candle.bid.o > 0, "Candle has bid prices");
         _results.Verify("19.6", candle.ask != null && candle.ask.o > 0, "Candle has ask prices");
      }
      #endregion

      #region Order
      private static async Task Order_RunOrderOperations()
      {
         string instrument = InstrumentName.Currency.EURUSD;
         string expiry = ConvertDateTimeToAcceptDateFormat(DateTime.Now.AddMonths(1));

         // create new pending order
         var request = new MarketIfTouchedOrderRequest()
         {
            instrument = instrument,
            units = 1, // buy
            timeInForce = TimeInForce.GoodUntilDate,
            gtdTime = expiry,
            price = 1.0,
            priceBound = 1.01,
            clientExtensions = new ClientExtensions()
            {
               id = "test_order_1",
               comment = "test order comment",
               tag = "test_order"
            },
            tradeClientExtensions = new ClientExtensions()
            {
               id = "test_trade_1",
               comment = "test trade comment",
               tag = "test_trade"
            }
         };

         // first, kill any open orders
         var openOrders = await Rest20.GetPendingOrderListAsync(_accountId);
         openOrders.ForEach(async x => await Rest20.CancelOrderAsync(_accountId, x.id));

         // create order 
         var response = await Rest20.PostOrderAsync(_accountId, request);
         var orderTransaction = response.orderCreateTransaction;

         _results.Verify("11.0", orderTransaction != null && orderTransaction.id > 0, "Order successfully opened");
         _results.Verify("11.1", orderTransaction.type == TransactionType.MarketIfTouchedOrder, "Order type is correct.");

         // Get all orders
         var allOrders = await Rest20.GetOrderListAsync(_accountId);
         _results.Verify("11.2", allOrders != null && allOrders.Count > 0, "All orders list successfully retrieved");
         _results.Verify("11.3", allOrders.FirstOrDefault(x => x.id == orderTransaction.id) != null, "Test order in all orders return.");

         // Get pending orders
         var pendingOrders = await Rest20.GetPendingOrderListAsync(_accountId);
         _results.Verify("11.4", pendingOrders != null && pendingOrders.Count > 0, "Pending orders list successfully retrieved");
         _results.Verify("11.5", pendingOrders.Where(x => x.state != OrderState.Pending).Count() == 0, "Only pending orders returned.");
         _results.Verify("11.6", pendingOrders.FirstOrDefault(x => x.id == orderTransaction.id) != null, "Test order in pending orders return.");

         // Get order details
         var order = await Rest20.GetOrderDetailsAsync(_accountId, pendingOrders[0].id) as MarketIfTouchedOrder;
         _results.Verify("11.7", order != null && order.id == orderTransaction.id, "Order details successfully retrieved.");
         _results.Verify("11.8", (order.clientExtensions == null) == (request.clientExtensions == null), "order client extensions successfully retrieved.");
         _results.Verify("11.9", (order.tradeClientExtensions == null) == (request.tradeClientExtensions == null), "order trade client extensions successfully retrieved.");

         // Udpate extensions
         var newOrderExtensions = request.clientExtensions;
         newOrderExtensions.comment = "updated order comment";
         var newTradeExtensions = request.tradeClientExtensions;
         newTradeExtensions.comment = "updated trade comment";
         var extensionsModifyResponse = await Rest20.ModifyClientExtensionsAsync(_accountId, order.id, newOrderExtensions, newTradeExtensions);

         _results.Verify("11.10", extensionsModifyResponse != null, "Order extensions update received successfully.");
         _results.Verify("11.11", extensionsModifyResponse.orderClientExtensionsModifyTransaction.orderID == order.id, "Correct order extensions updated.");
         _results.Verify("11.12", extensionsModifyResponse.orderClientExtensionsModifyTransaction.clientExtensionsModify.comment == "updated order comment", "Order extensions comment updated successfully.");
         _results.Verify("11.13", extensionsModifyResponse.orderClientExtensionsModifyTransaction.tradeClientExtensionsModify.comment == "updated trade comment", "Order trade extensions comment updated successfully.");

         // Cancel & Replace an existing order
         request.units += 10;
         var cancelReplaceResponse = await Rest20.CancelReplaceOrderAsync(_accountId, order.id, request);
         var cancelTransaction = cancelReplaceResponse.orderCancelTransaction;
         var newOrderTransaction = cancelReplaceResponse.orderCreateTransaction;

         _results.Verify("11.14", cancelTransaction != null && cancelTransaction.orderID == order.id, "Order ancel+replace cancelled successfully.");
         _results.Verify("11.15", newOrderTransaction != null && newOrderTransaction.id > 0 && newOrderTransaction.id != order.id, "Order cancel+replace replaced successfully.");

         // Get new order details
         var newOrder = await Rest20.GetOrderDetailsAsync(_accountId, newOrderTransaction.id) as MarketIfTouchedOrder;
         _results.Verify("11.16", newOrder != null && newOrder.units == request.units, "New order details are correct.");

         // Cancel an order
         var cancelOrderResponse = await Rest20.CancelOrderAsync(_accountId, newOrder.id);
         _results.Verify("11.17", cancelOrderResponse != null, "Cancelled order retrieved successfully.");

         var cancelTransaction2 = cancelOrderResponse.orderCancelTransaction;
         _results.Verify("11.18", cancelTransaction2.orderID == newOrder.id, "Order cancelled successfully.");
      }
      #endregion

      #region Transaction
      private static async Task Transaction_GetTransactionsByDateRange()
      {
         // 09
         Dictionary<string, string> parameters = new Dictionary<string, string>();
         parameters.Add("from", _lastTransactionTime);
         parameters.Add("type", TransactionType.ClientConfigure);

         List<ITransaction> results = await Rest20.GetTransactionsByDateRangeAsync(_accountId, parameters);

         _results.Verify("09.0", results != null, string.Format("Transactions info received.", _accountId));
         _results.Verify("09.1", results.Where(x => x.type == TransactionType.ClientConfigure).Count() > 0, "Client configure transactions returned.");
         _results.Verify("09.2", results.Where(x => x.type != TransactionType.ClientConfigure).Count() > 0, "Non-client configure transactions returned.");
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
