using System.Runtime.Serialization;

namespace ApiOne.Client.Contracts.Responses;

[DataContract]
public class Result
{
    [DataMember(Order = 1)] public bool Success { get; set; }
    [DataMember(Order = 2)] public string Message { get; set; }
}