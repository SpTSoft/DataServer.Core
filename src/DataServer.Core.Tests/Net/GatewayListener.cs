using DataServer.Core.Logging;
using DataServer.Core.Net;
using DataServer.Core.Net.Args;
using System.Net;

namespace DataServer.Core.Tests.Net
{
    [TestClass]
    public class GatewayListenerTests
    {
        [TestMethod]
        public void CreateInstance()
        {
            int availablePort = NetHelper.GetAvailablePort();

            IGatewayListener gatewayListener = new GatewayListener(IPAddress.Any, availablePort, new GatewayListenerArgsFactory(), new NullLogger());
        }

        [TestMethod]
        public void RunInstance()
        {
            int availablePort = NetHelper.GetAvailablePort();

            IGatewayListener gatewayListener = new GatewayListener(IPAddress.Any, availablePort, new GatewayListenerArgsFactory(), new NullLogger());
			gatewayListener.Run();
        }

        [TestMethod]
        public void StopInstance()
        {
            int availablePort = NetHelper.GetAvailablePort();

            IGatewayListener gatewayListener = new GatewayListener(IPAddress.Any, availablePort, new GatewayListenerArgsFactory(), new NullLogger());
			gatewayListener.Run();
            gatewayListener.Stop();
        }
    }
}
