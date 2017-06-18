using OkonkwoOandaV20.TradeLibrary.DataTypes.Stream;
using System.Net;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class TransactionsSession : StreamSession<TransactionStreamResponse>
   {
      public TransactionsSession(string accountId) : base(accountId)
      {
      }

      protected override async Task<WebResponse> GetSession()
      {
         return await Rest20.StartTransactionsSession(_accountId);
      }
   }
}
