using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace EiyudenChronicle
{
	internal class SaveData
	{
		private static SaveData mThis = new SaveData();
		public JsonNode? Json { get; private set; }
		private String mFileName = "";

		private SaveData() { }

		public static SaveData Instance()
		{
			return mThis;
		}

		public bool Open(String filename)
		{
			if (System.IO.File.Exists(filename) == false) return false;

			var buffer = System.IO.File.ReadAllText(filename);
			Json = JsonNode.Parse(buffer);
			mFileName = filename;
			return true;
		}

		public void Save()
		{
			if (Json == null) return;
			if (String.IsNullOrEmpty(mFileName)) return;

			System.IO.File.WriteAllText(mFileName, Json.ToString());
		}
	}
}
