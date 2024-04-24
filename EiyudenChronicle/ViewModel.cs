using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EiyudenChronicle
{
	internal class ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public Info Info { get; private set; } = Info.Instance();
		public ICommand FileOpenCommand { get; private set; }
		public ICommand FileSaveCommand { get; private set; }
		public ICommand ChoiceItemCommand { get; private set; }
		public Basic Basic { get; private set; } = new Basic();
		public ObservableCollection<Item> Items { get; private set; } = new ObservableCollection<Item>();

		public ViewModel()
		{
			FileOpenCommand = new ActionCommand(FileOpen);
			FileSaveCommand = new ActionCommand(FileSave);
			ChoiceItemCommand = new ActionCommand(ChoiceItem);
		}

		private void Initialize()
		{
			Items.Clear();
			foreach (var node in SaveData.Instance().Json?["_inventory"]?["_items"]?.AsArray())
			{
				Items.Add(new Item(node));
			}

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Basic)));
		}

		private void FileOpen(object? parameter)
		{
			var dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Open(dlg.FileName);
			Initialize();
		}

		private void FileSave(object? parameter)
		{
			SaveData.Instance().Save();
		}

		private void ChoiceItem(object? parameter)
		{
			var item = parameter as Item;
			if (item == null) return;

			var dlg = new ChoiceWindow();
			dlg.ID = uint.Parse(item.ID);
			dlg.ShowDialog();
			item.ID = dlg.ID.ToString();
		}
	}
}
