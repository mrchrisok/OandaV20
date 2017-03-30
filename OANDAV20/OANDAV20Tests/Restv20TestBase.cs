using System.Collections.Generic;

namespace OANDAv20Tests
{
   public class Restv20TestBase
   {
      static protected readonly Restv20TestResults _results;
      protected short _failedTests = 0;

      public Restv20TestResults Results { get { return _results; } }
      public short Failures { get { return _failedTests; } }

      static Restv20TestBase()
      {
         _results = new Restv20TestResults();
      }
   }
}
