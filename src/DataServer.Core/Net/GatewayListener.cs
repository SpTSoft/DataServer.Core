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

using DataServer.Core.Net.Args;
using DataServer.Core.Net.Entities;
using DataServer.Core.Net.Entities.Sockets;
using DataServer.Core.Net.Exceptions;
using System.Net;

namespace DataServer.Core.Net
{
	public class GatewayListener : IGatewayListener
    {
        public event NotifyClientConnected? ClientConnected;
        public event NotifyClientDisconnected? ClientDisconnected;
        public event NotifyRequestCreated? RequestCreated;

        public IPAddress IPAddress { get; private set; }

        public PortNumber Port { get; private set;}

		public GatewayListenerStatusEnum Status { get; private set; } = GatewayListenerStatusEnum.NotStarted;

        public GatewayListener(IPAddress iPAddress, int port) 
        {
			PortNumber portNumber = port;

			if (NetHelper.CanUsePort(iPAddress, portNumber))
            {
                this.IPAddress = iPAddress;
                this.Port = portNumber;
            }
            else { throw new AccessPortException("Port:" + portNumber + " is locked."); }
        }

        public async void Run() 
        {
            TCPListener listener = new(this.IPAddress, this.Port);
            listener.Start();
			this.Status = GatewayListenerStatusEnum.Working;

			while (this.Status == GatewayListenerStatusEnum.Working)
			{
				/*try
				{*/
				TCPClient tcpClient = await listener.AcceptTcpClientAsync();

				NotifyClientConnectedEventArgs eConnected = CreateConnectedArgs(tcpClient);
				OnClientConnectedBasic(eConnected);

				Task taskRequest = ReceivingRequest(tcpClient);
				await taskRequest;

				NotifyRequestCreatedEventArgs eCreated = CreateRequestArgs(taskRequest);
				OnRequestCreatedBasic(eCreated);
				/*}
				catch (Exception) { }*/
			}
        }

		public void Stop() 
		{
			this.Status = GatewayListenerStatusEnum.Stoped;
		}

		protected virtual void OnClientConnected(NotifyClientConnectedEventArgs eConnected) { }

		protected virtual void OnClientDisconnected(NotifyClientDisconnectedEventArgs eDisconnected) { }

		protected virtual void OnRequestCreated(NotifyRequestCreatedEventArgs eCreated) { }

		private void OnClientConnectedBasic(NotifyClientConnectedEventArgs eConnected) 
		{
			OnClientConnected(eConnected);
			this.ClientConnected?.Invoke(this, eConnected);
		}

		private void OnClientDisconnectedBasic(NotifyClientDisconnectedEventArgs eDisconnected)
		{
			OnClientDisconnected(eDisconnected);
			this.ClientDisconnected?.Invoke(this, eDisconnected);
		}

		private void OnRequestCreatedBasic(NotifyRequestCreatedEventArgs eCreated)
		{
			OnRequestCreated(eCreated);
			this.RequestCreated?.Invoke(this, eCreated);
		}

		private NotifyClientConnectedEventArgs CreateConnectedArgs(INetClient netClient) 
		{
			NotifyClientConnectedEventArgs e = new(netClient);
			return e;
		}

		private NotifyRequestCreatedEventArgs CreateRequestArgs(Task task) 
		{
			throw new NotImplementedException();
		}

		protected Task ReceivingRequest(INetClient netClient) 
		{
			if (netClient as ITCPClient != null)
			{
				return ReceivingRequest((ITCPClient)netClient);
			}
			else if(netClient as IUDPClient != null)
			{
				return ReceivingRequest((IUDPClient)netClient);
			}
			throw new NotImplementedException();
		}

		protected async Task ReceivingRequest(ITCPClient tcpClient) 
		{
			///string clientEndPoint = tcpClient.Client.RemoteEndPoint.ToString(); Console.WriteLine("Received connection request from " + clientEndPoint);
			/*try
			{
				NetworkStream networkStream = tcpClient.GetStream();
				StreamReader reader = new StreamReader(networkStream);
				StreamWriter writer = new StreamWriter(networkStream);
				writer.AutoFlush = true;
				while (true)
				{
					string request = await reader.ReadLineAsync();
					if (request != null)
					{
						Console.WriteLine("Received service request: " + request);
						string response = Response(request);
						Console.WriteLine("Computed response is: " + response + "\n");
						await writer.WriteLineAsync(response);
					}
					else
						break; // клиент закрыл соединение
				}
				tcpClient.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				if (tcpClient.Connected)
					tcpClient.Close();
			}*/
			throw new NotImplementedException();
		}

		protected async Task ReceivingRequest(IUDPClient netClient)
		{
			throw new NotImplementedException();
		}
	}
}
