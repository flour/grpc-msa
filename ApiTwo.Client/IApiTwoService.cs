using ApiTwo.Client.Contracts.Requests;
using ApiTwo.Client.Contracts.Responses;
using System.ServiceModel;

namespace ApiTwo.Client
{
    [ServiceContract]
    public interface IApiTwoService
    {
        [OperationContract]
        ValueTask<TwoResponse> TwoCall(TwoRequest request, CancellationToken token = default);
    }
}
