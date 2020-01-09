using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sington : MonoBehaviour {

	private static Sington _instance;

	void Awake(){
		_instance = this;
	}

	public static Sington GetInstance(){
		if (_instance == null) {
			GameObject obj = new GameObject ();
			_instance = obj.AddComponent<Sington>();
		}
		return _instance;
	}
}
