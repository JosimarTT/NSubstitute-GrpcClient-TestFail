using Grpc.Core;
using Grpc.Core.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrpcClient;
using NSubstitute;

namespace GrpcClientTests;

[TestClass]
public class UnitTests
{
    private Greeter.GreeterClient grpcClient;

    [TestInitialize]
    public void Init()
    {
        grpcClient = Substitute.For<Greeter.GreeterClient>();
    }

    [TestCleanup]
    public void Cleanup()
    {
        grpcClient = null;
    }

    private GreeterRepository getRepository() => new(grpcClient);

    [TestMethod]
    public async Task TestMethod()
    {
        var grpcRequest = new HelloRequest { Name = "Batman" };
        var grpcResponse = new HelloReply { Message = "Hello Batman" };
        var mockCall = TestCalls.AsyncUnaryCall(Task.FromResult(grpcResponse),
            Task.FromResult(new Metadata()),
            () => Status.DefaultSuccess,
            () => new Metadata(),
            () => { });

        grpcClient.SayHelloAsync(Arg.Any<HelloRequest>(), null, null, CancellationToken.None).Returns(mockCall);

        await getRepository().SayHelloAsync(grpcRequest);

        await grpcClient.Received(1).SayHelloAsync(Arg.Any<HelloRequest>(), null, null, CancellationToken.None);
    }
}