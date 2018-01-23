using Microsoft.VisualStudio.TestTools.UnitTesting;
using OkonkwoOandaV20;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Account;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Instrument;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Pricing;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Stream;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Transaction;
using OkonkwoOandaV20.TradeLibrary.Primitives;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Linq;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Order;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Position;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order;
using System.IO;

namespace OkonkwoOandaV20Tests
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/introduction/
   /// </summary>
   public partial class Restv20Test
   {
      #region Declarations
      static bool m_ApiOperationsComplete = false;
      static EEnvironment m_TestEnvironment;
      static string m_TestToken;
      static string m_TestAccount;
      static short m_TokenAccounts;
      static string m_Currency = "USD";
      static string m_TestInstrument = InstrumentName.Currency.EURUSD;
      static List<Instrument> m_Instruments;
      static long m_FirstTransactionID;
      static long m_LastTransactionID;
      static string m_LastTransactionTime;

      protected List<Price> _prices;
      #endregion

      static string AccountId { get { return Credentials.GetDefaultCredentials().DefaultAccountId; } }

      [ClassInitialize]
      public static async void RunApiOperations(TestContext context)
      {
         try
         {
            await SetPracticeApiCredentials();

            if (Credentials.GetDefaultCredentials() == null)
            {
               throw new Exception("Exception: RestV20Test - Credentials must be defined to run this test.");
            }

            if (Credentials.GetDefaultCredentials().HasServer(EServer.Account))
            {
               // first, get accounts
               // this operation adds the test AccountId to Credentials (if it is null)
               await Account_GetAccountsList(m_TokenAccounts);

               // second, check market status
               await Initialize_GetMarketStatus();

               // third, proceed with all others
               await Account_GetAccountDetails();
               await Account_GetAccountSummary();
               await Account_GetAccountsInstruments();
               await Account_GetSingleAccountInstrument();
               await Account_PatchAccountConfiguration();

               await Transaction_GetTransactionDetails();
               await Transaction_GetTransactionsSinceId();

               await Pricing_GetPricing();

               await Instrument_GetInstrumentCandles();

               // test the pricing stream
               if (Credentials.GetDefaultCredentials().HasServer(EServer.StreamingPrices))
               {
                  Stream_GetStreamingPrices();
               }

               // start transactions stream
               Task transactionsStreamCheck = null;
               if (Credentials.GetDefaultCredentials().HasServer(EServer.StreamingTransactions))
               {
                  transactionsStreamCheck = Stream_GetStreamingTransactions();
               }

               // create stream traffic
               await Order_RunOrderOperations();
               await Trade_RunTradeOperations();
               await Position_RunPositionOperations();

               // stop transactions stream 
               if (transactionsStreamCheck != null)
               {
                  await transactionsStreamCheck;
               }

               // review the traffic
               await Transaction_GetTransactionsByDateRange();
               await Transaction_GetTransactionsByIdRange();
               await Account_GetAccountChanges();
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
            m_ApiOperationsComplete = true;
         }
      }

      private static async Task Initialize_GetMarketStatus()
      {
         bool marketIsHalted = await Utilities.IsMarketHalted();
         m_Results.Verify("00.0", marketIsHalted, "Market is halted.");
         if (marketIsHalted) throw new MarketHaltedException("Unable to continue tests. OANDA Fx market is halted!");
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
         m_Results.Verify("01.0", result.Count > 0, "Account list received.");

         string message = "Correct number of accounts received.";
         bool correctCount = true;
         if (listCount.HasValue && listCount.Value > 0)
         {
            count++;
            correctCount = result.Count == listCount;
            message = string.Format("Correct number of accounts ({0}) received.", result.Count);
         }
         m_Results.Verify("01." + count.ToString(), correctCount, message);

         foreach (var account in result)
         {
            count++;
            string description = string.Format("Account id {0} has correct format.", account.id);
            m_Results.Verify("01." + count.ToString(), account.id.Split('-').Length == 4, description);
         }

         // ensure the first account has sufficient funds
         m_TestAccount = m_TestAccount ?? result.OrderBy(r => r.id).First().id;
         Credentials.SetCredentials(m_TestEnvironment, m_TestToken, m_TestAccount);
      }

      // <summary>
      // Retrieve the full details for the test accountId
      // </summary>
      private static async Task Account_GetAccountDetails()
      {
         Account result = await Rest20.GetAccountDetailsAsync(AccountId);

         m_Results.Verify("08.0", result != null, string.Format("Account {0} info received.", AccountId));
         m_Results.Verify("08.1", result.id == AccountId, string.Format("Account id ({0}) is correct.", result.id));
         m_Results.Verify("08.2", result.currency == m_Currency, string.Format("Account currency ({0}) is correct.", result.currency));
         m_Results.Verify("08.3", result.openTradeCount == 0, string.Format("Account openTradeCount ({0}) is correct.", 0));
         m_Results.Verify("08.4", result.trades.Count == result.openTradeCount, string.Format("Account trades count ({0}) is correct.", 0));
      }

      /// <summary>
      /// Retrieve the list of instruments associated with the given accountId
      /// </summary>
      private static async Task Account_GetAccountsInstruments()
      {
         // Get an instrument list (basic)
         List<Instrument> result = await Rest20.GetAccountInstrumentsAsync(AccountId);
         m_Results.Verify("02.0", result.Count > 0, "Instrument list received");

         // Store the instruments for other tests
         m_Instruments = result;
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
         List<Instrument> result = await Rest20.GetAccountInstrumentsAsync(AccountId, eurusd);
         m_Results.Verify("03.0", result.Count == 1, string.Format("{0} info received.", instrument));
         m_Results.Verify("03.1", result[0].type == type, string.Format("{0} type ({1}) is correct.", instrument, result[0].type));
         m_Results.Verify("03.2", result[0].name == instrument, string.Format("{0} name ({1}) is correct.", instrument, result[0].name));
      }

      /// <summary>
      /// Retrieve summary information for the given accountId
      /// </summary>
      private static async Task Account_GetAccountSummary()
      {
         // 04
         AccountSummary result = await Rest20.GetAccountSummaryAsync(AccountId);
         m_Results.Verify("04.0", result != null, string.Format("Account {0} info received.", AccountId));
         m_Results.Verify("04.1", result.id == AccountId, string.Format("AccounSummary.id ({0}) is correct.", result.id));
         m_Results.Verify("04.2", result.currency == m_Currency, string.Format("AccountSummary.currency ({0}) is correct.", result.currency));
      }

      /// <summary>
      /// Retrieve summary information for the given accountId
      /// </summary>
      private static async Task Account_PatchAccountConfiguration()
      {
         // 05
         AccountSummary summary = await Rest20.GetAccountSummaryAsync(AccountId);

         m_FirstTransactionID = summary.lastTransactionID;
         m_LastTransactionID = summary.lastTransactionID;

         string alias = summary.alias;
         double? marginRate = summary.marginRate;

         string testAlias = "testAlias";
         double? testMarginRate = marginRate == null ? 0.5 : ((marginRate == 0.5) ? 0.4 : 0.5);
         Dictionary<string, string> accountConfig = new Dictionary<string, string>();
         accountConfig.Add("alias", testAlias);
         accountConfig.Add("marginRate", testMarginRate.ToString());

         m_LastTransactionTime = ConvertDateTimeToAcceptDateFormat(DateTime.UtcNow, AcceptDatetimeFormat.RFC3339);

         AccountConfigurationResponse response = await Rest20.PatchAccountConfigurationAsync(AccountId, accountConfig);
         ClientConfigureTransaction newConfig = response.clientConfigureTransaction;

         m_Results.Verify("05.0", newConfig != null, string.Format("Account configuration retrieved successfully.", newConfig.alias));
         m_Results.Verify("05.1", newConfig.alias != alias, string.Format("Account alias {0} updated successfully.", newConfig.alias));
         m_Results.Verify("05.2", newConfig.marginRate != marginRate, string.Format("Account marginRate {0} updated succesfully.", newConfig.marginRate));

         accountConfig["alias"] = alias;
         accountConfig["marginRate"] = (marginRate ?? 1.0).ToString();
         AccountConfigurationResponse response2 = await Rest20.PatchAccountConfigurationAsync(AccountId, accountConfig);
         ClientConfigureTransaction newConfig2 = response2.clientConfigureTransaction;

         m_Results.Verify("05.3", newConfig2.alias == alias, string.Format("Account alias {0} reverted successfully.", newConfig2.alias));
         m_Results.Verify("05.4", newConfig2.marginRate == marginRate, string.Format("Account marginRate {0} reverted succesfully.", newConfig2.marginRate));
      }

      private static async Task Account_GetAccountChanges()
      {
         //17
         AccountChangesResponse result = await Rest20.GetAccountChangesAsync(AccountId, m_FirstTransactionID);

         var changes = result.changes;

         m_Results.Verify("17.0", result != null, "Account changes received.");
         m_Results.Verify("17.1", changes.ordersFilled.Count > 0, "Account has filled orders.");
         m_Results.Verify("17.2", changes.ordersCancelled.Count > 0, "Account has cancelled orders.");
         m_Results.Verify("17.3", changes.tradesClosed.Count > 0, "Account has closed trades.");
         m_Results.Verify("17.4", changes.positions.Count > 0, "Account has position.");
         m_Results.Verify("17.5", result.state.marginAvailable > 0, "Account has transactions.");
      }
      #endregion

      #region Instrument
      // <summary>
      // Retrieve the full details for the test accountId
      // </summary>
      private static async Task Instrument_GetInstrumentCandles()
      {
         short count = 500;

         // get all price types .. MBA
         string price = string.Concat(CandleStickPriceType.Midpoint, CandleStickPriceType.Bid, CandleStickPriceType.Ask);

         string instrument = InstrumentName.Currency.EURUSD;
         string granularity = CandleStickGranularity.Hours01;

         var parameters = new Dictionary<string, string>();
         parameters.Add("price", price);
         parameters.Add("granularity", granularity);
         parameters.Add("count", count.ToString());

         List<CandlestickPlus> result = await Rest20.GetCandlesAsync(instrument, parameters);
         CandlestickPlus candle = result.FirstOrDefault();

         m_Results.Verify("12.0", result != null, "Candles list received.");
         m_Results.Verify("12.1", result.Count() == count, "Candles count is correct.");
         m_Results.Verify("12.2", candle.instrument == instrument, "Candles instrument is correct.");
         m_Results.Verify("12.3", candle.granularity == granularity, "Candles granularity is correct.");
         m_Results.Verify("12.4", candle.mid != null && candle.mid.o > 0, "Candle has mid prices");
         m_Results.Verify("12.5", candle.bid != null && candle.bid.o > 0, "Candle has bid prices");
         m_Results.Verify("12.6", candle.ask != null && candle.ask.o > 0, "Candle has ask prices");
      }
      #endregion

      #region Order
      /// <summary>
      /// Runs operations available at OANDA's Order endpoint
      /// </summary>
      /// <returns></returns>
      private static async Task Order_RunOrderOperations()
      {
         if (await Utilities.IsMarketHalted())
            throw new MarketHaltedException("OANDA Fx market is halted!");

         string expiry = ConvertDateTimeToAcceptDateFormat(DateTime.Now.AddMonths(1));

         #region create new pending order
         var request1 = new MarketIfTouchedOrderRequest()
         {
            instrument = m_TestInstrument,
            units = 1, // buy
            timeInForce = TimeInForce.GoodUntilDate,
            gtdTime = expiry,
            price = 1.0,
            priceBound = 1.01,
            clientExtensions = new ClientExtensions()
            {
               id = "test_order_1",
               comment = "test order 1 comment",
               tag = "test_order_1"
            },
            tradeClientExtensions = new ClientExtensions()
            {
               id = "test_trade_1",
               comment = "test trade 1 comment",
               tag = "test_trade_1"
            }
         };

         // first, kill any open orders
         var openOrders = await Rest20.GetPendingOrderListAsync(AccountId);
         openOrders.ForEach(async x => await Rest20.CancelOrderAsync(AccountId, x.id));

         // create order
         var response = await Rest20.PostOrderAsync(AccountId, request1);
         var orderTransaction = response.orderCreateTransaction;

         m_Results.Verify("11.0", orderTransaction != null && orderTransaction.id > 0, "Order successfully opened");
         m_Results.Verify("11.1", orderTransaction.type == TransactionType.MarketIfTouchedOrder, "Order type is correct.");

         // Get all orders
         var allOrders = await Rest20.GetOrderListAsync(AccountId);
         m_Results.Verify("11.2", allOrders != null && allOrders.Count > 0, "All orders list successfully retrieved");
         m_Results.Verify("11.3", allOrders.FirstOrDefault(x => x.id == orderTransaction.id) != null, "Test order in all orders return.");

         // Get pending orders
         var pendingOrders = await Rest20.GetPendingOrderListAsync(AccountId);
         m_Results.Verify("11.4", pendingOrders != null && pendingOrders.Count > 0, "Pending orders list successfully retrieved");
         m_Results.Verify("11.5", pendingOrders.Where(x => x.state != OrderState.Pending).Count() == 0, "Only pending orders returned.");
         m_Results.Verify("11.6", pendingOrders.FirstOrDefault(x => x.id == orderTransaction.id) != null, "Test order in pending orders return.");

         // Get order details
         var order = await Rest20.GetOrderDetailsAsync(AccountId, pendingOrders[0].id) as MarketIfTouchedOrder;
         m_Results.Verify("11.7", order != null && order.id == orderTransaction.id, "Order details successfully retrieved.");
         m_Results.Verify("11.8", (order.clientExtensions == null) == (request1.clientExtensions == null), "order client extensions successfully retrieved.");
         m_Results.Verify("11.9", (order.tradeClientExtensions == null) == (request1.tradeClientExtensions == null), "order trade client extensions successfully retrieved.");

         // Udpate extensions
         var newOrderExtensions = request1.clientExtensions;
         newOrderExtensions.comment = "updated order 1 comment";
         var newTradeExtensions = request1.tradeClientExtensions;
         newTradeExtensions.comment = "updated trade 1 comment";
         var extensionsModifyResponse = await Rest20.ModifyOrderClientExtensionsAsync(AccountId, order.id, newOrderExtensions, newTradeExtensions);

         m_Results.Verify("11.10", extensionsModifyResponse != null, "Order extensions update received successfully.");
         m_Results.Verify("11.11", extensionsModifyResponse.orderClientExtensionsModifyTransaction.orderID == order.id, "Correct order extensions updated.");
         m_Results.Verify("11.12", extensionsModifyResponse.orderClientExtensionsModifyTransaction.clientExtensionsModify.comment == "updated order 1 comment", "Order extensions comment updated successfully.");
         m_Results.Verify("11.13", extensionsModifyResponse.orderClientExtensionsModifyTransaction.tradeClientExtensionsModify.comment == "updated trade 1 comment", "Order trade extensions comment updated successfully.");

         // Cancel & Replace an existing order
         request1.units += 10;
         var cancelReplaceResponse = await Rest20.CancelReplaceOrderAsync(AccountId, order.id, request1);
         var cancelTransaction = cancelReplaceResponse.orderCancelTransaction;
         var newOrderTransaction = cancelReplaceResponse.orderCreateTransaction;

         m_Results.Verify("11.14", cancelTransaction != null && cancelTransaction.orderID == order.id, "Order ancel+replace cancelled successfully.");
         m_Results.Verify("11.15", newOrderTransaction != null && newOrderTransaction.id > 0 && newOrderTransaction.id != order.id, "Order cancel+replace replaced successfully.");

         // Get new order details
         var newOrder = await Rest20.GetOrderDetailsAsync(AccountId, newOrderTransaction.id) as MarketIfTouchedOrder;
         m_Results.Verify("11.16", newOrder != null && newOrder.units == request1.units, "New order details are correct.");

         // Cancel an order
         var cancelOrderResponse = await Rest20.CancelOrderAsync(AccountId, newOrder.id);
         m_Results.Verify("11.17", cancelOrderResponse != null, "Cancelled order retrieved successfully.");
         var cancelTransaction2 = cancelOrderResponse.orderCancelTransaction;
         m_Results.Verify("11.18", cancelTransaction2.orderID == newOrder.id, "Order cancelled successfully.");
         #endregion

         #region Create new pending order with exit orders

         // need this for rounding, etc.
         var owxInstrument = m_Instruments.FirstOrDefault(x => x.name == m_TestInstrument);

         double owxOrderPrice = Math.Round(1.0, owxInstrument.displayPrecision);
         double owxStopLossPrice = Math.Round(owxOrderPrice - (0.10 * owxOrderPrice), owxInstrument.displayPrecision);
         double owxTakeProfitPrice = Math.Round(owxOrderPrice + (0.10 * owxOrderPrice), owxInstrument.displayPrecision);

         var owxRequest = new LimitOrderRequest()
         {
            instrument = m_TestInstrument,
            units = 1, // buy
            timeInForce = TimeInForce.GoodUntilDate,
            gtdTime = expiry,
            price = owxOrderPrice,
            priceBound = Math.Round(owxOrderPrice + (0.01 * owxOrderPrice), owxInstrument.displayPrecision),
            stopLossOnFill = new StopLossDetails(owxInstrument)
            {
               timeInForce = TimeInForce.GoodUntilCancelled,
               price = owxStopLossPrice
            },
            takeProfitOnFill = new TakeProfitDetails(owxInstrument)
            {
               timeInForce = TimeInForce.GoodUntilCancelled,
               price = owxTakeProfitPrice
            }
         };

         // create order with exit orders
         var owxResponse = await Rest20.PostOrderAsync(AccountId, owxRequest);
         var owxOrderTransaction = owxResponse.orderCreateTransaction;
         m_Results.Verify("11.19", owxOrderTransaction != null && owxOrderTransaction.id > 0, "Order with exit orders successfully opened");
         m_Results.Verify("11.20", owxOrderTransaction.type == TransactionType.LimitOrder, "Order with exit orders type is correct.");

         // get order with exit orders details
         var pendingOrders2 = await Rest20.GetPendingOrderListAsync(AccountId);
         var order2 = await Rest20.GetOrderDetailsAsync(AccountId, pendingOrders2[0].id) as LimitOrder;
         m_Results.Verify("11.21", order2 != null && order2.stopLossOnFill.price == owxStopLossPrice, "Order with exit orders stoploss details are correct.");
         m_Results.Verify("11.22", order2 != null && order2.takeProfitOnFill.price == owxTakeProfitPrice, "Order with exit orders takeprofit details are correct.");

         // cancel & replace order with exit orders - update stoploss and takeprofit
         owxStopLossPrice = Math.Round(owxOrderPrice - (0.15 * owxOrderPrice), owxInstrument.displayPrecision);
         owxRequest.stopLossOnFill = new StopLossDetails(owxInstrument) { price = owxStopLossPrice };

         owxTakeProfitPrice = Math.Round(owxOrderPrice + (0.15 * owxOrderPrice), owxInstrument.displayPrecision);
         owxRequest.takeProfitOnFill = new TakeProfitDetails(owxInstrument) { price = owxTakeProfitPrice };

         var owxCancelReplaceResponse = await Rest20.CancelReplaceOrderAsync(AccountId, order2.id, owxRequest);
         var owxCancelTransaction = owxCancelReplaceResponse.orderCancelTransaction;
         var owxNewOrderTransaction = owxCancelReplaceResponse.orderCreateTransaction;

         m_Results.Verify("11.23", owxCancelTransaction != null && owxCancelTransaction.orderID == order2.id, "Order with exit orders cancel+replace cancelled successfully.");
         m_Results.Verify("11.24", owxCancelTransaction != null && owxCancelTransaction.reason == OrderCancelReason.ClientRequestReplaced, "Order with exit orders cancel+replace has correct reason.");
         m_Results.Verify("11.25", owxNewOrderTransaction != null && owxNewOrderTransaction.id > 0 && owxNewOrderTransaction.id == owxCancelTransaction.replacedByOrderID, "Order with exit orders cancel+replace replaced successfully.");

         // Get order with exit orders replacement order details
         var owxOrderTransaction2 = owxNewOrderTransaction as LimitOrderTransaction;
         m_Results.Verify("11.26", owxOrderTransaction2 != null && owxOrderTransaction2.reason == LimitOrderReason.Replacement, "Order with exit orders replacement order reason is correct.");
         m_Results.Verify("11.27", owxOrderTransaction2 != null && owxOrderTransaction2.stopLossOnFill.price == owxStopLossPrice, "Order with exit orders replacement order stoploss is correct.");
         m_Results.Verify("11.28", owxOrderTransaction2 != null && owxOrderTransaction2.takeProfitOnFill.price == owxTakeProfitPrice, "Order with exit orders replacement order takeprofit is correct.");
         #endregion
      }

      /// <summary>
      /// Places a market order.
      /// </summary>
      /// <param name="key">The key root used to store the order success and order fill results.</param>
      /// <returns></returns>
      private static async Task PlaceMarketOrder(string key, double units = 0, bool closeAllTrades = true)
      {
         // I'm fine with a throw here
         // To each his/her own on doing something different.
         if (await Utilities.IsMarketHalted())
            throw new MarketHaltedException("OANDA Fx market is halted!");

         if (closeAllTrades)
         {
            var closeList = await Rest20.GetOpenTradeListAsync(AccountId);
            closeList.ForEach(async x => await Rest20.CloseTradeAsync(AccountId, x.id));
         }

         // this should have a value by now
         var instrument = m_Instruments.FirstOrDefault(x => x.name == m_TestInstrument);
         if (units == 0) units = instrument.minimumTradeSize;

         var request = new MarketOrderRequest()
         {
            instrument = m_TestInstrument,
            units = units, // buy
            clientExtensions = new ClientExtensions()
            {
               id = "test_market_order_1",
               comment = "test market order comment",
               tag = "test_market_order"
            },
            tradeClientExtensions = new ClientExtensions()
            {
               id = "test_market_trade_1",
               comment = "test market trade comment",
               tag = "test_market_trade"
            }
         };

         var response = await Rest20.PostOrderAsync(AccountId, request);
         var createTransaction = response.orderCreateTransaction;
         var fillTransaction = response.orderFillTransaction;

         m_Results.Verify(key + ".0", createTransaction != null && createTransaction.id > 0, "Market order successfully placed.");
         m_Results.Verify(key + ".1", fillTransaction != null && fillTransaction.id > 0, "Market order successfully filled.");
      }
      #endregion

      #region Trade
      /// <summary>
      /// Runs operations available at OANDA's Trade endpoint
      /// </summary>
      /// <returns></returns>
      private static async Task Trade_RunTradeOperations()
      {
         await PlaceMarketOrder("13");

         // get list of trades
         var trades = await Rest20.GetTradeListAsync(AccountId);
         m_Results.Verify("13.2", trades.Count > 0 && trades[0].id > 0, "Trades list retrieved.");

         // get list of open trades
         var openTrades = await Rest20.GetOpenTradeListAsync(AccountId);
         m_Results.Verify("13.3", openTrades.Count > 0 && openTrades[0].id > 0, "Open trades list retrieved.");

         if (openTrades.Count == 0)
            throw new InvalidOperationException("Trade test operations cannot continue without an open trade.");

         // get details for a trade
         var trade = await Rest20.GetTradeDetailsAsync(AccountId, openTrades[0].id);
         m_Results.Verify("13.4", trade.id > 0 && trade.price > 0 && trade.initialUnits != 0, "Trade details retrieved");

         // Udpate extensions
         var updatedExtensions = trade.clientExtensions;
         updatedExtensions.comment = "updated test market trade comment";
         var extensionsModifyResponse = await Rest20.ModifyTradeClientExtensionsAsync(AccountId, trade.id, updatedExtensions);

         m_Results.Verify("13.5", extensionsModifyResponse != null, "Order extensions update received successfully.");
         m_Results.Verify("13.6", extensionsModifyResponse.tradeClientExtensionsModifyTransaction.tradeID == trade.id, "Correct trade extensions updated.");
         m_Results.Verify("13.7", extensionsModifyResponse.tradeClientExtensionsModifyTransaction.tradeClientExtensionsModify.comment == "updated test market trade comment", "Trade extensions comment updated successfully.");

         // need this for rounding, etc.
         var instrument = m_Instruments.FirstOrDefault(x => x.name == trade.instrument);

         // Add a takeProfit to an open trade
         double takeProfitPrice = Math.Round(1.10 * trade.price, instrument.displayPrecision);
         var takeProfit = new TakeProfitDetails(instrument)
         {
            price = takeProfitPrice,
            timeInForce = TimeInForce.GoodUntilCancelled,
            clientExtensions = new ClientExtensions()
            {
               id = "take_profit_1",
               comment = "take profit comment",
               tag = "take_profit"
            }
         };
         var patch1 = new PatchExitOrdersRequest() { takeProfit = takeProfit };
         var takeProfitPatch = (await Rest20.PatchTradeExitOrders(AccountId, trade.id, patch1)).takeProfitOrderTransaction;
         m_Results.Verify("13.8", takeProfitPatch != null && takeProfitPatch.id > 0, "Take profit patch received.");
         m_Results.Verify("13.9", takeProfitPatch.price == takeProfitPrice, "Trade patched with take profit.");

         // Add a stopLoss to an open trade
         double stopLossPrice = Math.Round(trade.price - (0.10 * trade.price), instrument.displayPrecision);
         var stopLoss = new StopLossDetails(instrument)
         {
            price = stopLossPrice,
            timeInForce = TimeInForce.GoodUntilCancelled,
            clientExtensions = new ClientExtensions()
            {
               id = "stop_loss_1",
               comment = "stop loss comment",
               tag = "stop_loss"
            }
         };
         var patch2 = new PatchExitOrdersRequest() { stopLoss = stopLoss };
         var stopLossPatch = (await Rest20.PatchTradeExitOrders(AccountId, trade.id, patch2)).stopLossOrderTransaction;
         m_Results.Verify("13.10", takeProfitPatch != null && takeProfitPatch.id > 0, "Stop loss patch received.");
         m_Results.Verify("13.11", stopLossPatch.price == stopLossPrice, "Trade patched with stop loss.");

         // Add a trailingStopLoss to an open trade
         double distance = Math.Round(2 * (trade.price - stopLoss.price), instrument.displayPrecision);
         var trailingStopLoss = new TrailingStopLossDetails(instrument)
         {
            distance = distance,
            timeInForce = TimeInForce.GoodUntilCancelled,
            clientExtensions = new ClientExtensions()
            {
               id = "trailing_stop_loss_1",
               comment = "trailing stop loss comment",
               tag = "trailing_stop_loss"
            }
         };
         var patch3 = new PatchExitOrdersRequest() { trailingStopLoss = trailingStopLoss };
         var trailingStopLossPatch = (await Rest20.PatchTradeExitOrders(AccountId, trade.id, patch3)).trailingStopLossOrderTransaction;
         m_Results.Verify("13.12", trailingStopLossPatch != null && trailingStopLossPatch.id > 0, "Trailing stop loss patch received.");
         m_Results.Verify("13.13", trailingStopLossPatch.distance == distance, "Trade patched with trailing stop loss.");

         // remove dependent orders
         var parameters = new Dictionary<string, object>();
         parameters.Add("takeProfit", null);
         parameters.Add("stopLoss", null);
         parameters.Add("trailingStopLoss", null);
         var result = await Rest20.CancelTradeExitOrders(AccountId, trade.id, parameters);
         m_Results.Verify("13.14", result.takeProfitOrderCancelTransaction != null, "Take profit cancelled.");
         m_Results.Verify("13.15", result.stopLossOrderCancelTransaction != null, "Stop loss cancelled.");
         m_Results.Verify("13.16", result.trailingStopLossOrderCancelTransaction != null, "Trailing stop loss cancelled.");

         if (await Utilities.IsMarketHalted())
            throw new MarketHaltedException("OANDA Fx market is halted!");

         // close an open trade
         var closedDetails = (await Rest20.CloseTradeAsync(AccountId, trade.id)).orderFillTransaction;
         m_Results.Verify("13.17", closedDetails.id > 0, "Trade closed");
         m_Results.Verify("13.18", !string.IsNullOrEmpty(closedDetails.time), "Trade close details has time.");
         m_Results.Verify("13.19", !string.IsNullOrEmpty(closedDetails.instrument), "Trade close details instrument correct.");
         m_Results.Verify("13.20", closedDetails.units == -1 * trade.initialUnits, "Trade close details units correct.");
         m_Results.Verify("13.21", closedDetails.price > 0, "Trade close details has price.");
      }
      #endregion

      #region Position
      /// <summary>
      /// Runs operations available at OANDA's Position endpoint
      /// </summary>
      /// <returns></returns>
      private static async Task Position_RunPositionOperations()
      {
         if (await Utilities.IsMarketHalted())
            throw new MarketHaltedException("OANDA Fx market is halted!");

         short units = 1;

         // Make sure there's a position to test
         await PlaceMarketOrder("14", units);

         // get list of positions
         var positions = await Rest20.GetPositionsAsync(AccountId);
         m_Results.Verify("14.2", positions.Count > 0, "All positions retrieved");

         // get list of open positions
         var openPositions = await Rest20.GetOpenPositionsAsync(AccountId);
         m_Results.Verify("14.3", openPositions.Count > 0, "Open positions retrieved");

         short increment = 4;
         foreach (var position in positions)
         {
            increment = VerifyPosition(position, increment);
         }

         // get position for a given instrument
         var onePosition = await Rest20.GetPositionAsync(AccountId, m_TestInstrument);
         increment = VerifyPosition(onePosition, increment);

         // closeout a position
         var request = new ClosePositionRequest() { longUnits = "ALL" };
         var response = await Rest20.ClosePositionAsync(AccountId, m_TestInstrument, request);
         m_LastTransactionID = response.lastTransactionID;
         m_Results.Verify("14." + increment.ToString(), response.longOrderCreateTransaction != null && response.longOrderCreateTransaction.id > 0, "Position close order created.");
         m_Results.Verify("14." + (increment + 1).ToString(), response.longOrderFillTransaction != null && response.longOrderFillTransaction.id > 0, "Position close fill order created.");
         m_Results.Verify("14." + (increment + 2).ToString(), response.longOrderFillTransaction.units == -1 * units, "Position close units correct.");

      }

      private static short VerifyPosition(Position position, short increment)
      {
         string key = "14.";

         m_Results.Verify(key + increment.ToString(), position.@long != null, "Position has direction");
         m_Results.Verify(key + (increment + 1).ToString(), position.@long.units > 0, "Position has units");
         m_Results.Verify(key + (increment + 2).ToString(), position.@long.averagePrice > 0, "Position has avgPrice");
         m_Results.Verify(key + (increment + 3).ToString(), !string.IsNullOrEmpty(position.instrument), "Position has instrument");

         return increment += 4;
      }
      #endregion

      #region Transaction
      private static async Task Transaction_GetTransactionsByDateRange()
      {
         // 09
         Dictionary<string, string> parameters = new Dictionary<string, string>();
         parameters.Add("from", m_LastTransactionTime);
         parameters.Add("type", TransactionType.ClientConfigure);

         List<ITransaction> results = await Rest20.GetTransactionsByDateRangeAsync(AccountId, parameters);

         m_Results.Verify("09.0", results != null, string.Format("Transactions info received.", AccountId));
         m_Results.Verify("09.1", results.Where(x => x.type == TransactionType.ClientConfigure).Count() > 0, "Client configure transactions returned.");
         m_Results.Verify("09.2", results.Where(x => x.type != TransactionType.ClientConfigure).Count() > 0, "Non-client configure transactions returned.");
      }

      private static async Task Transaction_GetTransactionDetails()
      {
         //15
         ITransaction result = await Rest20.GetTransactionDetailsAsync(AccountId, m_LastTransactionID);

         m_Results.Verify("15.0", result != null, string.Format("Transaction info received."));
         m_Results.Verify("15.1", result.id > 0, "Transaction has id.");
         m_Results.Verify("15.2", result.type != null, "Transaction has type.");
         m_Results.Verify("15.3", result.time != null, "Transaction has time.");
      }

      private static async Task Transaction_GetTransactionsByIdRange()
      {
         // 16
         Dictionary<string, string> parameters = new Dictionary<string, string>();
         parameters.Add("from", m_FirstTransactionID.ToString());
         parameters.Add("to", m_LastTransactionID.ToString());

         List<ITransaction> results = await Rest20.GetTransactionsByIdRangeAsync(AccountId, parameters);
         results.OrderBy(x => x.id);

         m_Results.Verify("16.0", results != null, string.Format("Transactions info received.", AccountId));
         m_Results.Verify("16.1", results.First().id == m_FirstTransactionID, string.Format("Id of first transaction is correct.", results.First().id));
         m_Results.Verify("16.2", results.Where(x => x.id < m_FirstTransactionID).Count() == 0, "All Id's are greater than first transactionId.");
         m_Results.Verify("16.3", results.Where(x => x.type == TransactionType.ClientConfigure).Count() > 0, "Client configure transactions returned.");
         m_Results.Verify("16.4", results.Where(x => x.type == TransactionType.MarketOrder).Count() > 0, "Market order transactions returned.");
      }

      private static async Task Transaction_GetTransactionsSinceId()
      {
         // 10
         List<ITransaction> results = await Rest20.GetTransactionsSinceIdAsync(AccountId, m_LastTransactionID);
         results.OrderBy(x => x.id);

         m_Results.Verify("10.0", results != null, string.Format("Transactions info received.", AccountId));
         m_Results.Verify("10.1", results.First().id == m_LastTransactionID + 1, string.Format("Id of first transaction is correct.", results.First().id));
         m_Results.Verify("10.2", results.Where(x => x.id <= m_LastTransactionID).Count() == 0, "All Id's are greater than last transactionId.");
         m_Results.Verify("10.3", results.Where(x => x.type == TransactionType.ClientConfigure).Count() > 0, "Client configure transactions returned.");
      }
      #endregion

      #region Pricing
      private static async Task Pricing_GetPricing()
      {
         List<string> instruments = new List<string>();
         m_Instruments.ForEach(x => instruments.Add(x.name));

         List<Price> prices = await Rest20.GetPriceListAsync(AccountId, instruments);

         m_Results.Verify("06.0", prices != null, string.Format("Prices retrieved successfully."));
         m_Results.Verify("06.1", prices.Count == m_Instruments.Count, string.Format("Correct count ({0}) of prices retrieved.", prices.Count));
      }
      #endregion

      #region Stream
      static Semaphore _transactionReceived;
      protected static Task Stream_GetStreamingTransactions()
      {
         // 07
         TransactionsSession session = new TransactionsSession(AccountId);
         _transactionReceived = new Semaphore(0, 100);
         session.DataReceived += OnTransactionReceived;
         session.StartSession();

         return Task.Run(() =>
         {
            // wait 10sec or until an event is received
            bool success = _transactionReceived.WaitOne(10000);
            session.StopSession();
            m_Results.Verify("07.0", success, "Transaction events stream is functioning.");
         });
      }

      static bool _gotTransaction = false;
      protected static void OnTransactionReceived(TransactionStreamResponse data)
      {
         if (!_gotTransaction)
         {
            m_Results.Verify("07.1", data.transaction != null, "Transaction received");
            if (data.transaction != null)
            {
               m_Results.Verify("07.2", data.transaction.id != 0, "Transaction has id.");
               m_Results.Verify("07.3", data.transaction.accountID == AccountId, string.Format("Transaction has correct accountID: ({0}).", AccountId));
            }

            // only testing first data
            _gotTransaction = !data.IsHeartbeat();
         }

         _transactionReceived.Release();
      }

      static Semaphore _tickReceived;
      protected static void Stream_GetStreamingPrices()
      {
         PricingSession session = new PricingSession(AccountId, m_Instruments);
         _tickReceived = new Semaphore(0, 100);
         session.DataReceived += OnPricingReceived;
         session.StartSession();
         bool success = _tickReceived.WaitOne(10000);
         session.StopSession();
         m_Results.Verify("18.0", success, "Pricing stream is functioning.");
      }

      static bool _gotPrice = false;
      protected static void OnPricingReceived(PricingStreamResponse data)
      {
         if (!_gotPrice)
         {
            if (data.price != null)
            {
               m_Results.Verify("18.1", data.price != null, "Pricing data received.");
               m_Results.Verify("18.2", data.price.instrument != null, "Streaming price has instrument");

               if (data.price.tradeable)
               {
                  m_Results.Verify("18.3", data.price.bids.Count > 0, "Streaming price has bids");
                  m_Results.Verify("18.4", data.price.asks.Count > 0, "Streaming price has asks");
               }

               _gotPrice = true;
            }
         }
         _tickReceived.Release();
      }
      #endregion

      #region Utilities
      /// <summary>
      /// Convert DateTime object to a string of the indicated format
      /// </summary>
      /// <param name="time">A DateTime object</param>
      /// <param name="format">Format type (RFC3339 or UNIX only</param>
      /// <returns>A date-time string</returns>
      private static string ConvertDateTimeToAcceptDateFormat(DateTime time, AcceptDatetimeFormat format = AcceptDatetimeFormat.RFC3339)
      {
         // look into doing this within the JsonSerializer so that objects can use DateTime instead of string

         if (format == AcceptDatetimeFormat.RFC3339)
            return XmlConvert.ToString(time, "yyyy-MM-ddTHH:mm:ssZ");
         else if (format == AcceptDatetimeFormat.Unix)
            return ((int)(time.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
         else
            throw new ArgumentException(string.Format("The value ({0}) of the format parameter is invalid.", (short)format));
      }

      /// <summary>
      /// Reads the api key from a supplied file name
      /// </summary>
      /// <returns></returns>
      private static async Task SetPracticeApiCredentials(string fileName = null)
      {
         fileName = fileName ?? @"C:\Users\Osita\SourceCode\GitHub\OandaV20\oandaPracticeApiCredentials.txt";

         try
         {
            using (StreamReader sr = new StreamReader(fileName))
            {
               string apiCredentials = await sr.ReadToEndAsync();

               // an OANDA trade or practice account is required to generate a valid token
               // for info, go to: https://www.oanda.com/account/tpa/personal_token
               m_TestToken = apiCredentials.Split('|')[0];

               // this should be a v20 account, not a standard/legacy account
               // if null, this will be set to the first v20 account found using the token above
               m_TestAccount = apiCredentials.Split('|')[1];
            }
         }
         catch (Exception ex)
         {
            throw new Exception("Could not read api credentials file.", ex);
         }

         // set this to the correct environment based on the account
         m_TestEnvironment = EEnvironment.Practice;

         // set this to the correct number of v20 accounts associated with the token     
         m_TokenAccounts = 1;

         Credentials.SetCredentials(m_TestEnvironment, m_TestToken, m_TestAccount);
      }
      #endregion
   }
}
