using Newtonsoft.Json;
using OkonkwoOandaV20.TradeLibrary.DataTypes.Stream;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.DataTypes.Communications
{
   public abstract class StreamSession<T> where T : IHeartbeat
   {
      protected readonly string _accountId;
      protected WebResponse _response;
      protected bool _shutdown;

      public delegate void DataHandler(T data);
      public event DataHandler DataReceived;
      public void OnDataReceived(T data)
      {
         DataHandler handler = DataReceived;
         if (handler != null) handler(data);
      }
      public delegate void SessionStatusHandler(string accountId, bool started, Exception e);
      public event SessionStatusHandler SessionStatusChanged;
      public void OnSessionStatusChanged(bool started, Exception e)
      {
         SessionStatusHandler handler = SessionStatusChanged;
         if (handler != null) handler(_accountId, started, e);
      }

      protected StreamSession(string accountId)
      {
         _accountId = accountId;
      }

      protected abstract Task<WebResponse> GetSession();

      public virtual async void StartSession()
      {
         _shutdown = false;
         _response = await GetSession();

         Task.Run(() =>
            {
               StreamReader reader = new StreamReader(_response.GetResponseStream());
               while (!_shutdown)
               {
                  try
                  {
                     string line = reader.ReadLine();
                     var data = JsonConvert.DeserializeObject<T>(line);

                     OnSessionStatusChanged(!_shutdown, null);

                     // Don't send heartbeats
                     if (!data.IsHeartbeat())
                     {
                        OnDataReceived(data);
                     }
                  }
                  catch (Exception e)
                  {
                     _shutdown = true;
                     throw e;
                  }
               }
            }
            );

      }

      public void StopSession()
      {
         _shutdown = true;
      }

      public bool Stopped()
      {
         return _shutdown;
      }
   }
}
