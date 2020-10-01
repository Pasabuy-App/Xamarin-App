using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Navigation;

namespace PasaBuy.App.Services
{
    public class ApiServices
    {
        private JsonSerializer _serializer = new JsonSerializer();

        private static ApiServices _ServiceClientInstance;

        public static ApiServices ServiceClientInstance
        {
            get
            {
                if (_ServiceClientInstance == null)
                    _ServiceClientInstance = new ApiServices();
                return _ServiceClientInstance;
            }
        }
        private HttpClient client;
        public ApiServices()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://maps.googleapis.com/maps/");
        }

        public async Task<GoogleDirection> GetDirections(string originLatitude, string originLongitude, string destinationLatitude1, string destinationLongitude1, string mode, string waypointlatitude, string waypointlongitude)
        {
            GoogleDirection googleDirection = new GoogleDirection();

            var response = await client.GetAsync($"api/directions/json?mode={mode}& transit_routing_preference=less_driving&origin={originLatitude},{originLongitude}&destination={destinationLatitude1},{destinationLongitude1}&waypoints={waypointlatitude},{waypointlongitude}&key={PSAConfig.GeoMatrixGoogleApiKey}").ConfigureAwait(false);
            //var response = await client.GetAsync($"api/directions/json?origin=14.3317599,121.0445461&destination=14.3317599,121.0445461&waypoints=14.3317599,121.0445461&key=AIzaSyDeR29pTg1D5Exga3rQd8a3XKL3XtukQQg").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    googleDirection = await Task.Run(() =>
                    JsonConvert.DeserializeObject<GoogleDirection>(json)
                    ).ConfigureAwait(false);

                }

            }

            Console.WriteLine("rest   haha  " + googleDirection);
            return googleDirection;
        }
    }
}
