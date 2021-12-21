using System.Runtime.Serialization;

namespace ApiOne.Client.Contracts.Requests
{
    [DataContract]
    public class OneRequest
    {
        [DataMember(Order = 1)]
        public string Query { get; set; }
    }
}
