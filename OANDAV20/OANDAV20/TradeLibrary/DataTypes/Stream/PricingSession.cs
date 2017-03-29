using OANDAV20.REST20.TradeLibrary.DataTypes.Instrument;
using OANDAV20.REST20.TradeLibrary.DataTypes.Stream;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OANDAV20.REST20.TradeLibrary
{
   public class PricingSession : StreamSession<PricingStreamResponse>
   {
      private readonly List<Instrument> _instruments;
      private bool _snapshot;

      public PricingSession(string accountId, List<Instrument> instruments, bool snapshot = true) : base(accountId)
      {
         _instruments = instruments;
         _snapshot = snapshot;
      }

      protected override async Task<WebResponse> GetSession()
      {
         return await Rest20.StartPricingSession(_accountId, _instruments, _snapshot);
      }
   }
}
