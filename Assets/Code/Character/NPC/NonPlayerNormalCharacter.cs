using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonPlayerNormalCharacter : NormalCharacter {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	private List<string> lis = new List<string>();
	void Speak() {
		Text text = GameObject.FindWithTag("dialog").GetComponent<Text>();
		if (text != null) {
			text.text = "Hello world";
		}
	}
	
}
