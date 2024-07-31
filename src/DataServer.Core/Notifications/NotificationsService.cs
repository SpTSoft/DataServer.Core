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

namespace DataServer.Core.Notifications
{
    public class NotificationsService : INotificationsService
    {
        public IGateway Gateway { get; init; }

        public NotificationsService(IGateway gateway) 
        {
            this.Gateway = gateway;
        }

        public void Run() 
        {
            this.Gateway.Run();
        }

        public void Run(params object[] @params) 
        {
            this.Gateway.Run();
        }

        public void Stop() 
        {
            this.Gateway.Stop();
        }

        public void Stop(params object[] @params) 
        {
            this.Gateway.Stop();
        }
    }
}
