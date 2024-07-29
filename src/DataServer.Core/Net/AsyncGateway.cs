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

namespace DataServer.Core.Net
{
    public class AsyncGateway : IAsyncGateway
    {
        public IPAddress IPAddress { get; init; }

        public int Port { get; init; }

        public GatewayStatusEnum Status { get; private set; }

        public AsyncGateway(IPAddress iPAddress, int port)
        {
            this.IPAddress = iPAddress;
            this.Port = port;
        }


        public async void Run() => throw new NotImplementedException();

        public static int GetAvailablePort() 
        {
            int port = 0;
            TcpListener tcpListener = new(IPAddress.Loopback, port);
            tcpListener.Start();
            port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
            tcpListener.Stop();
            return port;
        }
    }
}
