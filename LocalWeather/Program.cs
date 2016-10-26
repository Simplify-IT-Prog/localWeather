using System;
using ClassGoogleGeocoding;
using DarkSkyAPI;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create an instance of the Google Geocodes API: contacts google, retrieves JSON file, and parses lat and long.
            GoogleGeocodingAPI geocodeAPI = new GoogleGeocodingAPI();

            // Create DarkSky data model which contains all weather info for the submitted geolocation.
            DarkSkyAPIModel weatherForecast = new DarkSkyAPIModel(geocodeAPI.geoLatitude, geocodeAPI.geoLongtitude);

            Console.ReadLine();
        }
        

    }
}



