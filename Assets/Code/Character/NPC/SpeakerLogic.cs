﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Code.Dialog;
using UnityEngine;

public class SpeakerLogic : MonoBehaviour {
	
	// Use this for initialization
	public DialogueNode CurrentNode { get;  set; }
	private bool touching = false;
	public NormalCharacter speakTo { get; set; }
	public NormalCharacter me { get; set; }
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name.Equals("Hero")) {
			speakTo = other.gameObject.GetComponent<NormalCharacter>();
			me = this.gameObject.transform.parent.gameObject.GetComponent<NormalCharacter>();
			DialogWatcher.Instance.SetDialogContent(this);
		}
	}
	public void Start() {
		Debug.Log(DialogueSystem.Instance);
		CurrentNode = DialogueSystem.Instance.GetNode("ChiefHelloWorld");
	}
	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.name.Equals("Hero")) {
			speakTo = null;
			DialogWatcher.Instance.ReleaseContent(this);
		}
	}
	private void GetResult(int x) {
		
	}

	public bool TryTransfer() {
		DialogueNode dl = null;
		foreach (var i in CurrentNode.NextDialogueNode) {
			dl = DialogueSystem.Instance.MakeTransfer(this.CurrentNode, i.TranseferName,
			DialogueSystem.Instance.GetDefaultTransParams(i.TranseferName));

			break;
		}

		if (dl != null) {
			CurrentNode = dl;
			return true;
		}
		else {
			return false;			
		}
	}
	public bool TryTransferSpecified(string transnum,params object[] objects) {
		DialogueNode dl = DialogueSystem.Instance.MakeTransfer(this.CurrentNode,transnum,objects);
		if (dl != null) {
			CurrentNode = dl;
			return true;
		}
		else {
			return false;			
		}
	}
	void Update () {
	}
}
