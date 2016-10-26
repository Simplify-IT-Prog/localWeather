using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace ClassGoogleGeocoding
{
    public class GoogleGeocodingAPI
    {
        //Properties: Google Geocoding 
        public double geoLatitude { get; private set; } = 0.0d;
        public double geoLongtitude { get; private set; } = 0.0d;
        private googleGeocoding uniqueAddressModel { get; set; }
        private string address;

        //Methods: Google Geocoding API
        public GoogleGeocodingAPI ()
        {
            //Get desired address from user
            Console.WriteLine("Enter a zip code or City and State for relevant weather information:");
            string typedAddress = Console.ReadLine();

            if (typedAddress != "" )
            {
                this.address = typedAddress;

                //Determine if search term was already performed.  If not performed (no previously saved JSON results), then perform search.
                if (!File.Exists("Geocode_" + address + ".json")) { saveGeocodingJSON(); }  //Query Google Geocoding for information and save JSON file

                //open saved file and seralize object
                StreamReader sr = File.OpenText("Geocode_" + address + ".json");
                String gGeocodeData = sr.ReadToEnd();
                googleGeocoding uniqueAddressModel = JsonConvert.DeserializeObject<googleGeocoding>(gGeocodeData);

                // Set Latitude and Longtitude
                this.geoLatitude = uniqueAddressModel.results[0].geometry.location.lat;
                this.geoLongtitude = uniqueAddressModel.results[0].geometry.location.lng;
                //setGeoLat();
                //setGeoLng();
            }

        }
        private void saveGeocodingJSON()
        {
            //Receive address from user and save a JSON file to the project's asset folder.
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)"); //May not need this line; but good reference for future.

            //query address and save data to s
            Stream data = client.OpenRead(getGeocodingURL());
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();

            //save json file to asset folder
            string path = "Geocode_" + address + ".json";
            if (!File.Exists(path))
            {
                // Create a json file and write web output to file.
                using (StreamWriter sw = File.CreateText(path)) { sw.WriteLine(s); }
            }
        }
        private string getGeocodingURL()
        {
            string initialString;
            int addressToNum = -1;
            //Returns the URL for geocoding based on the submitted address
            //website for requesting geocoding informatoin: https://maps.googleapis.com/maps/api/geocode/json?address=Toledo&region=es&key=
            if (int.TryParse(address, out addressToNum))
            {
                initialString = String.Format(@"https://maps.googleapis.com/maps/api/geocode/json?components=postal_code:" + addressToNum + "&key=AIzaSyBRBZtk0JyJrAzmp2i9DklBHhjvKwoI0JE");
            } else {
                initialString = String.Format(@"https://maps.googleapis.com/maps/api/geocode/json?address=" + address.Replace(" ", "").ToLower());
            }
            
            //string lastString = @"&key=";  //Apparently you do not need a key;
            return (initialString);
        }
        private void setGeoLat() { this.geoLatitude = uniqueAddressModel.results[0].geometry.location.lat; }
        private void setGeoLng() { this.geoLongtitude = uniqueAddressModel.results[0].geometry.location.lng; }
        private class googleGeocoding
        {
            public List<Result> results { get; set; }
            public string status { get; set; }
        }
        private class Location
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }
        private class Geometry
        {
            public Location location { get; set; }
        }
        private class Result
        {
            public Geometry geometry { get; set; }
        }
    } //End class GoogleGeocoding
} //End namespace
