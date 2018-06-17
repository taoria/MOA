using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTrigger : MonoBehaviour {
	public NormalCharacter NormalCharacter;
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name.Equals("Map")) {
			NormalCharacter.Landing = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.name.Equals("Map")) {
			NormalCharacter.Landing = false;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}