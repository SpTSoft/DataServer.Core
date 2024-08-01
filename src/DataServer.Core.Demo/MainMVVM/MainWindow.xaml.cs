using DataServer.Core.Demo.DI;
using System.Windows;

namespace DataServer.Core.Demo.MainMVVM
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IMainWindow
	{
        public MainWindow()
        {
            InitializeComponent();
        }

		public MainWindow(IMainViewModel mainViewModel)
		{
			InitializeComponent();
			this.DataContext = mainViewModel;
		}
	}
}
