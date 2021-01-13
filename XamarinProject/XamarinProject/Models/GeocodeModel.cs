using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XamarinProject.Models
{
    public class GeocodeModel
    {

        public Root data { get; set; }


        public async static Task<GeocodeModel> GetGeoCode(string address)
        {
            using (HttpClient client = new HttpClient())
            {

                var uri = new Uri("https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyBPqtjELzjbjQ0hcB0Nyff5184EOD42GAI");


                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);

                string ans = await response.Content.ReadAsStringAsync();

                Root responseObject = JsonConvert.DeserializeObject<Root>(ans);

                return new GeocodeModel { data = responseObject };
            }
        }


        public GeocodeModel()
        {
        }
    }

    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class PlusCode
    {
        public string compound_code { get; set; }
        public string global_code { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public PlusCode plus_code { get; set; }
        public List<string> types { get; set; }
    }

    public class Root
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }

}

