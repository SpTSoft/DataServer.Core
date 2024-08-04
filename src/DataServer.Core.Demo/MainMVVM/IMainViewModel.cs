using System.ComponentModel;
using System.Net;

namespace DataServer.Core.Demo.MainMVVM
{
	public interface IMainViewModel : INotifyPropertyChanged, INotifyPropertyChanging
	{
		public int Port { get; }

		public IPAddress IPAddress { get; }
	}
}
