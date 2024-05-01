using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace EiyudenChronicle
{
	internal class Character : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private readonly JsonNode mNode;
		private readonly ObservableCollection<Unit> mParty;
		public Character(JsonNode node, ObservableCollection<Unit> party)
		{
			mNode = node;
			mParty = party;
		}

		public String ID
		{
			get => mNode["_id"].ToString();
			set
			{
				var old = mNode["_id"].ToString();
				mNode["_id"] = value;

				// update unit id
				foreach (var unit in mParty)
				{
					if(old == unit.ID)
					{
						unit.ID = value;
						break;
					}
				}

				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));

			}
		}

		public String Exp
		{
			get => mNode["_exp"].ToString();
			set
			{
				mNode["_exp"] = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
			}
		}
	}
}
