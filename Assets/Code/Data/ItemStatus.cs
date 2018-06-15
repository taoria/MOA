using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Item {
	[CreateAssetMenu(menuName = "GameInfo/Item")]
	public class ItemStatus :ScriptableObject {
		public string ItemName;

		private Dictionary<string, float> ItemStatusIndex {
			get { return ItemStatusDic.dictionary; }
		}
		public ItemDic ItemStatusDic = ItemDic.New<ItemDic>();
	}
	[Serializable]
	public class ItemDic : SerializableDictionary<string, float> {
		
	}
	
}
