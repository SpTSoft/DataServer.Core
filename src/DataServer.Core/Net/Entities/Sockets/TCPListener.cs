/*
* Copyright 2024 SpTSoftware
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
* 
*     http://www.apache.org/licenses/LICENSE-2.0
* 
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System.Net;
using System.Net.Sockets;

namespace DataServer.Core.Net.Entities.Sockets
{
	public class TCPListener : TcpListener, ITCPListener
	{
		public TCPListener(IPEndPoint localEP) : base(localEP) { }

		public TCPListener(IPAddress localaddr, int port) : base(localaddr, new PortNumber(port)) { }

		[Obsolete("This method has been deprecated. Please use TcpListener(IPAddress localaddr, int port) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public TCPListener(int port) : base(new PortNumber(port)) { }

		public new static TCPListener Create(int port) 
		{
			PortNumber portNumber = port;
			TCPListener listener = new(IPAddress.IPv6Any, portNumber);
			listener.Server.DualMode = true;

			return listener;
		}

		public new TCPClient AcceptTcpClient() 
		{
			return new TCPClient(base.AcceptTcpClient());
		}

		public new IAsyncResult BeginAcceptTcpClient(AsyncCallback callback, object state) => base.BeginAcceptTcpClient(callback, state);

		public new TCPClient EndAcceptTcpClient(IAsyncResult asyncResult) 
		{
			return new TCPClient(base.EndAcceptTcpClient(asyncResult));
		}

		public new Task<TCPClient> AcceptTcpClientAsync() 
		{
			Task<TcpClient> basedTask = base.AcceptTcpClientAsync();
			Task<TCPClient> yourTaskObject = basedTask.ContinueWith(t => new TCPClient(t.Result));
			return yourTaskObject;
		}
	}
}
