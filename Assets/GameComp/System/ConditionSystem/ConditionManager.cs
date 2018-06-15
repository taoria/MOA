using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//条件管理器
public class ConditionManager : ScriptableObject {
	public ConditionManager Instance;
	private Dictionary<string, ConditionFunction> _conditionPool;
	private void OnEnable() {
		if (Instance == null)
			Instance = this;
		Reigster(nameof(IsFirstTimeMeeting),IsFirstTimeMeeting);
		Reigster(nameof(IsCharacterAPlayer),IsCharacterAPlayer);
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="BaseCharacter"></param>
	/// <returns></returns>
	public  bool IsCharacterAPlayer(params object[] objects) {
		if (objects.Length != 0) {
			BaseCharacter baseCharacter = (BaseCharacter) objects[0];
			return baseCharacter.gameObject.name.Equals("Hero");
		}

		return false;
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="CharacterA ,CharacterB"></param>
	/// <returns></returns>
	public bool IsFirstTimeMeeting(params object[] objects) {
		NormalCharacter charA = (NormalCharacter) objects[0];
		NormalCharacter charB = (NormalCharacter) objects[1];
		return charA.KnowSomeone(charB)||charB.KnowSomeone(charA);
	}
	public delegate bool ConditionFunction(params object[] objects);
	public void Reigster(string conditionName,ConditionFunction condition) {
		if (_conditionPool.ContainsKey(conditionName)) {
			_conditionPool.Add(conditionName,condition);
		}
	}
	public bool JudgeCondtion(string str,object[] objects) {
		if (_conditionPool.ContainsKey(str)) {
			return _conditionPool[str](str, objects);
		}
		
		else {
			Debug.LogError("Unknown condition name:" + str);
		}
		return false;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
