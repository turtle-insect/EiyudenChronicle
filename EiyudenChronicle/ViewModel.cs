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
		public ICommand ChoiceCharacterCommand { get; private set; }
		public Basic Basic { get; private set; } = new Basic();
		public ObservableCollection<Item> Items { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Unit> Party { get; private set; } = new ObservableCollection<Unit>();
		public ObservableCollection<Character> Characters { get; private set; } = new ObservableCollection<Character>();

		public ViewModel()
		{
			FileOpenCommand = new ActionCommand(FileOpen);
			FileSaveCommand = new ActionCommand(FileSave);
			ChoiceItemCommand = new ActionCommand(ChoiceItem);
			ChoiceCharacterCommand = new ActionCommand(ChoiceCharacter);
		}

		private void Initialize()
		{
			Party.Clear();
			Items.Clear();
			Characters.Clear();

			foreach (var node in SaveData.Instance().Json?["_personalData"]?[0]?["_party"]?["_partyUnitList"]?.AsArray())
			{
				Party.Add(new Unit(node));
			}

			foreach (var node in SaveData.Instance().Json?["_inventory"]?["_items"]?.AsArray())
			{
				Items.Add(new Item(node));
			}

			foreach (var node in SaveData.Instance().Json?["_unitData"]?["_units"]?.AsArray())
			{
				Characters.Add(new Character(node, Party));
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

		private void ChoiceCharacter(object? parameter)
		{
			var ch = parameter as Character;
			if (ch == null) return;

			var dlg = new ChoiceWindow();
			dlg.Type = ChoiceWindow.eType.eCharacter;
			dlg.ID = uint.Parse(ch.ID);
			dlg.ShowDialog();
			ch.ID = dlg.ID.ToString();
		}
	}
}
