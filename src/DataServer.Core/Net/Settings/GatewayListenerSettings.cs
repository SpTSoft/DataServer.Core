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

using DataServer.Core.Net.Entities;
using DataServer.Core.Settings;
using DataServer.Core.Settings.Args;
using System.Net;

namespace DataServer.Core.Net.Settings
{
	public class GatewayListenerSettings : IGatewayListenerSettings
	{
		public event NotifySettingsChanged? SettingsChanged;

		private IPAddress _IPAddress;
		private PortNumber _Port;

		public IPAddress IPAddress 
		{
			get { return this._IPAddress; }
			set 
			{
				if (this._IPAddress != value)
				{
					SetVariable(ref this._IPAddress, value);
				}
			}
		}

		public PortNumber Port 
		{
			get { return this._Port; }
			set
			{
				if (this.Port != value)
				{
					SetVariable(ref this._Port, value);
				}
			}
		}

		#pragma warning disable CS8618
		public GatewayListenerSettings(IPAddress iPAddress, PortNumber port)
		#pragma warning restore CS8618 
		{
			this.IPAddress = iPAddress;
			this.Port = port;	
		}

		public GatewayListenerSettings(IPAddress iPAddress, int port) : this(iPAddress, (PortNumber)port) { }

		protected virtual void OnSettingsChanged(NotifySettingsChangedEventArgs eChanged) { }

		private void OnSettingsChangedBasic(NotifySettingsChangedEventArgs eChanged)
		{
			OnSettingsChanged(eChanged);
			this.SettingsChanged?.Invoke(this, eChanged);
		}

		private void SetVariable<T>(ref T variable, T value) 
		{
			NotifySettingsChangedEventArgs eChanged = new(variable, value);
			variable = value;
			OnSettingsChangedBasic(eChanged);
		}
	}
}
