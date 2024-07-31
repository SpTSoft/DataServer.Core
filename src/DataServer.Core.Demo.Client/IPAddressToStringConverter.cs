using System;
using System.Globalization;
using System.Net;
using System.Windows.Data;

namespace DataServer.Core.Demo.Client
{
	public class IPAddressToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
		{
			return value.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
		{
			return IPAddress.Parse(value.ToString());
		}
	}
}
