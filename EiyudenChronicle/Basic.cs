using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EiyudenChronicle
{
	internal class Basic
	{
		public String Money
		{
			get
			{
				var node = SaveData.Instance().Json?["_money"];
				if (node == null) return "";
				return node.ToString();
			}
			set
			{
				var node = SaveData.Instance().Json;
				if (node == null) return;
				node["_money"] = value;
			}
		}
	}
}
