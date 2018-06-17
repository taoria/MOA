using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFire : MonoBehaviour {
	public NormalCharacter shoot;
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<NormalCharacter>() != null) {
			if(shoot!=null&&other.GetComponent<NormalCharacter>().Equals(shoot)==false)
				other.GetComponent<NormalCharacter>().Hurt(shoot.MeCharacterStatus.Int);
		}
		else {
			if(other.gameObject.tag.Equals("Terrain"))
			GameObject.Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
		StartCoroutine(WaitLasting(2));
	}

	private IEnumerator WaitLasting(float i) {
		yield return new WaitForSeconds(i);
		GameObject.Destroy(this.gameObject);		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
