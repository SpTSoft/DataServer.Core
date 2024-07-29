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

using System.Net.Sockets;
using System.Net;

namespace DataServer.Core.Net
{
    public static class NetHelper
    {
        /// <summary>
        /// Проверка возможности использования порта
        /// </summary>
        /// <param name="iPAddress">IP адрес</param>
        /// <param name="port">Порт</param>
        /// <returns>true - при возмодности использовать</returns>
        public static bool CanUsePort(IPAddress iPAddress, int port)
        {
            bool isCanUsePort = false;
            using (TcpClient tcpClient = new())
            {
                try
                {
                    tcpClient.Connect(iPAddress, port);
                    isCanUsePort=false;
                }
                catch (Exception)
                {
                    isCanUsePort=true;
                }
            }
            return isCanUsePort;
        }

        /// <summary>
        /// Получить свободный порт
        /// </summary>
        /// <returns>Порт</returns>
        public static int GetAvailablePort()
        {
            int port = 0;
            TcpListener tcpListener = new(IPAddress.Loopback, port);
            tcpListener.Start();
            port=((IPEndPoint)tcpListener.LocalEndpoint).Port;
            tcpListener.Stop();
            return port;
        }

    }
}
