using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//开始对话 对话循环 结束对话
//对话监视器只负责显示对话,输送交互,不负责对话的逻辑
public class DialogWatcher : MonoBehaviour {
	public GameObject DialogGameObject;
	// Use this for initialization
	private string _currentWords;
	private bool finishedSpeaking = false;

	private bool invokedIn = false;
	//设置当前对话的内容.
	public void SetDialogContent(string words) {
		_currentWords = words;
	}
	//启用对话器
	public void StartSpeaking() {
		if (invokedIn == true) return;
		invokedIn = true;
		this.GetComponent<CanvasGroup>().alpha = 1;
		GetComponent<CanvasGroup>().interactable = false;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
		finishedSpeaking = false;
		speakRollerCol = 0;
	}

	void Start() {
		CloseWatcher();	
	}
	private int speakRollerCol = 0;
	void Speak() {
		
		speakRollerCol++;
		if (speakRollerCol <= _currentWords.Length ) {
			GameObject.FindGameObjectWithTag("dialog").GetComponent<Text>().text = _currentWords.Substring(0, speakRollerCol);
		}

		
		else {
			finishedSpeaking = true;
		}
	}
	//结束对话器
	public void CloseWatcher() {
		invokedIn = false;
		this.GetComponent<CanvasGroup>().alpha = 0;
		GetComponent<CanvasGroup>().interactable = true;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		speakRollerCol = 0;
	}

	// Update is called once per frame
	void Update () {
		//玩家按下F键
		if (Input.GetKeyDown(KeyCode.F)) {
			if (finishedSpeaking == false && invokedIn) {
				speakRollerCol = _currentWords.Length - 1;
			}
			else {
				CloseWatcher();
			}
		}
		//处于对话状态会说完这句话.
		if (finishedSpeaking==false&&invokedIn==true) {
			Speak();
		}
	}
}
