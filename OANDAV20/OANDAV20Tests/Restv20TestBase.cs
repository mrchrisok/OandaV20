using System.Collections.Generic;

namespace OkonkwoOandaV20Tests
{
   public class Restv20TestBase
   {
      static protected readonly Restv20TestResults m_Results;
      protected short m_FailedTests = 0;

      public Restv20TestResults Results { get { return m_Results; } }
      public short Failures { get { return m_FailedTests; } }

      static Restv20TestBase()
      {
         m_Results = new Restv20TestResults();
      }
   }
}
