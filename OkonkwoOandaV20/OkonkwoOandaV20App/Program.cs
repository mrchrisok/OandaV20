using OkonkwoOandaV20;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Communications.Requests.Order;
using OkonkwoOandaV20.TradeLibrary.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OkonkwoOandaV20App
{
   class Program
   {
      const string INSTRUMENT = InstrumentName.Currency.EURUSD;
      static string _accountID = null;

      static void Main(string[] args)
      {
         PutOnATrade().Wait();

         Console.ReadKey();
      }

      static private async Task PutOnATrade()
      {
         Console.WriteLine("Hello trader! Welcome to OkonkwoOandaV20");

         // to start trading, first set your api credentials
         var environment = EEnvironment.Practice;
         var token = "c9e9494d79013cac1d34f0e4dcb590cd-977a37b80762fb48cdb3b0b2e832628a";
         var accountID = "101-001-1913854-002";
         Credentials.SetCredentials(environment, token, accountID);

         Console.WriteLine("Nice! Credentials are set. ...");

         // now, check the market status for EUR_USD
         // if it is tradeable, we'll try to make some money :)
         if (!(await Utilities.IsMarketHalted(INSTRUMENT)))
         {
            Console.WriteLine("Euro market is open and rockin', so let's start trading! ...");

            long? tradeID = await PlaceMarketOrder();

            if (tradeID.HasValue)
            {
               // we have an open trade.
               // give it some time to make money :)
               await Task.Delay(10000);


               // now, let' close the trade and collect our profits! .. hopefully
               TradeCloseResponse closeResponse = null;
               try
               {
                  closeResponse = await Rest20.CloseTradeAsync(_accountID, tradeID.Value, "ALL");
               }
               catch
               {
                  Console.WriteLine("Oops. The trade can't be closed. Something went wrong. :(");
               }

               if (closeResponse != null)
               {
                  Console.WriteLine("Nice! The trade is closed.");

                  var profit = closeResponse.orderFillTransaction.pl;
                  Console.WriteLine($"Our profit was USD {profit}");

                  if(profit > 0)
                     Console.WriteLine($"Nice work! You are an awesome trader.");
                  else
                  {
                     Console.WriteLine($"Looks like you need to learn some money-making strategies. :(");
                     Console.WriteLine($"Keep studying, learning, but most of all .. keep trading!!");
                  }                
               }
            }
         }
         else
         {
            Console.WriteLine("Sorry, Oanda markets are closed or Euro market is not tradeable.");
            Console.WriteLine("Try again another time.");
         }

      }

      static async Task<long?> PlaceMarketOrder(string side = "buy")
      {
         _accountID = Credentials.GetDefaultCredentials().DefaultAccountId;
         var oandaInstrument = (await Rest20.GetAccountInstrumentsAsync(_accountID, INSTRUMENT)).First();
         long orderUnits = side == "buy" ? 1 : -1;

         var request = new MarketOrderRequest(oandaInstrument)
         {
            units = orderUnits
         };

         OrderPostResponse response = null;
         try
         {
            response = await Rest20.PostOrderAsync(_accountID, request);
            Console.WriteLine("Congrats! You've put on a trade! Let it run! :)");
         }
         catch
         {
            Console.WriteLine("Oops. Order creation failed.");
         }

         return response?.orderFillTransaction?.tradeOpened?.tradeID;
      }
   }
}
