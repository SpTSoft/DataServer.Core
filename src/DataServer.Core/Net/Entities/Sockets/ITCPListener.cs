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
	public interface ITCPListener : INetListener
	{
		public abstract static TCPListener Create(int port);

		public Socket Server { get; }

		public EndPoint LocalEndpoint { get; }

		public bool ExclusiveAddressUse { get; set; }

		public void AllowNatTraversal(bool allowed);

		public void Start();

		public void Start(int backlog);

		public void Stop();

		public bool Pending();

		public Socket AcceptSocket();

		public TCPClient AcceptTcpClient();

		public IAsyncResult BeginAcceptSocket(AsyncCallback callback, object state);

		public Socket EndAcceptSocket(IAsyncResult asyncResult);

		public IAsyncResult BeginAcceptTcpClient(AsyncCallback callback, object state);

		public TCPClient EndAcceptTcpClient(IAsyncResult asyncResult);

		public Task<Socket> AcceptSocketAsync();

		public Task<TCPClient> AcceptTcpClientAsync();
	}
}
