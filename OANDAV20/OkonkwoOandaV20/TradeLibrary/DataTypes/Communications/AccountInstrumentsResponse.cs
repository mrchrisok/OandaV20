using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class AccountInstrumentsResponse
   {
      public List<Instrument.Instrument> instruments;
      public long lastTransactionID;
   }
}
