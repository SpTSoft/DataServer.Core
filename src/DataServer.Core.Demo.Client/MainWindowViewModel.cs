using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataServer.Core.Demo.Client
{
	public class MainWindowViewModel : INotifyPropertyChanged, INotifyPropertyChanging
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		public event PropertyChangingEventHandler? PropertyChanging;

		private readonly MainWindowModel _Model = new();

		internal Task<string> _TaskResponse;

		public int Port
		{
			get { return this._Model.Port; }
			set
			{
				if (this._Model.Port != value)
				{
					OnPropertyChanging(nameof(this.Port));
					this._Model.Port = value;
					OnPropertyChanged(nameof(this.Port));
				}
			}
		}

		public IPAddress IPAddress
		{
			get { return this._Model.IPAddress; }
			set
			{
				if (this._Model.IPAddress != value)
				{
					OnPropertyChanging(nameof(this.IPAddress));
					this._Model.IPAddress = value;
					OnPropertyChanged(nameof(this.IPAddress));
				}
			}
		}

		public string Log
		{
			get { return this._Model.Log; }
			set
			{
				if (this._Model.Log != value)
				{
					OnPropertyChanging(nameof(this.Log));
					this._Model.Log = value;
					OnPropertyChanged(nameof(this.Log));
				}
			}
		}

		public MainWindowViewModel() 
		{
			this.Connect = new RelayCommand(ConnectAction, CanConnectingFunction);
		}

		public void OnPropertyChanged([CallerMemberName] string? prop = "")
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		public void OnPropertyChanging([CallerMemberName] string? prop = "")
		{
			this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(prop));
		}

		public ICommand? Connect { get; init; }

		private Action<object?> ConnectAction => (obj) =>
		{
			if (obj is MainWindowViewModel viewModel == true)
			{
				viewModel.Log += "Нажата кнопка старта" + Environment.NewLine;
				ConnectMethod();
			}
		};

		private Func<object?, bool> CanConnectingFunction => (obj) =>
		{
			if (obj is MainWindowViewModel viewModel == true)
			{ 
				if ((viewModel.Port != 0)&&((viewModel._TaskResponse == null)||(viewModel._TaskResponse.IsCompleted == true))) { return true; }
			}
			return false;
		};

		private async void ConnectMethod() 
		{
			this._TaskResponse = SendRequest(this.IPAddress, this.Port);

			await this._TaskResponse;
			this.Log += this._TaskResponse.Result.ToString() + Environment.NewLine;
		}

		private static async Task<string> SendRequest(IPAddress ipAddress, int port)
		{
			try
			{
				TcpClient client = new();
				await client.ConnectAsync(ipAddress, port); // соединение
				NetworkStream networkStream = client.GetStream();
				StreamWriter writer = new StreamWriter(networkStream);
				StreamReader reader = new StreamReader(networkStream);
				writer.AutoFlush = true;
				string requestData = "method=";
				await writer.WriteLineAsync(requestData);
				string response = await reader.ReadLineAsync();
				client.Close();
				return response;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}

	}
}
