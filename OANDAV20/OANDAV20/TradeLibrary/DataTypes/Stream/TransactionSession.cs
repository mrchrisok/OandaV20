using OANDAV20.TradeLibrary.DataTypes.Communications.Stream;
using System.Net;
using System.Threading.Tasks;

namespace OANDAV20.TradeLibrary.DataTypes.Communications
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
