using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//开始对话 对话循环 结束对话
//对话监视器只负责显示对话,输送交互,不负责对话的逻辑
public class DialogWatcher : BaseWatcher {
	public GameObject DialogGameObject;
	// Use this for initialization
	private string _currentWords;
	//对话是否结束
	private bool _finishedSpeaking = false;
	private bool _finishedDialogue = false;
	private int _dialogueRoller;
	private bool IsBusy = false;
	private SpeakerLogic _speakerLogic;
	//设置当前对话的内容.
	//设置说话的内容
	public void SetDialogContent(SpeakerLogic speakerLogic) {
		if(this._speakerLogic==null)
			this._speakerLogic = speakerLogic;
	}
	//再次说话
	public void RerollDialogue() {
		if (_speakerLogic.CurrentNode.Words.Count > ++_dialogueRoller) {
			Speaking();
		}
		else {
			_speakerLogic.TryTransfer();
			Speaking();
		}

	}
	//尝试启动对话
	//等待下一帧.
	public static DialogWatcher Instance { get; set; }

	
	IEnumerator TalkLinear() {
		while (speakRollerCol < _currentWords.Length) {
			GameObject.FindGameObjectWithTag("dialog").GetComponent<Text>().text = _currentWords.Substring(0, speakRollerCol);
			speakRollerCol++;
			yield return new WaitForSeconds(0.03f);
		}
		GameObject.FindGameObjectWithTag("dialog").GetComponent<Text>().text = _currentWords;
		_finishedSpeaking = true;
		if (_dialogueRoller >= _speakerLogic.CurrentNode.Words.Count-1) {
			_finishedDialogue = true;
		}
	}
	void Speaking() {
		var DialogType = _speakerLogic.CurrentNode.DialogType;
		switch (DialogType) {
			case "Default": {
				_currentWords = _speakerLogic.CurrentNode.Words[_dialogueRoller];
				break;
			}
			case "Option": {
				_currentWords = null;
				int count = 1;
				foreach (var str in _speakerLogic.CurrentNode.Words) {
					_currentWords += $"{count}.{str}\n";
					count++;
				}
				break;
			}
			case "Request": {
				break;
			}
		}
		_finishedSpeaking = false;
		_finishedDialogue = false;
		speakRollerCol = 0;
		StartCoroutine(TalkLinear());
	}
	void Start() {
		base.Start();
		InitWatcherImm();
		if(Instance==null)
		Instance = this;
	}
	private void InitWatcherImm() {
		speakRollerCol = 0;
		_finishedSpeaking = false;
		_finishedDialogue = true;
		_dialogueRoller = 0;
		TurnOff();
	}
	private int speakRollerCol = 0;
	//释放资源
	public void ReleaseContent(SpeakerLogic speakerLogic) {
		if(_speakerLogic.Equals(speakerLogic))
			_speakerLogic = null;
	}

	private KeyCode[] optionsKey = new KeyCode[] {
		KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7,
		KeyCode.Alpha8
	};
	// Update is called once per frame
	void Update () {
		//玩家按下数字键:
		if (_speakerLogic != null) {
			if (_speakerLogic.CurrentNode.DialogType.Equals("Options")&&_finishedDialogue&&IsBusy==true) {
				for (int i = 0; i < optionsKey.Length; i++) {
					if (Input.GetKeyDown(optionsKey[i])) {
						_speakerLogic.TryTransferSpecified("Number"+i,null);
					}
				}
			}
		}
		//玩家按下F键
		if (Input.GetKeyDown(KeyCode.F)) {
			//存在对话逻辑,并且对话框空闲
			if (_speakerLogic != null &&IsBusy ==false) {
				//打开对话框
				TurnOn();
				IsBusy = true;
				Speaking();
				return;
			}
			//存在对话逻辑,但是已经在对话.
			if (_speakerLogic != null && _finishedDialogue == false) {
				//当前这句话还没讲完
				if (_finishedSpeaking==false) {
					//立刻说完
					speakRollerCol = _currentWords.Length;
				}
				else {
					//已经讲完的话,进入下一句对话.
					RerollDialogue();
				}
				return;
			}
			//话说完就关掉吧
			if (_speakerLogic == null || _finishedDialogue == true) {
				IsBusy = false;
				InitWatcherImm();
			}
		}
	}
}
