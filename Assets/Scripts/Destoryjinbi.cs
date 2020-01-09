using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoryjinbi : MonoBehaviour {
	//地图
	Map _map;
	Vector2 pos;
	void Start () {
		_map = GameObject.FindObjectOfType<Map> ();
	}
	/// <summary>
	/// 玩家获取道具
	/// </summary>
	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			//Debug.Log ("碰撞了");
			pos = transform.position;
			_map.map [_map.GetGrid(pos).Rows,_map.GetGrid(pos).Columns] = 1;
			//Debug.Log (_map.GetGrid(pos).Rows+" " +_map.GetGrid (pos).Columns);
			Destroy (gameObject);
		}

	}
}
