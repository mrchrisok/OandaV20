using OkonkwoOandaV20;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.Factories;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Stream;
using OkonkwoOandaV20.TradeLibrary.Primitives;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OkonkwoOandaV20App
{
   class Program
   {
      static void Main(string[] args)
      {
         WriteNewLine("Hello trader! Welcome to OkonkwoOandaV20");

         SetApiCredentials();

         StartTransactionsStream();

         PutOnATrade().Wait();

         StopTransactionsStream();
           
         Console.ReadKey();
      }

      static string AccountID { get; set; }
      const string INSTRUMENT = InstrumentName.Currency.EURUSD;

      static void SetApiCredentials()
      {
         WriteNewLine("Setting your V20 credentials ...");

         AccountID = "101-001-1913854-001";
         var environment = EEnvironment.Practice;
         var token = "c9e9494d79013cac1d34f0e4dcb590cd-977a37b80762fb48cdb3b0b2e832628a";

         Credentials.SetCredentials(environment, token, AccountID);

         WriteNewLine("Nice! Credentials are set.");
      }

      #region trading
      static private async Task PutOnATrade()
      {
         WriteNewLine("Checking to see if EUR_USD is open for trading ...");

         // first, check the market status for EUR_USD
         // if it is tradeable, we'll try to make some money :)
         if (!(await Utilities.IsMarketHalted(INSTRUMENT)))
         {
            WriteNewLine("EUR_USD is open and rockin', so let's start trading!");

            long? tradeID = await PlaceMarketOrder();

            if (tradeID.HasValue)
            {
               // we have an open trade.
               // give it some time to make money :)
               await Task.Delay(10000);

               WriteNewLine("Okay, we've waited 10 seconds. Closing trade now ...");

               // now, let' close the trade and collect our profits! .. hopefully
               TradeCloseResponse closeResponse = null;
               try
               {
                  closeResponse = await Rest20.CloseTradeAsync(AccountID, tradeID.Value, "ALL");
               }
               catch
               {
                  WriteNewLine("Oops. The trade can't be closed. Something went wrong. :(");
               }

               if (closeResponse != null)
               {
                  WriteNewLine("Nice! The trade is closed.");

                  var profit = closeResponse.orderFillTransaction.pl;
                  WriteNewLine($"Our profit was USD {profit}");

                  if (profit > 0)
                     WriteNewLine($"Nice work! You are an awesome trader.");
                  else
                  {
                     WriteNewLine($"Looks like you need to learn some money-making strategies. :(");
                     WriteNewLine($"Keep studying, learning, but most of all .. keep trading!!");
                  }
               }
            }
            else
            {
               WriteNewLine($"Looks like something went awry with the trade. you need to learn some money-making strategies. :(");
            }
         }
         else
         {
            WriteNewLine("Sorry, Oanda markets are closed or Euro market is not tradeable.");
            WriteNewLine("Try again another time.");
         }
      }

      static async Task<long?> PlaceMarketOrder(string side = "buy")
      {
         WriteNewLine("Creating a EUR_USD market BUY order ...");

         var oandaInstrument = (await Rest20.GetAccountInstrumentsAsync(AccountID, INSTRUMENT)).First();
         long orderUnits = side == "buy" ? 10 : -10;

         var request = new MarketOrderRequest(oandaInstrument)
         {
            units = orderUnits
         };

         OrderPostResponse response = null;
         try
         {
            response = await Rest20.PostOrderAsync(AccountID, request);
            WriteNewLine("Congrats! You've put on a trade! Let it run! :)");
         }
         catch (Exception ex)
         {
            var errorResponse = ErrorResponseFactory.Create(ex.Message);

            WriteNewLine("Oops. Order creation failed.");
            WriteNewLine($"The failure message is: {errorResponse.errorMessage}.");
            WriteNewLine("Try again later.");
         }

         return response?.orderFillTransaction?.tradeOpened?.tradeID;
      }
      #endregion

      #region transactions stream
      static Semaphore _transactionReceived;
      static TransactionsSession _transactionsSession;

      static void StartTransactionsStream()
      {
         WriteNewLine("Starting transactions stream ...");

         _transactionsSession = new TransactionsSession(AccountID);
         _transactionReceived = new Semaphore(0, 100);
         _transactionsSession.DataReceived += OnTransactionReceived;

         _transactionsSession.StartSession();

         bool success = _transactionReceived.WaitOne(10000);

         if (success)
            WriteNewLine("Good news!. Transactions stream is functioning.");
         else
            WriteNewLine("Bad news!. Transactions stream is not functioning.");
      }

      protected static void OnTransactionReceived(TransactionStreamResponse data)
      {
         if (!data.IsHeartbeat())
            WriteNewLine("V20 notification - New account transaction: " + data.transaction.type);

         _transactionReceived.Release();
      }

      static void StopTransactionsStream()
      {
         _transactionsSession.StopSession();
      }
      #endregion

      static void WriteNewLine(string message)
      {
         Console.WriteLine($"\n{message}");
      }
   }
}
