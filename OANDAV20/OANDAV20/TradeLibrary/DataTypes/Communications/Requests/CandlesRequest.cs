using OANDAV20.TradeLibrary.DataTypes.Communications.Instrument;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OANDAV20.TradeLibrary.DataTypes.Communications.Communications.Requests
{
   public class CandlesRequest : Request
   {
      [DefaultValue("M")]
      public string price { get; set; }

      [DefaultValue(CandleStickGranularity.Seconds05)]
      public string granularity { get; set; }

      [Range(0, 5000)]
      public int? count { get; set; }

      public string from { get; set; }
      public string to { get; set; }

      [DefaultValue(false)]
      public bool smooth { get; set; }

      [DefaultValue(false)]
      public bool includeFirst { get; set; }

      [Range(0, 23)]
      [DefaultValue(17)]
      public int dailyAlignment { get; set; }

      [DefaultValue("America/New_York")]
      public string alignmentTimezone { get; set; }

      [DefaultValue(WeeklyAlignment.Friday)]
      public string weeklyAlignment { get; set; }
   }
}
