using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yang{
	public int _A{ get{ return 0;} set{ if(value >= 0) _a = value;}}
	public int _a;
}

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {

		test a = gameObject.AddComponent<test>();
		Debug.Log (a);

		Yang y = new Yang ();
		y._A = 5;
		Debug.Log (y._A);

		y._a = 5;
		Debug.Log (y._a);

		Vector3 vec = new Vector3 (1, 1, 1);
		vec = new Vector3 (5, 5, 5);
		GetComponent<BoxCollider>().center = new Vector3(10, 10, 10);
		GetComponent<Transform> ();
		transform.position = transform.position;

		Yang b = null;
		Yang b2 = b;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
