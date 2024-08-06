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

using DataServer.Core.Logging.Settings;

namespace DataServer.Core.Logging
{
	public class FileLogger : ILogger
	{
		private readonly IFileLoggerSettings _Settings;

		private string FilePath => GetFilePath(this._Settings);

		public FileLogger(IFileLoggerSettings settings) 
		{
			this._Settings = settings;

			if (Path.Exists(this._Settings.FilePath) == false)
			{
				Directory.CreateDirectory(this._Settings.FilePath);
			}
		}

		public void Log(string message) 
		{
			if (File.Exists(this.FilePath) == false) { CreateLogFile(this.FilePath); }
			AppendLogFile(this.FilePath, message, this._Settings);
		}

		public void Log(LoggerMessage loggerMessage) => throw new NotImplementedException();

		public void Log(LoggerExceptionMessage exceptionMessage) => throw new NotImplementedException();

		private void CreateLogFile(string path) 
		{
			using (var stream = File.Open(this.FilePath, FileMode.OpenOrCreate)) { }
		}

		private void AppendLogFile(string path, string message, IFileLoggerSettings settings) 
		{ 
			File.AppendAllText(path, settings.PrefixMessage + message + settings.SuffixMessage);
		}

		private string GetFilePath(IFileLoggerSettings settings) 
		{
			if (settings.DateTimePosition == DateTimePositionEnum.Prefix)
			{
				return this._Settings.FilePath + DateTime.Now.ToShortDateString() + " " + this._Settings.FileName + this._Settings.FileExtension;
			}
			else
			{
				return this._Settings.FilePath  + this._Settings.FileName + " " + DateTime.Now.ToShortDateString() + this._Settings.FileExtension;
			}
		}
	}
}
