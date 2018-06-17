using System;
using System.Collections;
using System.Collections.Generic;
using GameComp.System.MapSystem;
using GameComp.System.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "GameCom/GameLoader")]
public class GameLoader : ScriptableObject {
	public float LoadingProgress;
	public string LoadingState;
	private static GameLoader _instance;
	public static GameLoader Instance {
		get {
			if (_instance == null) {
				_instance = Resources.Load<GameLoader>("System/"+nameof(GameLoader));
			}
			return _instance;
		}
		set { _instance = value; }
	}
	

	public LoadConfig LoadConfig;
	private void OnEnable() {
		if (Instance == null) {
			Instance = this;
		}
	}
	//Fake Game Loader
	public IEnumerator LoadGame() {
		LoadingState = "初始化存档数据";
		SaveData svData = new SaveData();
		yield return new WaitForEndOfFrame();
		LoadingState = "读取游戏场景";
		AsyncOperation op = SceneManager.LoadSceneAsync(1);
		
		//当载入进度完成时
		op.completed += (x) => {
			MapManager.Instance.ShowMap();
		};
		op.allowSceneActivation = false;
		while (op.progress < 0.9f) {
			LoadingProgress = 0.05f + op.progress * 0.25f;
			yield return LoadingProgress;
		}
		//上面的是进度条,然而东西太少了一下就载完了,因此加了一个假进度条功能.
		while (LoadingProgress < 0.3f) {
			LoadingProgress += 0.01f;
			yield return LoadingProgress;
		}
		yield return op;
		LoadingState = "读取游戏数据";
		IEnumerator i = svData.LoadSaveFile(LoadConfig.ConfigName);
		while (i.MoveNext()) {
			LoadingProgress += 0.01f;
			yield return LoadingProgress;
		}
		while (LoadingProgress < 0.6f) {
			LoadingProgress += 0.01f;
			yield return LoadingProgress;
		}

		LoadingState = "载入游戏地图数据";
		Debug.Log(MapManager.Instance);
		
		var v = MapManager.Instance.LoadMap(LoadConfig.StartingMap);
		while (v.MoveNext()) {
			LoadingProgress += 0.01f;
			yield return LoadingProgress;
		}
		LoadingState = "载入剩余资源";
		while (LoadingProgress < 1f) {
			LoadingProgress += 0.01f;
			yield return LoadingProgress;
		}
		
		LoadingState = "载入完毕";
		LoadingProgress = 1;
		yield return new WaitForEndOfFrame();
		op.allowSceneActivation = true;
	
	}
}
