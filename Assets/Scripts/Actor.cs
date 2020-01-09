 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
	Rigidbody2D _rigidbody;
	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 pos = transform.position;
		if (Input.GetKey (KeyCode.W)) 
		{
			_rigidbody.MovePosition (pos+Vector2.up * 0.1f);
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			_rigidbody.MovePosition (pos+Vector2.down * 0.1f);
		}
		if (Input.GetKey (KeyCode.A)) 
		{
			_rigidbody.MovePosition (pos+Vector2.left * 0.1f);
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			_rigidbody.MovePosition (pos+Vector2.right * 0.1f);
		}

	
	}
















	//障碍物检测
	//检测前方
	/*bool DirectionForward()
	{
		bool check = false;
		RaycastHit hitForward;
		Vector3 LocalForward = 
			_transform.TransformPoint(Vector3.forward)-_transform.position;
		if (Physics.Raycast 
			(_transform.position,LocalForward, 
				out hitForward, _colliderDistance/2))
		{
			if(CompareTags(hitForward.transform.gameObject.tag))
			{
				_isZhangAiWu =true;
				check = true;
			}
		}
		return check;
	}
	//检测右方
	bool DirectionRight()
	{
		bool check = false;
		RaycastHit hitRight;
		Vector3 LocalRight = 
			_transform.TransformPoint(Vector3.right)-_transform.position;
		if (Physics.Raycast (_transform.position, 
			LocalRight, 
			out hitRight, _colliderDistance)) 
		{
			if(CompareTags(hitRight.transform.gameObject.tag))
			{
				_isZhangAiWu =true;
				check = true;
			}
		}
		return check;
	}
	//检测左方
	bool DirectionLeft()
	{
		bool check = false;
		RaycastHit hitLeft;
		Vector3 LocalLeft = 
			_transform.TransformPoint(-Vector3.right)-_transform.position;
		if (Physics.Raycast (_transform.position, 
			LocalLeft, 
			out hitLeft, _colliderDistance)) 
		{
			if(CompareTags(hitLeft.transform.gameObject.tag))
			{
				_isZhangAiWu =true;
				check = true;
			}
		}
		return check;
	}*/


}
