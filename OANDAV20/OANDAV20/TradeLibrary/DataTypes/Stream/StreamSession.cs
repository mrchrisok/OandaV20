using OANDAV20.TradeLibrary.DataTypes.Communications.Stream;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace OANDAV20.TradeLibrary.DataTypes.Communications
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
      public delegate void SessionStatusHandler(bool started, Exception e);
      public event SessionStatusHandler SessionStatusChanged;
      public void OnSessionStatusChanged(bool started, Exception e)
      {
         SessionStatusHandler handler = SessionStatusChanged;
         if (handler != null) handler(started, e);
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
               DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
               StreamReader reader = new StreamReader(_response.GetResponseStream());
               while (!_shutdown)
               {
                  try
                  {
                     MemoryStream memStream = new MemoryStream();

                     string line = reader.ReadLine();
                     memStream.Write(Encoding.UTF8.GetBytes(line), 0, Encoding.UTF8.GetByteCount(line));
                     memStream.Position = 0;

                     var data = (T)serializer.ReadObject(memStream);

                     OnSessionStatusChanged(true, null);

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
