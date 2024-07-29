using DataServer.Core.Net;
using System.Net;

namespace DataServer.Core.Tests.Net
{
    [TestClass]
    public class NetHelperTests
    {
        [TestMethod]
        public void GettingAvailablePortInvoking()
        {
            int port = NetHelper.GetAvailablePort();
        }

        [TestMethod]
        public void CanUsePortInvoking()
        {
            IPAddress iPAddress = IPAddress.Any;
            int port = 59000;
            bool canUse = NetHelper.CanUsePort(iPAddress, port);
        }
    }
}