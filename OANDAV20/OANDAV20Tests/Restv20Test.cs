using Microsoft.VisualStudio.TestTools.UnitTesting;
using OANDAV20;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OANDAv20Tests
{
   [TestClass]
   public partial class Restv20Test : Restv20TestBase
   {
      [TestInitialize]
      public void CheckIfAllApiOperationsHaveCompleted()
      {
         while (!_apiOperationsComplete) Task.Delay(250).Wait();
      }

      #region Credentials
      [TestMethod]
      public void test_Credentials_get_credentials()
      {
         Assert.IsTrue(Credentials.GetDefaultCredentials().Environment == _testEnvironment, "Credentials Environment is incorrect.");
         Assert.IsTrue(Credentials.GetDefaultCredentials().AccessToken == _testToken, "Credentials Token is incorrect.");
         Assert.IsTrue(Credentials.GetDefaultCredentials().DefaultAccountId == _testAccount, "Credentials AccountId is incorrect.");
      }
      #endregion

      #region Account
      [TestMethod]
      public void test_Account_retrieve_accounts_list()
      {
         var accountsRetrieved = _results.Items.FirstOrDefault(x => x.Key == "01.0").Value as Restv20TestResult;

         Assert.IsTrue(accountsRetrieved.Success, accountsRetrieved.Success.ToString() + ": " + accountsRetrieved.Details);
      }

      [TestMethod]
      public void test_Account_retrieve_correct_number_of_accounts()
      {
         string key = "01.1";
         var result = _results.Items.FirstOrDefault(x => x.Key == key).Value as Restv20TestResult;

         Assert.IsTrue(result.Success, result.Success.ToString() + ": " + result.Details);
      }

      [TestMethod]
      public void test_Account_account_numbers_have_correct_format()
      {
         string key = "01.";
         var results = _results.Items.Where(x => x.Key.StartsWith(key) && x.Key != "01.0" && x.Key != "01.1");
         var failure = results.FirstOrDefault(x => x.Value.Success == false);

         string message = failure.Key != null ? failure.Value.Success.ToString() + ": " + failure.Value.Details : "";

         Assert.IsTrue(failure.Key == null, failure.Key + ": " + message);
      }

      [TestMethod]
      public void test_Account_retrieve_account_detail_info()
      {
         // 08
         var accountRetrieved = _results.Items.FirstOrDefault(x => x.Key == "08.0").Value as Restv20TestResult;
         var idIsCorrect = _results.Items.FirstOrDefault(x => x.Key == "08.1").Value as Restv20TestResult;
         var correctCurrency = _results.Items.FirstOrDefault(x => x.Key == "08.2").Value as Restv20TestResult;
         var correctOpenTradeCount = _results.Items.FirstOrDefault(x => x.Key == "08.3").Value as Restv20TestResult;
         var tradesMatchTradeCount = _results.Items.FirstOrDefault(x => x.Key == "08.4").Value as Restv20TestResult;

         Assert.IsTrue(accountRetrieved.Success, accountRetrieved.Success.ToString() + ": " + accountRetrieved.Details);
         Assert.IsTrue(idIsCorrect.Success, idIsCorrect.Success.ToString() + ": " + idIsCorrect.Details);
         Assert.IsTrue(correctCurrency.Success, correctCurrency.Success.ToString() + ": " + correctCurrency.Details);
         Assert.IsTrue(correctOpenTradeCount.Success, correctOpenTradeCount.Success.ToString() + ": " + correctOpenTradeCount.Details);
         Assert.IsTrue(tradesMatchTradeCount.Success, tradesMatchTradeCount.Success.ToString() + ": " + tradesMatchTradeCount.Details);
      }

      [TestMethod]
      public void test_Account_retrieve_instruments_list()
      {
         string key = "02.0";
         var results = _results.Items.Where(x => x.Key == key);
         var failure = results.FirstOrDefault(x => x.Value.Success == false);

         Assert.IsTrue(failure.Key == null, failure.Key + ": " + failure.Value);
      }

      [TestMethod]
      public void test_Account_retrieve_single_instrument_info()
      {
         // 03
         var instrumentReceived = _results.Items.FirstOrDefault(x => x.Key == "03.0").Value as Restv20TestResult;
         var instrumentTypeCorrect = _results.Items.FirstOrDefault(x => x.Key == "03.1").Value as Restv20TestResult;
         var instrumentNameCorrect = _results.Items.FirstOrDefault(x => x.Key == "03.2").Value as Restv20TestResult;

         Assert.IsTrue(instrumentReceived.Success, instrumentReceived.Success.ToString() + ": " + instrumentReceived.Details);
         Assert.IsTrue(instrumentTypeCorrect.Success, instrumentTypeCorrect.Success.ToString() + ": " + instrumentTypeCorrect.Details);
         Assert.IsTrue(instrumentNameCorrect.Success, instrumentNameCorrect.Success.ToString() + ": " + instrumentNameCorrect.Details);
      }

      [TestMethod]
      public void test_Account_retrieve_account_summary_info()
      {
         // 04
         var summaryRetrieved = _results.Items.FirstOrDefault(x => x.Key == "04.0").Value as Restv20TestResult;
         var accountIdIsCorrect = _results.Items.FirstOrDefault(x => x.Key == "04.1").Value as Restv20TestResult;
         var currencyIsCorrect = _results.Items.FirstOrDefault(x => x.Key == "04.2").Value as Restv20TestResult;

         Assert.IsTrue(summaryRetrieved.Success, summaryRetrieved.Success.ToString() + ": " + summaryRetrieved.Details);
         Assert.IsTrue(accountIdIsCorrect.Success, accountIdIsCorrect.Success.ToString() + ": " + accountIdIsCorrect.Details);
         Assert.IsTrue(currencyIsCorrect.Success, currencyIsCorrect.Success.ToString() + ": " + currencyIsCorrect.Details);
      }

      [TestMethod]
      public void test_Account_patch_account_configuration()
      {
         // 05
         var configRetrieved = _results.Items.FirstOrDefault(x => x.Key == "05.0").Value as Restv20TestResult;
         var aliasChanged = _results.Items.FirstOrDefault(x => x.Key == "05.1").Value as Restv20TestResult;
         var marginRateChanged = _results.Items.FirstOrDefault(x => x.Key == "05.2").Value as Restv20TestResult;
         var aliasReverted = _results.Items.FirstOrDefault(x => x.Key == "05.3").Value as Restv20TestResult;
         var marginRateReverted = _results.Items.FirstOrDefault(x => x.Key == "05.4").Value as Restv20TestResult;

         Assert.IsTrue(configRetrieved.Success, configRetrieved.Success.ToString() + ": " + configRetrieved.Details);
         Assert.IsTrue(aliasChanged.Success, aliasChanged.Success.ToString() + ": " + aliasChanged.Details);
         Assert.IsTrue(marginRateChanged.Success, marginRateChanged.Success.ToString() + ": " + marginRateChanged.Details);
         Assert.IsTrue(aliasReverted.Success, aliasReverted.Success.ToString() + ": " + aliasReverted.Details);
         Assert.IsTrue(marginRateReverted.Success, marginRateReverted.Success.ToString() + ": " + marginRateReverted.Details);
      }

      [TestMethod]
      public void test_Account_retrieve_account_changes()
      {
         var changesRetrieved = _results.Items.FirstOrDefault(x => x.Key == "17.0").Value as Restv20TestResult;
         var ordersFilledReceived = _results.Items.FirstOrDefault(x => x.Key == "17.1").Value as Restv20TestResult;
         var ordersCancelledReceived = _results.Items.FirstOrDefault(x => x.Key == "17.2").Value as Restv20TestResult;
         var tradesClosedReceived = _results.Items.FirstOrDefault(x => x.Key == "17.3").Value as Restv20TestResult;
         var positionsReceived = _results.Items.FirstOrDefault(x => x.Key == "17.4").Value as Restv20TestResult;
         var accountHasMargin = _results.Items.FirstOrDefault(x => x.Key == "17.5").Value as Restv20TestResult;

         Assert.IsTrue(changesRetrieved.Success, changesRetrieved.Success.ToString() + ": " + changesRetrieved.Details);
         Assert.IsTrue(ordersFilledReceived.Success, ordersFilledReceived.Success.ToString() + ": " + ordersFilledReceived.Details);
         Assert.IsTrue(ordersCancelledReceived.Success, ordersCancelledReceived.Success.ToString() + ": " + ordersCancelledReceived.Details);
         Assert.IsTrue(tradesClosedReceived.Success, tradesClosedReceived.Success.ToString() + ": " + tradesClosedReceived.Details);
         Assert.IsTrue(positionsReceived.Success, positionsReceived.Success.ToString() + ": " + positionsReceived.Details);
         Assert.IsTrue(accountHasMargin.Success, accountHasMargin.Success.ToString() + ": " + accountHasMargin.Details);
      }
      #endregion

      #region Instrument
      [TestMethod]
      public void test_Instrument_retrieve_candlestick_list()
      {
         var candlesRetrieved = _results.Items.FirstOrDefault(x => x.Key == "12.0").Value as Restv20TestResult;
         var candlesCountCorrect = _results.Items.FirstOrDefault(x => x.Key == "12.1").Value as Restv20TestResult;

         Assert.IsTrue(candlesRetrieved.Success, candlesRetrieved.Success.ToString() + ": " + candlesRetrieved.Details);
         Assert.IsTrue(candlesCountCorrect.Success, candlesCountCorrect.Success.ToString() + ": " + candlesCountCorrect.Details);
      }

      [TestMethod]
      public void test_Instrument_retrieve_candlestick_details()
      {
         var candlesInstrumentCorrect = _results.Items.FirstOrDefault(x => x.Key == "12.2").Value as Restv20TestResult;
         var candlesGranularityCorrect = _results.Items.FirstOrDefault(x => x.Key == "12.3").Value as Restv20TestResult;
         var candlesHaveMidPrice = _results.Items.FirstOrDefault(x => x.Key == "12.4").Value as Restv20TestResult;
         var candlesHaveBidPrice = _results.Items.FirstOrDefault(x => x.Key == "12.5").Value as Restv20TestResult;
         var candlesHaveAskPrice = _results.Items.FirstOrDefault(x => x.Key == "12.6").Value as Restv20TestResult;

         Assert.IsTrue(candlesInstrumentCorrect.Success, candlesInstrumentCorrect.Success.ToString() + ": " + candlesInstrumentCorrect.Details);
         Assert.IsTrue(candlesGranularityCorrect.Success, candlesGranularityCorrect.Success.ToString() + ": " + candlesGranularityCorrect.Details);
         Assert.IsTrue(candlesHaveMidPrice.Success, candlesHaveMidPrice.Success.ToString() + ": " + candlesHaveMidPrice.Details);
         Assert.IsTrue(candlesHaveBidPrice.Success, candlesHaveBidPrice.Success.ToString() + ": " + candlesHaveBidPrice.Details);
         Assert.IsTrue(candlesHaveAskPrice.Success, candlesHaveAskPrice.Success.ToString() + ": " + candlesHaveAskPrice.Details);
      }
      #endregion

      #region Order
      [TestMethod]
      public void test_Order_create_order()
      {
         var orderCreated = _results.Items.FirstOrDefault(x => x.Key == "11.0").Value as Restv20TestResult;
         var orderTypeIsCorrect = _results.Items.FirstOrDefault(x => x.Key == "11.1").Value as Restv20TestResult;

         Assert.IsTrue(orderCreated.Success, orderCreated.Success.ToString() + ": " + orderCreated.Details);
         Assert.IsTrue(orderTypeIsCorrect.Success, orderTypeIsCorrect.Success.ToString() + ": " + orderTypeIsCorrect.Details);
      }

      [TestMethod]
      public void test_Order_get_all_orders()
      {
         var allOrdersRetrieved = _results.Items.FirstOrDefault(x => x.Key == "11.2").Value as Restv20TestResult;
         var testOrderTypeReturned = _results.Items.FirstOrDefault(x => x.Key == "11.3").Value as Restv20TestResult;

         Assert.IsTrue(allOrdersRetrieved.Success, allOrdersRetrieved.Success.ToString() + ": " + allOrdersRetrieved.Details);
         Assert.IsTrue(testOrderTypeReturned.Success, testOrderTypeReturned.Success.ToString() + ": " + testOrderTypeReturned.Details);
      }

      [TestMethod]
      public void test_Order_get_pending_orders()
      {
         var pendingOrdersRetrieved = _results.Items.FirstOrDefault(x => x.Key == "11.4").Value as Restv20TestResult;
         var onlyPendingOrderRetrieved = _results.Items.FirstOrDefault(x => x.Key == "11.5").Value as Restv20TestResult;
         var testPendingOrderRetrieved = _results.Items.FirstOrDefault(x => x.Key == "11.6").Value as Restv20TestResult;

         Assert.IsTrue(pendingOrdersRetrieved.Success, pendingOrdersRetrieved.Success.ToString() + ": " + pendingOrdersRetrieved.Details);
         Assert.IsTrue(onlyPendingOrderRetrieved.Success, onlyPendingOrderRetrieved.Success.ToString() + ": " + onlyPendingOrderRetrieved.Details);
         Assert.IsTrue(testPendingOrderRetrieved.Success, testPendingOrderRetrieved.Success.ToString() + ": " + testPendingOrderRetrieved.Details);
      }

      [TestMethod]
      public void test_Order_get_order_details()
      {
         var orderDetailsRetrieved = _results.Items.FirstOrDefault(x => x.Key == "11.7").Value as Restv20TestResult;
         var clientExtensionsRetrieved = _results.Items.FirstOrDefault(x => x.Key == "11.8").Value as Restv20TestResult;
         var tradeExtensionsRetrieved = _results.Items.FirstOrDefault(x => x.Key == "11.9").Value as Restv20TestResult;

         Assert.IsTrue(orderDetailsRetrieved.Success, orderDetailsRetrieved.Success.ToString() + ": " + orderDetailsRetrieved.Details);
         Assert.IsTrue(clientExtensionsRetrieved.Success, clientExtensionsRetrieved.Success.ToString() + ": " + clientExtensionsRetrieved.Details);
         Assert.IsTrue(tradeExtensionsRetrieved.Success, tradeExtensionsRetrieved.Success.ToString() + ": " + tradeExtensionsRetrieved.Details);
      }

      [TestMethod]
      public void test_Order_update_order_extensions()
      {
         var extensionsUpdated = _results.Items.FirstOrDefault(x => x.Key == "11.10").Value as Restv20TestResult;
         var extensionsOrderCorrect = _results.Items.FirstOrDefault(x => x.Key == "11.11").Value as Restv20TestResult;
         var clientExtensionCommentCorrect = _results.Items.FirstOrDefault(x => x.Key == "11.12").Value as Restv20TestResult;
         var tradeExtensionCommentCorrect = _results.Items.FirstOrDefault(x => x.Key == "11.13").Value as Restv20TestResult;

         Assert.IsTrue(extensionsUpdated.Success, extensionsUpdated.Success.ToString() + ": " + extensionsUpdated.Details);
         Assert.IsTrue(extensionsOrderCorrect.Success, extensionsOrderCorrect.Success.ToString() + ": " + extensionsOrderCorrect.Details);
         Assert.IsTrue(clientExtensionCommentCorrect.Success, clientExtensionCommentCorrect.Success.ToString() + ": " + clientExtensionCommentCorrect.Details);
         Assert.IsTrue(tradeExtensionCommentCorrect.Success, tradeExtensionCommentCorrect.Success.ToString() + ": " + tradeExtensionCommentCorrect.Details);
      }

      [TestMethod]
      public void test_Order_cancel_replace_order()
      {
         var orderWasCancelled = _results.Items.FirstOrDefault(x => x.Key == "11.14").Value as Restv20TestResult;
         var orderWasReplaced = _results.Items.FirstOrDefault(x => x.Key == "11.15").Value as Restv20TestResult;
         var newOrderDetailsCorrect = _results.Items.FirstOrDefault(x => x.Key == "11.16").Value as Restv20TestResult;

         Assert.IsTrue(orderWasCancelled.Success, orderWasCancelled.Success.ToString() + ": " + orderWasCancelled.Details);
         Assert.IsTrue(orderWasReplaced.Success, orderWasReplaced.Success.ToString() + ": " + orderWasReplaced.Details);
         Assert.IsTrue(newOrderDetailsCorrect.Success, newOrderDetailsCorrect.Success.ToString() + ": " + newOrderDetailsCorrect.Details);
      }

      [TestMethod]
      public void test_Order_cancel_order()
      {
         var cancelledOrderReturned = _results.Items.FirstOrDefault(x => x.Key == "11.17").Value as Restv20TestResult;
         var orderWasCancelled = _results.Items.FirstOrDefault(x => x.Key == "11.18").Value as Restv20TestResult;

         Assert.IsTrue(cancelledOrderReturned.Success, cancelledOrderReturned.Success.ToString() + ": " + cancelledOrderReturned.Details);
         Assert.IsTrue(orderWasCancelled.Success, orderWasCancelled.Success.ToString() + ": " + orderWasCancelled.Details);
      }
      #endregion

      #region Trade
      [TestMethod]
      public void test_Trade_place_market_order()
      {
         var marketOrderCreated = _results.Items.FirstOrDefault(x => x.Key == "13.0").Value as Restv20TestResult;
         var marketOrderFilled = _results.Items.FirstOrDefault(x => x.Key == "13.1").Value as Restv20TestResult;

         Assert.IsTrue(marketOrderCreated.Success, marketOrderCreated.Success.ToString() + ": " + marketOrderCreated.Details);
         Assert.IsTrue(marketOrderFilled.Success, marketOrderFilled.Success.ToString() + ": " + marketOrderFilled.Details);
      }

      [TestMethod]
      public void test_Trade_get_trades_list()
      {
         var allTradesRetrieved = _results.Items.FirstOrDefault(x => x.Key == "13.2").Value as Restv20TestResult;
         var openTradesRetrieved = _results.Items.FirstOrDefault(x => x.Key == "13.3").Value as Restv20TestResult;

         Assert.IsTrue(allTradesRetrieved.Success, allTradesRetrieved.Success.ToString() + ": " + allTradesRetrieved.Details);
         Assert.IsTrue(openTradesRetrieved.Success, openTradesRetrieved.Success.ToString() + ": " + openTradesRetrieved.Details);
      }

      [TestMethod]
      public void test_Trade_get_trade_details()
      {
         var detailsRetrieved = _results.Items.FirstOrDefault(x => x.Key == "13.4").Value as Restv20TestResult;

         Assert.IsTrue(detailsRetrieved.Success, detailsRetrieved.Success.ToString() + ": " + detailsRetrieved.Details);
      }

      [TestMethod]
      public void test_Trade_update_trade_extensions()
      {
         var extensionsUpdated = _results.Items.FirstOrDefault(x => x.Key == "13.5").Value as Restv20TestResult;
         var extensionsTradeCorrect = _results.Items.FirstOrDefault(x => x.Key == "13.6").Value as Restv20TestResult;
         var tradeExtensionCommentCorrect = _results.Items.FirstOrDefault(x => x.Key == "13.7").Value as Restv20TestResult;

         Assert.IsTrue(extensionsUpdated.Success, extensionsUpdated.Success.ToString() + ": " + extensionsUpdated.Details);
         Assert.IsTrue(extensionsTradeCorrect.Success, extensionsTradeCorrect.Success.ToString() + ": " + extensionsTradeCorrect.Details);
         Assert.IsTrue(tradeExtensionCommentCorrect.Success, tradeExtensionCommentCorrect.Success.ToString() + ": " + tradeExtensionCommentCorrect.Details);
      }

      [TestMethod]
      public void test_Trade_patch_exit_orders()
      {
         var takeProfitPatched = _results.Items.FirstOrDefault(x => x.Key == "13.8").Value as Restv20TestResult;
         var takeProfitPriceCorrect = _results.Items.FirstOrDefault(x => x.Key == "13.9").Value as Restv20TestResult;
         var stopLossPatched = _results.Items.FirstOrDefault(x => x.Key == "13.10").Value as Restv20TestResult;
         var stopLossPriceCorrect = _results.Items.FirstOrDefault(x => x.Key == "13.11").Value as Restv20TestResult;
         var trailingStopLossPatched = _results.Items.FirstOrDefault(x => x.Key == "13.12").Value as Restv20TestResult;
         var trailingStopLossDistanceCorrect = _results.Items.FirstOrDefault(x => x.Key == "13.13").Value as Restv20TestResult;

         var takeProfitCancelled = _results.Items.FirstOrDefault(x => x.Key == "13.14").Value as Restv20TestResult;
         var stopLossCancelled = _results.Items.FirstOrDefault(x => x.Key == "13.15").Value as Restv20TestResult;
         var trailingStopLossCancelled = _results.Items.FirstOrDefault(x => x.Key == "13.16").Value as Restv20TestResult;

         Assert.IsTrue(takeProfitPatched.Success, takeProfitPatched.Success.ToString() + ": " + takeProfitPatched.Details);
         Assert.IsTrue(takeProfitPriceCorrect.Success, takeProfitPriceCorrect.Success.ToString() + ": " + takeProfitPriceCorrect.Details);
         Assert.IsTrue(stopLossPatched.Success, stopLossPatched.Success.ToString() + ": " + stopLossPatched.Details);
         Assert.IsTrue(stopLossPriceCorrect.Success, stopLossPriceCorrect.Success.ToString() + ": " + stopLossPriceCorrect.Details);
         Assert.IsTrue(trailingStopLossPatched.Success, trailingStopLossPatched.Success.ToString() + ": " + trailingStopLossPatched.Details);
         Assert.IsTrue(trailingStopLossDistanceCorrect.Success, trailingStopLossDistanceCorrect.Success.ToString() + ": " + trailingStopLossDistanceCorrect.Details);
         Assert.IsTrue(takeProfitCancelled.Success, takeProfitCancelled.Success.ToString() + ": " + takeProfitCancelled.Details);
         Assert.IsTrue(stopLossCancelled.Success, stopLossCancelled.Success.ToString() + ": " + stopLossCancelled.Details);
         Assert.IsTrue(trailingStopLossCancelled.Success, trailingStopLossCancelled.Success.ToString() + ": " + trailingStopLossCancelled.Details);
      }

      [TestMethod]
      public void test_Trade_close_open_trade()
      {
         var tradeClosed = _results.Items.FirstOrDefault(x => x.Key == "13.17").Value as Restv20TestResult;
         var tradeCloseHasTime = _results.Items.FirstOrDefault(x => x.Key == "13.18").Value as Restv20TestResult;
         var tradeCloseHasInstrument = _results.Items.FirstOrDefault(x => x.Key == "13.19").Value as Restv20TestResult;
         var tradeCloseUnitsCorrect = _results.Items.FirstOrDefault(x => x.Key == "13.20").Value as Restv20TestResult;
         var tradeCloseHasPrice = _results.Items.FirstOrDefault(x => x.Key == "13.21").Value as Restv20TestResult;

         Assert.IsTrue(tradeClosed.Success, tradeClosed.Success.ToString() + ": " + tradeClosed.Details);
         Assert.IsTrue(tradeCloseHasTime.Success, tradeCloseHasTime.Success.ToString() + ": " + tradeCloseHasTime.Details);
         Assert.IsTrue(tradeCloseHasInstrument.Success, tradeCloseHasInstrument.Success.ToString() + ": " + tradeCloseHasInstrument.Details);
         Assert.IsTrue(tradeCloseUnitsCorrect.Success, tradeCloseUnitsCorrect.Success.ToString() + ": " + tradeCloseUnitsCorrect.Details);
         Assert.IsTrue(tradeCloseHasPrice.Success, tradeCloseHasPrice.Success.ToString() + ": " + tradeCloseHasPrice.Details);
      }
      #endregion

      #region Position
      [TestMethod]
      public void test_Position_get_positions()
      {
         // 14.0 & 14.1 do not need to be tested.

         var allPositionsRetrieved = _results.Items.FirstOrDefault(x => x.Key == "14.2").Value as Restv20TestResult;
         var openPositionsRetrieved = _results.Items.FirstOrDefault(x => x.Key == "14.3").Value as Restv20TestResult;

         Assert.IsTrue(allPositionsRetrieved.Success, allPositionsRetrieved.Success.ToString() + ": " + allPositionsRetrieved.Details);
         Assert.IsTrue(openPositionsRetrieved.Success, openPositionsRetrieved.Success.ToString() + ": " + openPositionsRetrieved.Details);
      }

      [TestMethod]
      public void test_Position_get_open_positions()
      {
         var postionHasDirection = _results.Items.FirstOrDefault(x => x.Key == "14.4").Value as Restv20TestResult;
         var positionHasUnits = _results.Items.FirstOrDefault(x => x.Key == "14.5").Value as Restv20TestResult;
         var positionHasAvgPrice = _results.Items.FirstOrDefault(x => x.Key == "14.6").Value as Restv20TestResult;
         var positionHasInstrument = _results.Items.FirstOrDefault(x => x.Key == "14.7").Value as Restv20TestResult;

         Assert.IsTrue(postionHasDirection.Success, postionHasDirection.Success.ToString() + ": " + postionHasDirection.Details);
         Assert.IsTrue(positionHasUnits.Success, positionHasUnits.Success.ToString() + ": " + positionHasUnits.Details);
         Assert.IsTrue(positionHasAvgPrice.Success, positionHasAvgPrice.Success.ToString() + ": " + positionHasAvgPrice.Details);
         Assert.IsTrue(positionHasInstrument.Success, positionHasInstrument.Success.ToString() + ": " + positionHasInstrument.Details);
      }

      [TestMethod]
      public void test_Position_get_position_details()
      {
         var postionHasDirection = _results.Items.FirstOrDefault(x => x.Key == "14.8").Value as Restv20TestResult;
         var positionHasUnits = _results.Items.FirstOrDefault(x => x.Key == "14.9").Value as Restv20TestResult;
         var positionHasAvgPrice = _results.Items.FirstOrDefault(x => x.Key == "14.10").Value as Restv20TestResult;
         var positionHasInstrument = _results.Items.FirstOrDefault(x => x.Key == "14.11").Value as Restv20TestResult;

         Assert.IsTrue(postionHasDirection.Success, postionHasDirection.Success.ToString() + ": " + postionHasDirection.Details);
         Assert.IsTrue(positionHasUnits.Success, positionHasUnits.Success.ToString() + ": " + positionHasUnits.Details);
         Assert.IsTrue(positionHasAvgPrice.Success, positionHasAvgPrice.Success.ToString() + ": " + positionHasAvgPrice.Details);
         Assert.IsTrue(positionHasInstrument.Success, positionHasInstrument.Success.ToString() + ": " + positionHasInstrument.Details);
      }

      [TestMethod]
      public void test_Position_close_position()
      {
         var closeOrderCreated = _results.Items.FirstOrDefault(x => x.Key == "14.12").Value as Restv20TestResult;
         var closeOrderFilled = _results.Items.FirstOrDefault(x => x.Key == "14.13").Value as Restv20TestResult;
         var closeUnitsCorrect = _results.Items.FirstOrDefault(x => x.Key == "14.14").Value as Restv20TestResult;

         Assert.IsTrue(closeOrderCreated.Success, closeOrderCreated.Success.ToString() + ": " + closeOrderCreated.Details);
         Assert.IsTrue(closeOrderFilled.Success, closeOrderFilled.Success.ToString() + ": " + closeOrderFilled.Details);
         Assert.IsTrue(closeUnitsCorrect.Success, closeUnitsCorrect.Success.ToString() + ": " + closeUnitsCorrect.Details);
      }
      #endregion

      #region Transaction
      [TestMethod]
      public void test_Transaction_get_transactions_by_date_range()
      {
         var transactionsReceived = _results.Items.FirstOrDefault(x => x.Key == "09.0").Value as Restv20TestResult;
         var clientConfigureReceived = _results.Items.FirstOrDefault(x => x.Key == "09.1").Value as Restv20TestResult;
         var notClientConfigureReceived = _results.Items.FirstOrDefault(x => x.Key == "09.2").Value as Restv20TestResult;

         Assert.IsTrue(transactionsReceived.Success, transactionsReceived.Success.ToString() + ": " + transactionsReceived.Details);
         Assert.IsTrue(clientConfigureReceived.Success, clientConfigureReceived.Success.ToString() + ": " + clientConfigureReceived.Details);
         Assert.IsTrue(transactionsReceived.Success, transactionsReceived.Success.ToString() + ": " + transactionsReceived.Details);
         Assert.IsFalse(notClientConfigureReceived.Success, notClientConfigureReceived.Success.ToString() + ": " + notClientConfigureReceived.Details);
      }

      [TestMethod]
      public void test_Transaction_get_transactions_since_id()
      {
         var transactionsReceived = _results.Items.FirstOrDefault(x => x.Key == "10.0").Value as Restv20TestResult;
         var firstIdIsNextId = _results.Items.FirstOrDefault(x => x.Key == "10.1").Value as Restv20TestResult;
         var allIdsGreaterThanLastId = _results.Items.FirstOrDefault(x => x.Key == "10.2").Value as Restv20TestResult;
         var clientConfigureReceived = _results.Items.FirstOrDefault(x => x.Key == "10.3").Value as Restv20TestResult;

         Assert.IsTrue(transactionsReceived.Success, transactionsReceived.Success.ToString() + ": " + transactionsReceived.Details);
         Assert.IsTrue(firstIdIsNextId.Success, firstIdIsNextId.Success.ToString() + ": " + firstIdIsNextId.Details);
         Assert.IsTrue(allIdsGreaterThanLastId.Success, allIdsGreaterThanLastId.Success.ToString() + ": " + allIdsGreaterThanLastId.Details);
         Assert.IsTrue(clientConfigureReceived.Success, clientConfigureReceived.Success.ToString() + ": " + clientConfigureReceived.Details);
      }

      [TestMethod]
      public void test_Transaction_get_transaction_detail()
      {
         var transactionReceived = _results.Items.FirstOrDefault(x => x.Key == "15.0").Value as Restv20TestResult;
         var transactionHasId = _results.Items.FirstOrDefault(x => x.Key == "15.1").Value as Restv20TestResult;
         var transactionHasType = _results.Items.FirstOrDefault(x => x.Key == "15.2").Value as Restv20TestResult;
         var transactionHasTime = _results.Items.FirstOrDefault(x => x.Key == "15.3").Value as Restv20TestResult;

         Assert.IsTrue(transactionReceived.Success, transactionReceived.Success.ToString() + ": " + transactionReceived.Details);
         Assert.IsTrue(transactionHasId.Success, transactionHasId.Success.ToString() + ": " + transactionHasId.Details);
         Assert.IsTrue(transactionHasType.Success, transactionHasType.Success.ToString() + ": " + transactionHasType.Details);
         Assert.IsTrue(transactionHasTime.Success, transactionHasTime.Success.ToString() + ": " + transactionHasTime.Details);
      }

      [TestMethod]
      public void test_Transaction_get_transactions_by_id_range()
      {
         var transactionsReceived = _results.Items.FirstOrDefault(x => x.Key == "16.0").Value as Restv20TestResult;
         var firstIdIsCorrect = _results.Items.FirstOrDefault(x => x.Key == "16.1").Value as Restv20TestResult;
         var allIdsGreaterThanFirst = _results.Items.FirstOrDefault(x => x.Key == "16.2").Value as Restv20TestResult;
         var clientConfiguredReturned = _results.Items.FirstOrDefault(x => x.Key == "16.3").Value as Restv20TestResult;
         var marketOrdersReturned = _results.Items.FirstOrDefault(x => x.Key == "16.4").Value as Restv20TestResult;

         Assert.IsTrue(transactionsReceived.Success, transactionsReceived.Success.ToString() + ": " + transactionsReceived.Details);
         Assert.IsTrue(firstIdIsCorrect.Success, firstIdIsCorrect.Success.ToString() + ": " + firstIdIsCorrect.Details);
         Assert.IsTrue(allIdsGreaterThanFirst.Success, allIdsGreaterThanFirst.Success.ToString() + ": " + allIdsGreaterThanFirst.Details);
         Assert.IsTrue(clientConfiguredReturned.Success, clientConfiguredReturned.Success.ToString() + ": " + clientConfiguredReturned.Details);
         Assert.IsTrue(marketOrdersReturned.Success, marketOrdersReturned.Success.ToString() + ": " + marketOrdersReturned.Details);
      }
      #endregion

      #region Pricing
      [TestMethod]
      public void test_Pricing_get_prices_list()
      {
         var pricesReceived = _results.Items.FirstOrDefault(x => x.Key == "06.0").Value as Restv20TestResult;
         var priceCountMatches = _results.Items.FirstOrDefault(x => x.Key == "06.1").Value as Restv20TestResult;

         Assert.IsTrue(pricesReceived.Success, pricesReceived.Success.ToString() + ": " + pricesReceived.Details);
         Assert.IsTrue(priceCountMatches.Success, priceCountMatches.Success.ToString() + ": " + priceCountMatches.Details);
      }
      #endregion

      #region Stream
      [TestMethod]
      public void test_Stream_transaction_stream_functional()
      {
         // 7
         var streamFunctional = _results.Items.FirstOrDefault(x => x.Key == "07.0").Value as Restv20TestResult;
         var dataReceived = _results.Items.FirstOrDefault(x => x.Key == "07.1").Value as Restv20TestResult;
         var dataHasId = _results.Items.FirstOrDefault(x => x.Key == "07.2").Value as Restv20TestResult;
         var dataHasAccountId = _results.Items.FirstOrDefault(x => x.Key == "07.3").Value as Restv20TestResult;

         Assert.IsTrue(streamFunctional.Success, streamFunctional.Success.ToString() + ": " + streamFunctional.Details);
         Assert.IsTrue(dataReceived.Success, dataReceived.Success.ToString() + ": " + dataReceived.Details);
         Assert.IsTrue(dataHasId.Success, dataHasId.Success.ToString() + ": " + dataHasId.Details);
         Assert.IsTrue(dataHasAccountId.Success, dataHasAccountId.Success.ToString() + ": " + dataHasAccountId.Details);
      }

      [TestMethod]
      public void test_Stream_pricing_stream_functional()
      {
         // 18
         var streamFunctional = _results.Items.FirstOrDefault(x => x.Key == "18.0").Value as Restv20TestResult;
         var dataReceived = _results.Items.FirstOrDefault(x => x.Key == "18.1").Value as Restv20TestResult;
         var dataHasInstrument = _results.Items.FirstOrDefault(x => x.Key == "18.2").Value as Restv20TestResult;

         Assert.IsTrue(streamFunctional.Success, streamFunctional.Success.ToString() + ": " + streamFunctional.Details);
         Assert.IsTrue(dataReceived.Success, dataReceived.Success.ToString() + ": " + dataReceived.Details);
         Assert.IsTrue(dataHasInstrument.Success, dataHasInstrument.Success.ToString() + ": " + dataHasInstrument.Details);

         // trap these
         // will throw if the instrument is not tradeable
         // the keys will not be present in _results
         try
         {
            var dataHasBids = _results.Items.FirstOrDefault(x => x.Key == "18.3").Value as Restv20TestResult;
            var dataHasAsks = _results.Items.FirstOrDefault(x => x.Key == "18.4").Value as Restv20TestResult;

            Assert.IsTrue(dataHasBids.Success, dataHasBids.Success.ToString() + ": " + dataHasBids.Details);
            Assert.IsTrue(dataHasAsks.Success, dataHasAsks.Success.ToString() + ": " + dataHasAsks.Details);
         }
         catch
         {

         }
      }
      #endregion
   }
}
