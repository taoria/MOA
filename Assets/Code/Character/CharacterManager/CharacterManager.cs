using System;
using System.Collections.Generic;
using Code.Item.Data;
using GameComp.System.MapSystem;
using UnityEngine;
using CharacterInfo = Code.Item.Data.CharacterInfo;
//角色类型：CharacterType 角色的类型名称
namespace Code.Character.CharacterManager {
	[CreateAssetMenu(menuName = "GameCom/CharacterManager")]
	public class CharacterManager : ScriptableObject {
		private Dictionary<string, BaseCharacter> _baseCharactersPool;
		//
		public static CharacterManager Instance;
		public Vector2 DefaultSpawnPosition;
		//创建角色在默认位置
		public void CreateCharacter(CharacterInfo of) {
			CreateCharacterAt(of,DefaultSpawnPosition);
		}

		private void OnEnable() {
			if (Instance == null)
				Instance = this;
			if(_baseCharactersPool==null)
				_baseCharactersPool = new Dictionary<string, BaseCharacter>();
		}

		//创建角色在指定位置
		public GameObject CreateCharacterAt(CharacterInfo of,Vector2 v) {
			GameObject gameObject = Resources.Load<GameObject>("Character/"+of.CharacterOutlook);
			GameObject.Instantiate(gameObject);
			if (of.CharacterDic.dictionary.ContainsKey("ScriptType")) {
				Type t = Type.GetType(of.CharacterDic.dictionary["ScriptType"]);
				gameObject.AddComponent(t);
			}
			gameObject.GetComponent<CharacterStatus>().LoadFromCharacterInfo(of);
			gameObject.transform.position = v;
			_baseCharactersPool.Add(gameObject.name,gameObject.GetComponent<BaseCharacter>());
			return gameObject;
		}
		//根据地图提供的信息创建角色
		public void CreateCharacterFromCharacterPreInfo(CharacterPreInfo cpf) {
			GameObject gameObject = CreateCharacterAt(cpf.CharacterInfo,cpf.position);
			gameObject.name = cpf.CharacterName;
			gameObject.GetComponent<CharacterStatus>().ObjectName = cpf.CharacterName;
		
		}
		//根据提供的实际数据反向生成角色
		public GameObject CreateCharacterFromObjectStatusData(CharacterStatusData data) {
			GameObject gameObject = Resources.Load<GameObject>("Character/"+data.CharacterOutlook);
			GameObject.Instantiate(gameObject);
			Type t = Type.GetType(data.CharacterScriptType);
			gameObject.AddComponent(t);
			gameObject.GetComponent<CharacterStatus>()._characterStatusData = data;
			_baseCharactersPool.Add(gameObject.name,gameObject.GetComponent<BaseCharacter>());
			return gameObject;
		}

		public void SaveCharacterToSaveFile(BaseCharacter bc,SaveData sv) {
	
		}

		public void LoadCharactersFromSaveFile(SaveData sv) {
		
		}
		//Unfinished Work
		public void LoadCharactersFromMapInfo(MapInfo mapInfo) {
			if (mapInfo.CharacterInfoDic == null || mapInfo.CharacterInfoDic.Length == 0) return;


		}
		
		public void ClearCharacterOnScene() {
			foreach (var v in _baseCharactersPool) {
				GameObject.Destroy(v.Value.gameObject);
			}
			_baseCharactersPool.Clear();
		
		}
		public void LoadCharacters(SaveData sv) {
		
		}
	
	
		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
		
		}
	}
}
