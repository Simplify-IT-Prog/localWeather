using System;
using ClassGoogleGeocoding;
using DarkSkyAPI;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create an instance of the Google Geocodes API
            // S
            GoogleGeocodingAPI geocodeAPI = new GoogleGeocodingAPI();
            DarkSkyAPIModel weatherForecast = new DarkSkyAPIModel(geocodeAPI.geoLatitude, geocodeAPI.geoLongtitude);

            Console.ReadLine();
        }
        

    }
}



