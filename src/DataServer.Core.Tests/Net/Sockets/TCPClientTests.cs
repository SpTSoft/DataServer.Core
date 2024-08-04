using DataServer.Core.Net.Entities.Sockets;
using System.Net.Sockets;

namespace DataServer.Core.Tests.Net.Sockets
{

	[TestClass]
	public class TCPClientTests
	{
		[TestMethod]
		public void CastTest()
		{
			TcpClient tcpClient = new TcpClient();
			TCPClient client = new(tcpClient);
		}

	}
}
