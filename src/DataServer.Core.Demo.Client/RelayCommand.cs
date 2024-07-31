using System;
using System.Windows.Input;

namespace DataServer.Core.Demo.Client
{
	public class RelayCommand : ICommand
	{
		private readonly Action<object?> _Execute;
		private readonly Func<object?, bool>? _CanExecute;

		event EventHandler? ICommand.CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
		{
			this._Execute = execute;
			this._CanExecute = canExecute;
		}

		bool ICommand.CanExecute(object? parameter) { return this._CanExecute == null || this._CanExecute(parameter); }

		void ICommand.Execute(object? parameter) { this._Execute(parameter); }
	}
}
