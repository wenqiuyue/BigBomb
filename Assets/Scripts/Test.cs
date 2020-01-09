using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	class TestInt{
		public int _a;
		public TestInt(int a){
			_a = a;
		}
	}

	// Use this for initialization
	void Start () {
		List<TestInt> list = new List<TestInt> ();
		TestInt t;
		for(int i = 0; i < 10; i++){
			t = list[i];
			t._a = i + 1;
			list.Add (t);


		}

		for(int i = 0; i < list.Count; i++){
			Debug.Log (list[i]._a);
		}
		Debug.Log (list[0] == list[1]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
