using System.Net;

namespace DataServer.Core.Demo.MainMVVM
{
	public class MainModel
	{
		public int Port { get; set; }

		public IPAddress IPAddress { get; set; } = IPAddress.Any;

	}
}
