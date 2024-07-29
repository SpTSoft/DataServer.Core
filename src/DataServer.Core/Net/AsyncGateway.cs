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

using DataServer.Core.Net.Exceptions;
using System.Collections;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;

namespace DataServer.Core.Net
{
    public class AsyncGateway : IAsyncGateway
    {
        private readonly List<TcpClient> _Clients = new ();

        public IPAddress IPAddress { get; init; }

        public int Port { get; init; }

        public GatewayStatusEnum Status { get; private set; }

        public ReadOnlyCollection<TcpClient> Clients => _Clients.AsReadOnly();

        public AsyncGateway(IPAddress iPAddress, int port)
        {
            if (NetHelper.CanUsePort(iPAddress, port) == true)
            {
                this.IPAddress = iPAddress;
                this.Port = port;
                this.Status = GatewayStatusEnum.NotStarted;
            }
            else { throw new AccessPortException("Port:" + port + " is locked."); }
        }

        public async void Run()
        {
            TcpListener tcpListener = new(this.IPAddress, this.Port);
            tcpListener.Start();
            this.Status = GatewayStatusEnum.Working;
            while (this.Status == GatewayStatusEnum.Working)
            {
                try
                {
                    TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                    lock (((ICollection)this._Clients).SyncRoot) 
                    {
                        this._Clients.Add(tcpClient);
                    }
                    Task task = Process(tcpClient);
                    await task;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }
        }

        public void Stop() 
        {
            this.Status = GatewayStatusEnum.Stoped;
        }

        private async Task Process(TcpClient tcpClient) { throw new NotImplementedException(); }

    }
}
