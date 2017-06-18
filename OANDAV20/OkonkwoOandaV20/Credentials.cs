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
            {EEnvironment.Practice, new Dictionary<EServer, string>
               {
                  {EServer.Account, "https://api-fxpractice.oanda.com/v3/"},
                  {EServer.Labs, "https://api-fxpractice.oanda.com/labs/v3/"},
                  {EServer.StreamingPrices, "https://stream-fxpractice.oanda.com/v3/"},
                  {EServer.StreamingTransactions, "https://stream-fxpractice.oanda.com/v3/"},
               }
            },
            {EEnvironment.Trade, new Dictionary<EServer, string>
               {
                  {EServer.Account, "https://api-fxtrade.oanda.com/v3/"},
                  {EServer.Labs, "https://api-fxtrade.oanda.com/labs/v3/"},
                  {EServer.StreamingPrices, "https://stream-fxtrade.oanda.com/v3/"},
                  {EServer.StreamingTransactions, "https://stream-fxtrade.oanda.com/v3/"}
               }
            }
         };


      private static Credentials m_Instance;

      public string AccessToken { get; set; }
      public string DefaultAccountId { get; set; }
      public EEnvironment Environment { get; set; }
      public string Username { get; set; }

      public static Credentials GetDefaultCredentials()
      {
         if (m_Instance == null)
         {
            //_instance = GetPracticeCredentials();
            //_instance = GetSandboxCredentials();
         }
         return m_Instance;
      }

      private static Credentials GetPracticeCredentials()
      {
         return new Credentials()
         {
            DefaultAccountId = "621396",
            Environment = EEnvironment.Practice,
            AccessToken = "73eba38ad5b44778f9a0c0fec1a66ed1-44f47f052c897b3e1e7f24196bbc071f"
         };
      }

      private static Credentials GetLiveCredentials()
      {
         // You'll need to add your own accessToken and account if desired
         return new Credentials()
         {
            DefaultAccountId = "001-001-432582-001",
            AccessToken = "5a0478f89da0cac4ee02ed60ff9329a6-0450b6274d7bbbc7fac532029be78d66",
            Environment = EEnvironment.Trade
         };
      }

      public static void SetCredentials(EEnvironment environment, string accessToken, string defaultAccount = "0")
      {
         m_Instance = new Credentials
         {
            Environment = environment,
            AccessToken = accessToken,
            DefaultAccountId = defaultAccount
         };
      }
   }
}
