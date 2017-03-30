using OANDAV20.TradeLibrary.DataTypes.Communications.Stream;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OANDAV20.TradeLibrary.DataTypes.Communications
{
   public class PricingSession : StreamSession<PricingStreamResponse>
   {
      private readonly List<Instrument.Instrument> _instruments;
      private bool _snapshot;

      public PricingSession(string accountId, List<Instrument.Instrument> instruments, bool snapshot = true) : base(accountId)
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
