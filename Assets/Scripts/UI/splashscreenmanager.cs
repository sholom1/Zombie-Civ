using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashscreenmanager : MonoBehaviour {
	public GameObject text1;
	public GameObject text2;
	public GameObject text3;
	public GameObject selection;
	// Use this for initialization
	void Start () {
		Invoke ("title", 2);		
	}

	void title(){
		text1.SetActive (true);
		Invoke ("description", 2);
	}
	void description(){
		text2.SetActive (true);
		Invoke ("names", 2);
	}
	void names (){
		text3.SetActive (true);
		Invoke ("endsplashscreen", 2);
	}
	void endsplashscreen(){
		selection.SetActive (true);
		gameObject.SetActive (false);
	}
}
