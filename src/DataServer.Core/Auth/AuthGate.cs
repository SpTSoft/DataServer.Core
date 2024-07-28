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

using DataServer.Core.Net;

namespace DataServer.Core.Auth
{
    public class AuthGate : IAuthGate
    {
        private readonly INetGate _NetGate;
        private readonly IAuthCore _AuthCore;

        IReadOnlyNetGate IAuthGate.NetGate => this._NetGate.ConvertToReadOnly();

        public AuthGate(INetGate netGate, IAuthCore authCore) 
        {
            this._NetGate = netGate;
            this._AuthCore = authCore;
        }
    }
}
