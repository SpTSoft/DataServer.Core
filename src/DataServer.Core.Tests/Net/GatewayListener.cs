using DataServer.Core.Net;
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

            IGatewayListener gatewayListener = new GatewayListener(IPAddress.Any, availablePort, new GatewayListenerArgsFactory());
        }

        [TestMethod]
        public void RunInstance()
        {
            int availablePort = NetHelper.GetAvailablePort();

            IGatewayListener gatewayListener = new GatewayListener(IPAddress.Any, availablePort, new GatewayListenerArgsFactory());
            gatewayListener.Run();
        }

        [TestMethod]
        public void StopInstance()
        {
            int availablePort = NetHelper.GetAvailablePort();

            IGatewayListener gatewayListener = new GatewayListener(IPAddress.Any, availablePort, new GatewayListenerArgsFactory());
            gatewayListener.Run();
            gatewayListener.Stop();
        }
    }
}