using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerLogic : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	private List<string> list = new List<string>();
	private void OnTriggerEnter2D(Collider2D other) {
	
		if (other.gameObject.name.Equals("Hero")) {
			//获取对话框
			Debug.Log("Hero");
			DialogWatcher dialogWatcher  = GameObject.FindWithTag("DialogBox").GetComponent<DialogWatcher>();
			dialogWatcher.SetDialogContent("HUMBLE to SKYLORD,I am the tribe leader Wutaka,We summon you here to save us from YOUR enemy.");
			dialogWatcher.StartSpeaking();
			
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.name.Equals("Hero")) {
			//获取对话框
			Debug.Log("Hero");
			DialogWatcher dialogWatcher  = GameObject.FindWithTag("DialogBox").GetComponent<DialogWatcher>();
			dialogWatcher.CloseWatcher();
			
		}
	}

	//启动
	// Update is called once per frame
	void Update () {
	
	}
}
