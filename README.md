<H1> Local Weather </H1>
<H4> 
Console application that provides the current weather and the eight day forecast for the desired location.<br>
Requires an active internet connection to funciton.
</H4>

<H1> How to use: </H1>
<H4>
1) Enter a zipcode or City, State combination.<br>
2) Weather information is displayed for the specified region.<br>
3) After the forecast is reviewd, click enter to terminate the application.<br>
</H4>

<H1>API's utilized:</H1>
<H4>
Google's geocoding API.<br>  See https://developers.google.com/maps/documentation/geocoding/start for additional information.<br>
The DarkSky Weather API.<br>  See https://darksky.net/dev/ for additional information.
</H4>
    
<H1>Functional Overview:</H1>
<H4>To get the local weather, the Darksky API requires a latitude and longtitude be passed in the GET request.  Most users don't specify a location using latitude and longtitude.  They are familiar with zipcodes and city, state combinations.  So, the user is asked to enter a location.  Google's geocoding API is queried with the specific location information.  The API returns a JSON file that is deserialized and the data is saved to a model representing this information.  The latitude and longtitude are retrieved from the model and passed to a Darksky class.  This class then queries the Darksky API using a GET request.  A JSON file is received and deserialized into a Darksky model.  The current weather information and the eight day forecast are displayed in the console.</H4><br><br><br>
    


