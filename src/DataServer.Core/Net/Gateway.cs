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
using System.Collections;
using System.Collections.ObjectModel;

namespace DataServer.Core.Net
{
    public class Gateway : IGateway
    {
        private readonly IGatewayListener _GatewayListener;
        private readonly IGatewayInteractionStorage _GatewayInteractionStorage;
        private readonly IGatewaySender _GatewaySender;
        private readonly IGatewayRouter _GatewayRouter;

        private readonly List<IClient> _Clients = new();

        public ReadOnlyCollection<IClient> Clients => _Clients.AsReadOnly();

        public Gateway(IGatewayListener gatewayListener, IGatewayInteractionStorage gatewayInteractionStorage,IGatewayRouter gatewayRouter, IGatewaySender gatewaySender) 
        {
            this._GatewayListener = gatewayListener;

            this._GatewayListener.ClientConnected += _GatewayListener_ClientConnected;
            this._GatewayListener.ClientDisconnected += _GatewayListener_ClientDisconnected;
            this._GatewayListener.RequestCreated += _GatewayListener_RequestCreated;

            this._GatewayInteractionStorage = gatewayInteractionStorage;
            this._GatewaySender = gatewaySender;
            this._GatewayRouter = gatewayRouter;
        }

        public void Run() 
        {
            this._GatewayListener.Run();
        }

        public void Stop() 
        {
            this._GatewayListener.Stop();
        }

        private void AddClient(IClient client) 
        {
            lock (((ICollection)this._Clients).SyncRoot) 
            {
                this._Clients.Add(client);
            }
        }

        private void RemoveClient(IClient client) 
        {
            lock (((ICollection)this._Clients).SyncRoot)
            {
                this._Clients.Remove(client);
            }
        }

		private void _GatewayListener_ClientConnected(object sender, NotifyClientConnectedEventArgs eConnected) 
		{ 
			throw new NotImplementedException(); 
		}

        private void _GatewayListener_ClientDisconnected(object sender, NotifyClientDisconnectedEventArgs eDisconnected)
		{
			throw new NotImplementedException();
		}

		private void _GatewayListener_RequestCreated(object sender, NotifyRequestCreatedEventArgs eCreated)
		{
			throw new NotImplementedException();
		}
	}
}
