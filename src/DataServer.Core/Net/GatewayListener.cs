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

using DataServer.Core.Logging;
using DataServer.Core.Net.Args;
using DataServer.Core.Net.Entities;
using DataServer.Core.Net.Entities.Sockets;
using DataServer.Core.Net.Exceptions;
using DataServer.Core.Net.Settings;
using System.Net;

namespace DataServer.Core.Net
{
	public class GatewayListener : IGatewayListener
    {
		private readonly ILogger _Logger;
		private readonly IGatewayListenerArgsFactory _ArgsFactory;

        public event NotifyClientConnected? ClientConnected;
        public event NotifyClientDisconnected? ClientDisconnected;
        public event NotifyRequestCreated? RequestCreated;

		private IPAddress _IPAddress;
		private PortNumber _Port;

		private GatewayListenerStatusEnum _Status = GatewayListenerStatusEnum.NotStarted;

		public IPAddress IPAddress 
		{
			get { return this._IPAddress; }
			private set
			{
				this._IPAddress = value;
				this._Logger.Log("GatewayListener: IPAddress Setted:" + value.ToString());
			}
		}

        public PortNumber Port
		{
			get { return this._Port; }
			private set
			{
				this._Port = value;
				this._Logger.Log("GatewayListener: Port Setted:" + value.ToString());
			}
		}

		public GatewayListenerStatusEnum Status 
		{ 
			get {  return this._Status; } 
			private set 
			{
				this._Status = value;
				this._Logger.Log("GatewayListener: Status Setted:" + value.ToString());
			}
		} 

		public GatewayListener(IGatewayListenerSettings settings, IGatewayListenerArgsFactory argsFactory, ILogger logger) : 
			this(settings.IPAddress, settings.Port, argsFactory, logger) { }

		#pragma warning disable CS8618
		public GatewayListener(IPAddress iPAddress, int port, IGatewayListenerArgsFactory argsFactory, ILogger logger)
		#pragma warning restore CS8618
		{
			this._Logger = logger;
			
			PortNumber portNumber = port;

			if (NetHelper.CanUsePort(iPAddress, portNumber))
            {
                this.IPAddress = iPAddress;
                this.Port = portNumber;
            }
            else 
			{
				throw ExceptionLog<AccessPortException>("GatewayListener: Port:" + portNumber + " is locked.");
			}

			this._ArgsFactory = argsFactory;
        }

        public async void Run() 
        {
			this._Logger.Log("GatewayListener: Calling Async Method Run");

            TCPListener listener = new(this.IPAddress, this.Port);
            listener.Start();

			this._Logger.Log("GatewayListener: TCPListener Start:" + this.IPAddress.ToString() + "/" + this.Port.ToString());

			this.Status = GatewayListenerStatusEnum.Working;
			

			while (this.Status == GatewayListenerStatusEnum.Working)
			{
				/*try
				{*/
				TCPClient tcpClient = await listener.AcceptTcpClientAsync();
				this._Logger.Log("GatewayListener: TCPListener Accept Client:" + tcpClient.ToString());

				NotifyClientConnectedEventArgs eConnected = this._ArgsFactory.CreateConnectedEventArgs(tcpClient);
				OnClientConnectedBasic(eConnected);

				Task taskRequest = ReceivingRequest(tcpClient);
				await taskRequest;

				NotifyRequestCreatedEventArgs eCreated = this._ArgsFactory.CreateRequestEventArgs(taskRequest);
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

		private T ExceptionLog<T>(string message) where T : Exception, new()
		{
			object[] args = { message };

			T exception = (T)Activator.CreateInstance(typeof(T), args);

			this._Logger.Log(new LoggerExceptionMessage(message, exception));

			return exception;
		}
	}
}
