using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destory1 : MonoBehaviour {
	//定义地图
	Map _map;
	//位置
	Vector2 pos;
	void Start () {
	//获取map脚本
		_map = GameObject.FindObjectOfType<Map> ();
	}
	/// <summary>
	/// 爆炸效果触碰障碍物后销毁障碍物
	/// </summary>
	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Bombxiaoguo") {
			//传入当前位置转化为行列值，并将行列位置置为1
			pos = transform.position;
			_map._Map [_map.GetGrid(pos).Rows,_map.GetGrid(pos).Columns] = 1;
			//输出当前位置的行列值
			Debug.Log (_map.GetGrid(pos).Rows+" " +_map.GetGrid (pos).Columns);
			//摧毁障碍物
			Destroy (gameObject);
		}
	}
}

