using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Jinbi : MonoBehaviour {
	GameObject  _target;
	public bool _isCanMove=false ;
	public float _jinBiSpeed=400;
	Map _map;
	Vector2 pos;
	// Use this for initialization
	void Start () {
		_target = GameObject.FindGameObjectWithTag ("Player");
		_map = GameObject.FindObjectOfType<Map> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_isCanMove) {
			transform.position = Vector2.MoveTowards
				(transform.position, _target.transform.position, Time.deltaTime * _jinBiSpeed);
		}

	}
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
