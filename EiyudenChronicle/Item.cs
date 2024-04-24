using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace EiyudenChronicle
{
	internal class Item : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private readonly JsonNode mNode;
		public Item(JsonNode node)
		{
			mNode = node;
		}

		public String ID
		{
			get => mNode["_id"].ToString();
			set
			{
				mNode["_id"] = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
			}
		}

		public String Count
		{
			get => mNode["_count"].ToString();
			set => mNode["_count"] = value;
		}
	}
}
