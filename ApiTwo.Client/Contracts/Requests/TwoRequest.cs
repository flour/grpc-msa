using System.Runtime.Serialization;

namespace ApiTwo.Client.Contracts.Requests
{
    [DataContract]
    public class TwoRequest
    {
        [DataMember(Order = 1)] public string? Query { get; set; }
    }
}
