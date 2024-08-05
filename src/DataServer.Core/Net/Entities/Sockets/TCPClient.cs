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
using System.Reflection;

namespace DataServer.Core.Net.Entities.Sockets
{
	public class TCPClient : TcpClient, ITCPClient 
	{
		public TCPClient(IPEndPoint localEP) : base(localEP) { }

		public TCPClient() : base() { }

		public TCPClient(TcpClient tcpClient) 
		{
			this.Client = tcpClient.Client;
			this.Active = InternalGetActiveValue(tcpClient);
		}

		internal bool InternalGetActiveValue(TcpClient tcpClient) 
		{
			object? myProtectedProperty = tcpClient.GetType().GetProperty("Active", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(tcpClient);
			return (bool)myProtectedProperty;
		}

		public TCPClient(AddressFamily family) : base(family) { }

		public TCPClient(string hostname, int port) : base(hostname, new PortNumber(port)) { }
	}
}
