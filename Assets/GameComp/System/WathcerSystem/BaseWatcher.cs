using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseWatcher : MonoBehaviour {
	public void TurnOff() {
		Showing = false;
		GetComponent<RectTransform>().localScale=new Vector3(0,0,0);
	//	GetComponent<CanvasGroup>().blocksRaycasts = true;    
	}

	public void TurnOn() {
		Showing = true;
		GetComponent<RectTransform>().localScale=new Vector3(1,1,1);
	//	GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
	public void SwitchWatcher() {
		if (Showing) {
			TurnOff();
		}
		else {
			TurnOn();
		}
	}

	private bool Showing { get; set; }

	// Use this for initialization
	public void Start () {
		TurnOff();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
