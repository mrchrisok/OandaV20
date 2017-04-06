using Microsoft.VisualStudio.TestTools.UnitTesting;
using OANDAV20;
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
      #endregion

      #region Instrument
      //[TestMethod]
      //public void test_Instrument_retrieve_instrument_details()
      //{
      //   string key = "01";
      //   var results = _results.Items.Where(x => x.Key.StartsWith(key) && x.Key != "01.0");
      //   var failure = results.FirstOrDefault(x => x.Value.Success = false);

      //   Assert.IsTrue(failure.Key == null, failure.Key + ": " + failure.Value);
      //}
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
      #endregion

      #region Position
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
   }
}
