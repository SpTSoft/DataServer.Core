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

using DataServer.Core.Access;
using DataServer.Core.Database;
using DataServer.Core.Modeling;

namespace DataServer.Core
{
    public class CoreWrapper : ICoreWrapper
    {
        private readonly IModuleCompositor _ModuleCompositor;
        private readonly IAccessGate _AccessGate;
        private readonly IDBGate _DBGate;

        public CoreWrapper(IModuleCompositor moduleCompositor, IDBGate dBGate, IAccessGate accessGate) 
        {
            this._ModuleCompositor = moduleCompositor;
            this._DBGate = dBGate;
            this._AccessGate = accessGate;
        }
    }
}
