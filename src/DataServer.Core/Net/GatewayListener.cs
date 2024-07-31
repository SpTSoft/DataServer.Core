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
using DataServer.Core.Net.Exceptions;
using System.Net;
using System.Net.Sockets;

namespace DataServer.Core.Net
{
    public class GatewayListener : IGatewayListener
    {
        public event NotifyClientConnected? ClientConnected;
        public event NotifyClientDisconnected? ClientDisconnected;
        public event NotifyRequestCreated? RequestCreated;

        public IPAddress IPAddress { get; private set; }

        public int Port { get; private set;}

		public GatewayListenerStatusEnum Status { get; private set; } = GatewayListenerStatusEnum.NotStarted;

        public GatewayListener(IPAddress iPAddress, int Port) 
        {
            if (NetHelper.CanUsePort(iPAddress, Port))
            {
                this.IPAddress = iPAddress;
                this.Port = Port;
            }
            else { throw new AccessPortException("Port:" + Port + " is locked."); }
        }

        public async void Run() 
        {
            TcpListener listener = new(this.IPAddress, this.Port);
            listener.Start();
			this.Status = GatewayListenerStatusEnum.Working;

			while (this.Status == GatewayListenerStatusEnum.Working)
			{
				/*try
				{*/
				TcpClient tcpClient = await listener.AcceptTcpClientAsync();

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

		private NotifyClientConnectedEventArgs CreateConnectedArgs(TcpClient tcpClient) 
		{
			throw new NotImplementedException();
		}

		private NotifyRequestCreatedEventArgs CreateRequestArgs(Task task) 
		{
			throw new NotImplementedException();
		}

		private Task ReceivingRequest(TcpClient tcpClient) 
		{
			throw new NotImplementedException();
		}
	}
}
