using Grpc.Core;

namespace GrpcClient;

public class GreeterRepository : IGreeterService
{
    private Greeter.GreeterClient _greeterClient { get; }

    public GreeterRepository(Greeter.GreeterClient greeterClient)
    {
        _greeterClient = greeterClient;
    }


    public async Task<HelloReply> SayHelloAsync(HelloRequest request)
    {
        return await _greeterClient.SayHelloAsync(request);
    }
}

public interface IGreeterService
{
    Task<HelloReply> SayHelloAsync(HelloRequest request);
}