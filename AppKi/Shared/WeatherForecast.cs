using System.Runtime.Serialization;

namespace AppKi.Shared
{
    [DataContract]
    public class WeatherForecast
    {
        [DataMember(Order = 1)] public DateTime Date { get; set; }
        [DataMember(Order = 2)] public string Description { get; set; }
        [DataMember(Order = 3)] public int TemperatureC { get; set; }
        [DataMember(Order = 4)] public string? Summary { get; set; }
    }
}