using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml.Serialization;
using Code.Item.Data;
using UnityEngine;
[Serializable]
public class SaveData {
	[SerializeField]
	//角色动态信息
	public Dictionary<string, CharacterStatusData> _objectStatuseDataDic = new Dictionary<string, CharacterStatusData>();
	[SerializeField]
	public string CurrentMap = "Village";
	public void Save() {
		StreamWriter streamWriter= new StreamWriter("save.sav");
		String res = JsonConvert.SerializeObject(this);
		streamWriter.Write(res);
		streamWriter.Close();
	}
	void AddData() {
		
	}

	public float loadingData;
	private Task<string> _readerBuffer;
	public IEnumerator LoadSaveFile(string name) {
		StreamReader streamReader = new StreamReader("save.sav");
		
		_readerBuffer = streamReader.ReadToEndAsync();
		while (_readerBuffer.IsCompleted == false) {
			yield return 0;
		}
		SaveData saveData = JsonConvert.DeserializeObject<SaveData>(_readerBuffer.Result);
		this._objectStatuseDataDic = saveData._objectStatuseDataDic;
		yield return 0;
	}
}
