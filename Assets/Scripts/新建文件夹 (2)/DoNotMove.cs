using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotMove : MonoBehaviour {

	Map _map;
	Vector2 pos;
	void Start () {
		_map = GameObject.FindObjectOfType<Map> ();
	}
	void Update () {
		Change ();
	}
	/// <summary>
	/// 玩家获取道具
	/// </summary>
	public void Change(){
			pos = transform.position;
		_map.map [_map.GetGrid(pos).Rows,_map.GetGrid(pos).Columns] = 2;
		//_map.map [_map.GetGrid(pos).Rows,_map.GetGrid(pos).Columns] = 99;
			
			//Destroy (gameObject);
	}
}
