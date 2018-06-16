using System;
using System.Collections;
using Code.Dialog;
using GameComp.System.MapSystem;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

//游戏生命周期
namespace GameComp.System.LifeCycleSystem {
	[CreateAssetMenu(menuName =  "GameCom/LifeCycleController")]
	public class LifeCycle : ScriptableObject {
		public GameLoader GameLoader;
		private static LifeCycle _instance;
		public static LifeCycle Instance {
			get {                 
				if (_instance == null) {
					_instance = Resources.Load<LifeCycle>("System/"+nameof(LifeCycle));
				}
				return _instance; 
			}
			set { _instance = value; }
		}
		private void OnEnable() {
			if (LifeCycle.Instance == null) {
				Instance = this;
			}
		}

		public float Progress { get; set; }
		public string ProgressState { get; set; }
		public IEnumerator StartGame() {
			var i =  GameLoader.LoadGame();
			while (i.MoveNext()) {
				Progress = GameLoader.LoadingProgress;
				ProgressState = GameLoader.LoadingState;
				yield return new WaitForEndOfFrame();
			}
			Progress = GameLoader.LoadingProgress;
			ProgressState = GameLoader.LoadingState;
			yield return  new WaitForEndOfFrame();
		}
	}
}
