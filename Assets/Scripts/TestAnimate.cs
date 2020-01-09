using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimate : MonoBehaviour {

	public float startTime;
	public float perTime;
	public int animateIndex = 0;


	public SpriteRenderer _render;

	public Sprite[] _animateArray;



	// Use this for initialization
	void Start () {
		Invoke ("PlayAnimate", startTime);
	}
	
	void PlayAnimate(){
		if (animateIndex + 1 == _animateArray.Length) {
			animateIndex = 0;
			Invoke ("Invisible", 0.05f);
			return;
		}
		animateIndex = (animateIndex + 1) % _animateArray.Length;
		_render.sprite = _animateArray[animateIndex];
		Invoke ("PlayAnimate", perTime);
		_render.enabled = true;
	}

	void Invisible(){
		_render.enabled = false;
		Invoke ("PlayAnimate", 1);
	}
}
