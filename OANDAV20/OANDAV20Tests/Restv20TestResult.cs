using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MarketMiner.Api.Tests.OANDAv20
{
   public class Restv20TestResult
   {
      public bool Success { get; set; }
      public string Details { get; set; }
   }

   public class Restv20TestResults
   {
      #region Declarations
      string _lastMessage;
      Dictionary<string, Restv20TestResult> _results = new Dictionary<string, Restv20TestResult>();
      Dictionary<string, string> _mutableMessages = new Dictionary<string, string>();
      #endregion

      #region Public properties and methods
      public ReadOnlyDictionary<string, Restv20TestResult> Items
      {
         get { return new ReadOnlyDictionary<string, Restv20TestResult>(_results); }
      }

      public ReadOnlyDictionary<string, string> Messages
      {
         get { return new ReadOnlyDictionary<string, string>(_mutableMessages); }
      }

      public string LastMessage
      {
         get { return _lastMessage; }
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
         _results.Add(key, new Restv20TestResult { Success = success, Details = testDescription });
         if (!success)
         {
            Add(key + ": " + success + ": " + testDescription); // add message
         }
         return success;
      }

      //------
      public void Add(string key, Restv20TestResult testResult)
      {
         _results.Add(key, testResult);
      }

      public void Add(string message)
      {
         _lastMessage = message;
         _mutableMessages.Add(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + ':' + _mutableMessages.Count, message);
      }
      #endregion
   }
}
