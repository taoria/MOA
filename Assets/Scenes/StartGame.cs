using System.Collections;
using System.Collections.Generic;
using GameComp.System.LifeCycleSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
	public Canvas LoadingUI;
	public void Jump() {
		this.GetComponent<CanvasGroup>().alpha = 0;
		this.GetComponent<CanvasGroup>().blocksRaycasts = false;
		this.GetComponent<CanvasGroup>().interactable = false;
		LifeCycle lifeCycle = LifeCycle.Instance;
		LoadingUI.GetComponent<CanvasGroup>().alpha = 1;
		StartCoroutine(lifeCycle.StartGame());
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
