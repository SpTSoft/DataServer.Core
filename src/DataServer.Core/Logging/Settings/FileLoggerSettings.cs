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

using DataServer.Core.Settings;
using DataServer.Core.Settings.Args;

namespace DataServer.Core.Logging.Settings
{
	public class FileLoggerSettings : IFileLoggerSettings
	{
		private readonly IFileLoggerSettings _DefaultFileLoggerSettings = new DefaultFileLoggerSettings();

		private string? _FileName;
		private string? _FilePath;
		private string? _FileExtension;
		private string _PrefixMessage;
		private string _SuffixMessage;
		private DateTimePositionEnum _DateTimePosition;

		public event NotifySettingsChanged? SettingsChanged;

		public string FileName 
		{
			get { return GetVariable(this._FileName, this._DefaultFileLoggerSettings.FileName); }
			set { SetVariable(ref this._FileName, value); }
		}

		public string FilePath
		{
			get { return GetVariable(this._FilePath, this._DefaultFileLoggerSettings.FilePath); }
			set { SetVariable(ref this._FilePath, value); }
		}

		public string FileExtension
		{
			get { return GetVariable(this._FileExtension, this._DefaultFileLoggerSettings.FileExtension); }
			set { SetVariable(ref this._FileExtension, value); }
		}

		public DateTimePositionEnum DateTimePosition
		{
			get { return GetVariable(this._DateTimePosition, this._DefaultFileLoggerSettings.DateTimePosition); }
			set { SetVariable(ref this._DateTimePosition, value); }
		}

		public string PrefixMessage
		{
			get { return GetVariable(this._PrefixMessage, this._DefaultFileLoggerSettings.PrefixMessage); }
			set { SetVariable(ref this._PrefixMessage, value); }
		}

		public string SuffixMessage
		{
			get { return GetVariable(this._SuffixMessage, this._DefaultFileLoggerSettings.SuffixMessage); }
			set { SetVariable(ref this._SuffixMessage, value); }
		}

		public FileLoggerSettings() 
		{
		
		}

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

		private T GetVariable<T>(T variable, T defVariable) 
		{
			if (variable == null)
			{
				return defVariable;
			}
			else
			{
				return variable;
			}
		}
	}
}
