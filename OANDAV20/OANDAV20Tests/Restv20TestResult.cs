using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OkonkwoOandaV20Tests
{
   public class Restv20TestResult
   {
      public bool Success { get; set; }
      public string Details { get; set; }
   }

   public class Restv20TestResults
   {
      #region Declarations
      string m_LastMessage;
      Dictionary<string, Restv20TestResult> m_Results = new Dictionary<string, Restv20TestResult>();
      Dictionary<string, string> m_MutableMessages = new Dictionary<string, string>();
      #endregion

      #region Public properties and methods
      public ReadOnlyDictionary<string, Restv20TestResult> Items
      {
         get { return new ReadOnlyDictionary<string, Restv20TestResult>(m_Results); }
      }

      public ReadOnlyDictionary<string, string> Messages
      {
         get { return new ReadOnlyDictionary<string, string>(m_MutableMessages); }
      }

      public string LastMessage
      {
         get { return m_LastMessage; }
      }

      //------
      public bool Verify(bool success, string testDescription)
      {
         return Verify(DateTime.UtcNow.ToString(), success, testDescription);
      }

      public bool Verify(string success, string testDescription)
      {
         return Verify(DateTime.UtcNow.ToString(), !string.IsNullOrEmpty(success), testDescription);
      }

      public bool Verify(string key, string success, string testDescription)
      {
         return Verify(key, !string.IsNullOrEmpty(success), testDescription);
      }

      public bool Verify(string key, bool success, string testDescription)
      {
         m_Results.Add(key, new Restv20TestResult { Success = success, Details = testDescription });
         if (!success)
         {
            Add(key + ": " + success + ": " + testDescription); // add message
         }
         return success;
      }

      //------
      public void Add(string key, Restv20TestResult testResult)
      {
         m_Results.Add(key, testResult);
      }

      public void Add(string message)
      {
         m_LastMessage = message;
         m_MutableMessages.Add(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + ':' + m_MutableMessages.Count, message);
      }
      #endregion
   }
}
