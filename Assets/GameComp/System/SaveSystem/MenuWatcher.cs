using System.Collections;
using System.Collections.Generic;
using Code.Item.Data;
using UnityEngine;

public class MenuWatcher : BaseWatcher {
	public void Save() {
		GameObject hero = GameObject.Find("Hero");
		SaveData sv = new SaveData();
		hero.GetComponent<CharacterStatus>().SaveToSaveData(sv);
		sv.Save();
	}

	public void Load() {
		SaveData sv = new SaveData();
		sv.LoadSaveFile("save.sav");
		GameObject hero = GameObject.Find("Hero");
		hero.GetComponent<CharacterStatus>().LoadFromSaveData(sv);
	}
	
	// Use this for initialization
	void Start () {
		base.Start();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
