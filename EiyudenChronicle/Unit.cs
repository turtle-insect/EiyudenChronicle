using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace EiyudenChronicle
{
	internal class Unit : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private readonly JsonNode mNode;

		public Unit(JsonNode node)
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
	}
}
