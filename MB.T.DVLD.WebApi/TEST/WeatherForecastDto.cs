using System;

namespace MB.T.DVLD.WebApi.TEST
{
    public class WeatherForecastDto // Data Transfer Object
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
