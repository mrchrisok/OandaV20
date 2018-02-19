using System.Collections.Generic;

namespace OkonkwoOandaV20
{
   public enum EServer
   {
      Account,
      Labs,
      StreamingPrices,
      StreamingTransactions
   }

   public enum EEnvironment
   {
      Practice,
      Trade
   }

   public class Credentials
   {
      public bool HasServer(EServer server)
      {
         return Servers[Environment].ContainsKey(server);
      }

      public string GetServer(EServer server)
      {
         if (HasServer(server))
         {
            return Servers[Environment][server];
         }
         return null;
      }

      private static readonly Dictionary<EEnvironment, Dictionary<EServer, string>> Servers = new Dictionary<EEnvironment, Dictionary<EServer, string>>
      {
         {  EEnvironment.Practice, new Dictionary<EServer, string>
            {
               {EServer.Account, "https://api-fxpractice.oanda.com/v3/"},
               {EServer.Labs, "https://api-fxpractice.oanda.com/labs/v3/"},
               {EServer.StreamingPrices, "https://stream-fxpractice.oanda.com/v3/"},
               {EServer.StreamingTransactions, "https://stream-fxpractice.oanda.com/v3/"},
            }
         },
         {  EEnvironment.Trade, new Dictionary<EServer, string>
            {
               {EServer.Account, "https://api-fxtrade.oanda.com/v3/"},
               {EServer.Labs, "https://api-fxtrade.oanda.com/labs/v3/"},
               {EServer.StreamingPrices, "https://stream-fxtrade.oanda.com/v3/"},
               {EServer.StreamingTransactions, "https://stream-fxtrade.oanda.com/v3/"}
            }
         }
      };

      private static Credentials m_DefaultCredentials;

      public string AccessToken { get; set; }
      public string DefaultAccountId { get; set; }
      public EEnvironment Environment { get; set; }

      public static Credentials GetDefaultCredentials()
      {
         return m_DefaultCredentials;
      }

      public static void SetCredentials(EEnvironment environment, string accessToken, string defaultAccount = "0")
      {
         m_DefaultCredentials = new Credentials
         {
            Environment = environment,
            AccessToken = accessToken,
            DefaultAccountId = defaultAccount
         };
      }
   }
}
