using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EiyudenChronicle
{
	internal class ActionCommand : ICommand
	{
		public readonly Action<object?> mAction;
		public event EventHandler? CanExecuteChanged;
		public ActionCommand(Action<object?> action) => mAction = action;

		public bool CanExecute(object? parameter) => true;

		public void Execute(object? parameter) => mAction(parameter);
	}
}
