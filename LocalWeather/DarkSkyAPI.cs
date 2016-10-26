using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace DarkSkyAPI
{
    public class DarkSkyAPIModel
    {

        // Properties: Dark Sky API 
        private double latitude { get; set; }
        private double longitude { get; set; }
        private DarkSkyDataModel weatherModel { get; set; }
        private string address;


        // Methods: Dark Sky API

        // Constructor
        public DarkSkyAPIModel (double lat, double lng)
        {
            this.latitude = lat;
            this.longitude = lng;

            // Connects to Dark Sky API, retreives data, and parses data into weatherModel
            // TODO add check to see if these lat and lng have already been searched today.  If so, use the saved JSON file to continue.
            saveDarkSKYJSON();

            // Print Weather information
            printWeatherReport();
        }

        // Get Dark Sky Weather JSON and deserialize
        private void saveDarkSKYJSON()
        {
            //Generate URL with Key, Lat and Longitude  and save a JSON file to the project's asset folder.
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)"); //May not need this line; but good reference for future.

            //query address and save data to s
            Stream data = client.OpenRead(getURL());
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();

            // deserialize received data
            this.weatherModel = JsonConvert.DeserializeObject<DarkSkyDataModel>(s);
        }
        private string getURL()
        {
            //Returns the URL for Dark Sky API based on the submitted latitude and longitude
            //website for requesting  informatoin: https://api.darksky.net/forecast/d353c94884828ab143c8633437f899aa/37.8267,-122.4233
            string initialString = @"https://api.darksky.net/forecast/d353c94884828ab143c8633437f899aa/";
            return (initialString + latitude + ","+longitude);
        }

        // Print Weather Report
        public void printWeatherReport()
        {
            // Print Timezone (Continent/City
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine();
            Console.WriteLine("---------------------------");
            Console.WriteLine();
            Console.WriteLine($"Weather Info at {latitude}, {longitude}");
            Console.WriteLine();
            Console.WriteLine("---------------------------");
            Console.WriteLine();
            // Current conditions
            Console.WriteLine("Current conditions:");
            Console.WriteLine($"Time: {getDateTime(weatherModel.currently.time).ToShortDateString() + " " + getDateTime(weatherModel.currently.time).ToShortTimeString()}");
            Console.WriteLine($"Summary: {weatherModel.currently.summary}");
            Console.WriteLine($"Current Temperature: {weatherModel.currently.temperature.ToString("F0")}");
            Console.WriteLine();
            Console.WriteLine("---------------------------");
            Console.WriteLine();
            // Daily Forcast
            Console.WriteLine($"Daily forcast for the next eight days:");
            Console.WriteLine();
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("-----");
                Console.WriteLine();
                Console.WriteLine($"Time: {getDateTime(weatherModel.daily.data[i].time).ToShortDateString()}");
                Console.WriteLine($"Summary: {weatherModel.daily.data[i].summary}");
                Console.WriteLine($"Current Temperature: {weatherModel.daily.data[i].temperature.ToString("F0")}");
                Console.WriteLine($"Low for the day: {weatherModel.daily.data[i].temperatureMin.ToString ("F0")}");
                Console.WriteLine($"High for the day: {weatherModel.daily.data[i].temperatureMax.ToString("F0")}");
                Console.WriteLine($"Probability of Rain: {weatherModel.daily.data[i].precipProbability.ToString("P")}");
                Console.WriteLine($"Sunrise: {getDateTime(weatherModel.daily.data[i].sunriseTime ).ToShortTimeString()}");
                Console.WriteLine($"Sunrise: {getDateTime(weatherModel.daily.data[i].sunsetTime).ToShortTimeString()}");
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------");
            Console.WriteLine();
            Console.WriteLine("That concludes your eight day forcast.");
        }

        private static DateTime getDateTime(long secUnixEpoch)
        {
            // DateTime equivalent to the UNIX Epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(secUnixEpoch);
            return dateTime.ToLocalTime();
        }

        // TODO go through the DarkSkyAPI classes and remove unnecessary fields
        public class DarkSkyDataModel
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string timezone { get; set; }
            public int offset { get; set; }
            public Currently currently { get; set; }
            public Minutely minutely { get; set; }
            public Hourly hourly { get; set; }
            public Daily daily { get; set; }
            public Flags flags { get; set; }
        }

        public class Currently
        {
            public int time { get; set; }
            public string summary { get; set; }
            public string icon { get; set; }
            public double precipIntensity { get; set; }
            public double precipProbability { get; set; }
            public double temperature { get; set; }
            public double apparentTemperature { get; set; }
            public double dewPoint { get; set; }
            public double humidity { get; set; }
            public double windSpeed { get; set; }
            public double  windBearing { get; set; }
            public double visibility { get; set; }
            public double cloudCover { get; set; }
            public double pressure { get; set; }
            public double ozone { get; set; }
        }

        public class Datum
        {
            public int time { get; set; }
            public double precipIntensity { get; set; }
            public double precipProbability { get; set; }
        }

        public class Minutely
        {
            public string summary { get; set; }
            public string icon { get; set; }
            public List<Datum> data { get; set; }
        }

        public class Datum2 : Currently 
        {
            //public int time { get; set; }
            //public string summary { get; set; }
            //public string icon { get; set; }
            //public double precipIntensity { get; set; }
            //public double precipProbability { get; set; }
            //public double temperature { get; set; }
            //public double apparentTemperature { get; set; }
            //public double dewPoint { get; set; }
            //public double humidity { get; set; }
            //public double windSpeed { get; set; }
            //public int windBearing { get; set; }
            //public double visibility { get; set; }
            //public double cloudCover { get; set; }
            //public double pressure { get; set; }
            //public double ozone { get; set; }
            public string precipType { get; set; }
        }

        public class Hourly
        {
            public string summary { get; set; }
            public string icon { get; set; }
            public List<Datum2> data { get; set; }
        }

        public class Datum3 : Datum2
        {
            public int sunriseTime { get; set; }
            public int sunsetTime { get; set; }
            public double moonPhase { get; set; }
            public double precipIntensityMax { get; set; }
            public int precipIntensityMaxTime { get; set; }
            public double temperatureMin { get; set; }
            public int temperatureMinTime { get; set; }
            public double temperatureMax { get; set; }
            public int temperatureMaxTime { get; set; }
            public double apparentTemperatureMin { get; set; }
            public int apparentTemperatureMinTime { get; set; }
            public double apparentTemperatureMax { get; set; }
            public int apparentTemperatureMaxTime { get; set; }
            //public int time { get; set; }
            //public string summary { get; set; }
            //public string icon { get; set; }
            //public double precipIntensity { get; set; }
            //public double precipProbability { get; set; }
            //public string precipType { get; set; }
            //public double dewPoint { get; set; }
            //public double humidity { get; set; }
            //public double windSpeed { get; set; }
            //public int windBearing { get; set; }
            //public double visibility { get; set; }
            //public double cloudCover { get; set; }
            //public double pressure { get; set; }
            //public double ozone { get; set; }
        }

        public class Daily
        {
            public string summary { get; set; }
            public string icon { get; set; }
            public List<Datum3> data { get; set; }
        }

        public class Flags
        {
            public List<string> sources { get; set; }
            public string units { get; set; }
        }

        public class Alerts
        {
            public string description { get; set; }
            public int expires { get; set; }
            public string title { get; set; }
            public string uri { get; set; }
        }

    }
}