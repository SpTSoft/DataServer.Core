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
	public interface ITCPClient : INetClient
	{
		public void Connect(IPAddress[] ipAddresses, int port);

		public IAsyncResult BeginConnect(string host, int port, AsyncCallback requestCallback, object state);

		public IAsyncResult BeginConnect(IPAddress address, int port, AsyncCallback requestCallback, object state);

		public IAsyncResult BeginConnect(IPAddress[] addresses, int port, AsyncCallback requestCallback, object state);

		public void EndConnect(IAsyncResult asyncResult);

		public Task ConnectAsync(IPAddress address, int port);

		public Task ConnectAsync(string host, int port);

		public Task ConnectAsync(IPAddress[] addresses, int port);

		public NetworkStream GetStream();

		public int ReceiveBufferSize { get; set; }

		public int SendBufferSize { get; set; }

		public int ReceiveTimeout { get; set; }

		public int SendTimeout { get; set; }

		public LingerOption LingerState { get; set; }

		public bool NoDelay { get; set; }

	}
}
