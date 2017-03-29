using OANDAV20.REST20.TradeLibrary.DataTypes.Stream;
using System.Net;
using System.Threading.Tasks;

namespace OANDAV20.REST20.TradeLibrary
{
   public class TransactionSession : StreamSession<TransactionStreamResponse>
   {
      public TransactionSession(string accountId) : base(accountId)
      {
      }

      protected override async Task<WebResponse> GetSession()
      {
         return await Rest20.StartTransactionsSession(_accountId);
      }
   }
}
