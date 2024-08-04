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
	public interface IUDPClient : INetClient
	{
		public int Send(byte[] dgram, int bytes, IPEndPoint endPoint);

		public int Send(byte[] dgram, int bytes, string hostname, int port);

		public int Send(byte[] dgram, int bytes);

		public IAsyncResult BeginSend(byte[] datagram, int bytes, IPEndPoint endPoint, AsyncCallback requestCallback, object state);

		public IAsyncResult BeginSend(byte[] datagram, int bytes, string hostname, int port, AsyncCallback requestCallback, object state);

		public IAsyncResult BeginSend(byte[] datagram, int bytes, AsyncCallback requestCallback, object state);

		public int EndSend(IAsyncResult asyncResult);

		public byte[] Receive(ref IPEndPoint remoteEP);

		public IAsyncResult BeginReceive(AsyncCallback requestCallback, object state);

		public byte[] EndReceive(IAsyncResult asyncResult, ref IPEndPoint? remoteEP);

		public void JoinMulticastGroup(IPAddress multicastAddr);

		public void JoinMulticastGroup(IPAddress multicastAddr, IPAddress localAddress);

		public void JoinMulticastGroup(int ifindex, IPAddress multicastAddr);

		public void JoinMulticastGroup(IPAddress multicastAddr, int timeToLive);

		public void DropMulticastGroup(IPAddress multicastAddr);

		public void DropMulticastGroup(IPAddress multicastAddr, int ifindex);

		public Task<int> SendAsync(byte[] datagram, int bytes);

		public Task<int> SendAsync(byte[] datagram, int bytes, IPEndPoint endPoint);

		public Task<int> SendAsync(byte[] datagram, int bytes, string hostname, int port);

		public Task<UdpReceiveResult> ReceiveAsync();
	}
}
