using DataServer.Core.Net;
using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;

namespace DataServer.Core.Demo.MainMVVM
{
	public class MainViewModel : IMainViewModel
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		public event PropertyChangingEventHandler? PropertyChanging;

		private IGatewayListener _GatewayListener;

		public MainViewModel(IGatewayListener gatewayListener)
		{
			this._GatewayListener = gatewayListener;
		}


		public int Port
		{
			get { return this._GatewayListener.Port; }
		}

		public IPAddress IPAddress
		{
			get { return this._GatewayListener.IPAddress; }
		}

		public void OnPropertyChanged([CallerMemberName] string? prop = "")
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		public void OnPropertyChanging([CallerMemberName] string? prop = "")
		{
			this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(prop));
		}
	}
}
