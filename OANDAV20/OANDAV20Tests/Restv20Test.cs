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
         string key = "01.0";
         var result = _results.Items.FirstOrDefault(x => x.Key == key).Value as Restv20TestResult;

         Assert.IsTrue(result.Success, result.Success.ToString() + ": " + result.Details);
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
         var instrument = _results.Items.FirstOrDefault(x => x.Key == "03.0").Value as Restv20TestResult;
         var type = _results.Items.FirstOrDefault(x => x.Key == "03.1").Value as Restv20TestResult;
         var name = _results.Items.FirstOrDefault(x => x.Key == "03.2").Value as Restv20TestResult;

         Assert.IsTrue(instrument.Success, instrument.Success.ToString() + ": " + instrument.Details);
         Assert.IsTrue(type.Success, type.Success.ToString() + ": " + type.Details);
         Assert.IsTrue(name.Success, name.Success.ToString() + ": " + name.Details);
      }

      [TestMethod]
      public void test_Account_retrieve_account_summary_info()
      {
         // 04
         var account = _results.Items.FirstOrDefault(x => x.Key == "04.0").Value as Restv20TestResult;
         var id = _results.Items.FirstOrDefault(x => x.Key == "04.1").Value as Restv20TestResult;
         var currency = _results.Items.FirstOrDefault(x => x.Key == "04.2").Value as Restv20TestResult;

         Assert.IsTrue(account.Success, account.Success.ToString() + ": " + account.Details);
         Assert.IsTrue(id.Success, id.Success.ToString() + ": " + id.Details);
         Assert.IsTrue(currency.Success, currency.Success.ToString() + ": " + currency.Details);
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
      #endregion

      #region Trade
      #endregion

      #region Position
      #endregion

      #region Transaction
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
