using System;
using System.Collections.Generic;
using UnityEngine;

// 
//人物的预设属性数据,存在某个字典序列
namespace Code.Item.Data {
	[Serializable]
	public class CharacterDic : SerializableDictionary<string,string>{}
	[CreateAssetMenu(menuName = "GameInfo/Character")]
	public class CharacterInfo : ScriptableObject {
		public string CharacterOutlook;
		private Dictionary<string, string> ItemStatusIndex {
			get { return CharacterDic.dictionary; }
		}
		public CharacterDic CharacterDic= CharacterDic.New<CharacterDic>();
	}
	[Serializable]
	public class CharacterPreInfo  {
		public CharacterInfo CharacterInfo;
		public Vector2 position;
		public string CharacterName;
	}
}
