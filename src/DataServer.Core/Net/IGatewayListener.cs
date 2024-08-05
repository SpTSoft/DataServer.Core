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
using System.Net;

namespace DataServer.Core.Net
{
    public delegate void NotifyClientConnected(object sender, NotifyClientConnectedEventArgs eConnected);

    public delegate void NotifyClientDisconnected(object sender, NotifyClientDisconnectedEventArgs eDisconnected);

    public delegate void NotifyRequestCreated(object sender, NotifyRequestCreatedEventArgs eCreated);

    public interface IGatewayListener
    {
        public event NotifyClientConnected? ClientConnected;
        public event NotifyClientDisconnected? ClientDisconnected;
        public event NotifyRequestCreated? RequestCreated;

        public IPAddress IPAddress { get; }

        public PortNumber Port { get; }

		public GatewayListenerStatusEnum Status { get; }

        public void Run();

        public void Stop();
    }
}
