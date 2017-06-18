using System;

namespace OkonkwoOandaV20.Framework
{
   public class MarketHaltedException : Exception
   {
      public MarketHaltedException(string message) : base(message) { }
   }
}
