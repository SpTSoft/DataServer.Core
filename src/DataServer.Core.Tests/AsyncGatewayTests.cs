using DataServer.Core.Net;
using System.Net;

namespace DataServer.Core.Tests
{
    [TestClass]
    public class AsyncGatewayTests
    {
        [TestMethod]
        public void Initialize()
        {
            IPAddress iPAddress = IPAddress.Any;
            int port = 59000;
            IAsyncGateway asyncGateway = new AsyncGateway(iPAddress, port);
        }

        [TestMethod]
        public void PortValidation()
        {   
        }

        [TestMethod]
        public void Running()
        {
            IPAddress iPAddress = IPAddress.Any;
            int port = 59000;
            IAsyncGateway asyncGateway = new AsyncGateway(iPAddress, port);
            asyncGateway.Run();
        }

        [TestMethod]
        public void Stopping()
        {
            IPAddress iPAddress = IPAddress.Any;
            int port = 59000;
            IAsyncGateway asyncGateway = new AsyncGateway(iPAddress, port);
            asyncGateway.Stop();
        }

        [TestMethod]
        public void GettingAvailablePort()
        {
            int port = AsyncGateway.GetAvailablePort();
        }
    }
}