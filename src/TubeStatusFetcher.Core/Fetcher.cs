using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubeStatusFetcher.Core
{
    public class Fetcher
    {
        private readonly string _apiEndPoint = "https://api.tfl.gov.uk/line/mode/tube/status?detail=true";

        private readonly string _jubileeStopPoints = "https://api.tfl.gov.uk/line/jubilee/stoppoints";
        private readonly string _dlrStopPoints = "https://api.tfl.gov.uk/line/dlr/stoppoints";

        // Obtained from running GetTubeStopArrivalInfo(), and intepret returned JSON in http://jsonviewer.stack.hu/ 
        private readonly string _blackwall_StopPoint_Id = "940GZZDLBLA";
        private readonly string _canarywharf_StopPoint_Id = "940GZZLUCYF";

        // Mode as parameter
        // https://api.tfl.gov.uk/StopPoint/940GZZLUCYF/Arrivals?mode=tube 
        //
        private readonly string _blackwall_Arrival = "https://api.tfl.gov.uk/StopPoint/940GZZDLBLA/arrivals";
        private readonly string _canarywharf_Arrival = "https://api.tfl.gov.uk/StopPoint/940GZZLUCYF/arrivals";


        public List<LineInfo> GetTubeInfo()
        {
            var client = new RestClient(_apiEndPoint);
            var request = new RestRequest("/", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            var response = (RestResponse)client.Execute(request);
            var content = response.Content;
            var tflResponse = JsonConvert.DeserializeObject<List<TflLineInfo>>(content);

            var lineInfoList = tflResponse.Select(t =>
                new LineInfo()
                {
                    Id = t.id,
                    Name = t.name,
                    Reason = t.lineStatuses[0].reason,
                    StatusSeverityDescription = t.lineStatuses[0].statusSeverityDescription,
                    StatusSeverity = t.lineStatuses[0].statusSeverity
                }).ToList();

            return lineInfoList;
        }

        public List<ArrivalInfo> GetArrivalInfo()
        {
            var client = new RestClient(_canarywharf_Arrival);
            var request = new RestRequest("/", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            var response = (RestResponse)client.Execute(request);
            var content = response.Content;
            var tflResponse = JsonConvert.DeserializeObject<List<TflArrivalInfo>>(content);

            var arrivalInfoList = tflResponse.Select(t =>
                new ArrivalInfo()
                {
                    Id = t.id,
                    StationName = t.stationName,
                    LineName = t.lineName,
                    PlatformName = t.platformName,
                    Direction = t.direction,
                    DestinationName = t.destinationName,
                    TimeToStationInSeconds = t.timeToStation,
                    ExpectedArrival = t.expectedArrival,
                    TimeStamp = t.timestamp,
                    TimeToLive = t.timeToLive,
                    ModeName = t.modeName
                }).ToList();

            return arrivalInfoList;

        }

        public void GetTubeStopArrivalInfo()
        {
            var client = new RestClient(_jubileeStopPoints);
            var request = new RestRequest("/", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            var response = (RestResponse)client.Execute(request);
            var content = response.Content;
            
            //var tflResponse = JsonConvert.DeserializeObject<List<TflLineInfo>>(content);

            //var lineInfoList = tflResponse.Select(t =>
            //    new LineInfo()
            //    {
            //        Id = t.id,
            //        Name = t.name,
            //        Reason = t.lineStatuses[0].reason,
            //        StatusSeverityDescription = t.lineStatuses[0].statusSeverityDescription,
            //        StatusSeverity = t.lineStatuses[0].statusSeverity
            //    }).ToList();

            //return lineInfoList;
        }

    }
}
