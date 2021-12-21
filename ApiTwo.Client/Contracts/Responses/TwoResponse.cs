using System.Runtime.Serialization;

namespace ApiTwo.Client.Contracts.Responses
{
    [DataContract]
    public class TwoResponse
    {
        [DataMember(Order = 1)] public Guid Id { get; set; } 
        [DataMember(Order = 2)] public string Description { get; set; }
        [DataMember(Order = 3)] public DateTime Date { get; set; } = DateTime.UtcNow;
        [DataMember(Order = 4)] public decimal TemperatureC { get; set; }
        [DataMember(Order = 5)] public string? Summary { get; set; }
    }
}
