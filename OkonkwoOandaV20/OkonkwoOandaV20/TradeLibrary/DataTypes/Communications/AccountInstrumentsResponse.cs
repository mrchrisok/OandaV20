using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public class AccountInstrumentsResponse : Response
   {
      public List<Instrument.Instrument> instruments { get; set; }
   }
}
