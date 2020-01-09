using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getid : MonoBehaviour {

	public List<int> id = new List<int>();

	void Start () {
		
		id = DataSave1.getInstance._itemIdList;
	}

	void Update () {
		
		
	}
}
