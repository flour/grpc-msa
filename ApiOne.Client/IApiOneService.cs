using ApiOne.Client.Contracts.Requests;
using ApiOne.Client.Contracts.Responses;
using System.ServiceModel;

namespace ApiOne.Client
{
    [ServiceContract]
    public interface IApiOneService
    {
        [OperationContract]
        ValueTask<OneResponse> OneCall(OneRequest request, CancellationToken token = default);

        [OperationContract]
        IAsyncEnumerable<OneResponse> StreamCall(OneRequest request, CancellationToken token = default);

        [OperationContract]
        ValueTask<Result> StoreStream(IAsyncEnumerable<SomeData> request, CancellationToken token = default);
    }
}
