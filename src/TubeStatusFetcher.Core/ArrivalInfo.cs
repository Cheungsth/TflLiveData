using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubeStatusFetcher.Core
{
    public class ArrivalInfo
    {        
        public string Id { get; set; }
        public string StationName { get; set; }
        public string LineName { get; set; }
        public string PlatformName { get; set; }
        public string Direction { get; set; }
        public string DestinationName { get; set; }
        public int TimeToStationInSeconds { get; set; }

        public int TimeToStationInMins
        {
            get { return (TimeToStationInSeconds / 60) ; }
        }

        public DateTime ExpectedArrival { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime TimeToLive { get; set; }
        public string ModeName { get; set; }

    }
}
