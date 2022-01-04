using System.Runtime.Serialization;

namespace ApiOne.Client.Contracts.Requests;

[DataContract]
public class SomeData
{
   [DataMember(Order = 1)] public int Number { get; set; }
   [DataMember(Order = 2)] public string FirstName { get; set; }
   [DataMember(Order = 3)] public string LastName { get; set; }
   [DataMember(Order = 4)] public int Score { get; set; }
   [DataMember(Order = 5)] public string Note { get; set; }
}