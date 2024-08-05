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

namespace DataServer.Core.Net.Entities
{
	public class PortNumber
	{
		private int _Port;

		public int Port 
		{
			get {  return this._Port; }
			set 
			{
				if ((this._Port != value) && (ValidationPort(value) == true))
				{
					this._Port = value;
				}
			}
		}

		public bool IsNeedCallException { get; set; } = true;

		public PortNumber(int port) 
		{
			this.Port = port;
		}

		protected bool ValidationPort(int port, bool IsNeedCallException = true) 
		{
			if ((port >= IPEndPoint.MinPort) && (port <= IPEndPoint.MaxPort))
			{
				return true;
			}
			else
			{
				if (IsNeedCallException == true)
				{
					throw new IndexOutOfRangeException("Port is't valid");
				}
				else { return false; }
			}
		}

		public override string ToString() => this.Port.ToString();

		public static implicit operator int(PortNumber d) => d.Port;

		public static implicit operator PortNumber(int b) => new(b);
	}
}
