using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	 public Transform _player;
	  public float _speed=0.5f;

	void Start () {
		_player = gameObject.GetComponent<Transform>();
	}

	void Update () {
		if (Input.GetKey (KeyCode.UpArrow))
		{
			_player.Translate (Vector2.up* _speed, Space.Self);
		}
		if (Input.GetKey (KeyCode.DownArrow))
		{
			_player.Translate (Vector2.down* _speed, Space.Self);
		}
		if (Input.GetKey (KeyCode.LeftArrow))
		{
			_player.Translate (Vector2.left* _speed, Space.Self);
		}
		if (Input.GetKey (KeyCode.RightArrow))
		{
			_player.Translate (Vector2.right* _speed, Space.Self);
		}
	}
}
