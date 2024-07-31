using System.Net;

namespace DataServer.Core.Demo.Client
{
	public class MainWindowModel 
	{
		public int Port { get; set; }

		public IPAddress IPAddress { get; set; } = IPAddress.Any;

		public string Log { get; set; } = string.Empty;
	}
}
