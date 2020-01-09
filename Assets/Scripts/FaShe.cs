using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FaShe : MonoBehaviour {
	public GameObject feibiao;
	public GameObject player;
	public float speed=30;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		//transform.DOMove (new Vector3 (3, 3,0),2);

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.J)){
			Instantiate (feibiao);
			this.transform.position = player.transform.position;
			/*if (player.isleft) {
				this.transform.position += new Vector3 (-speed * Time.deltaTime, this.transform.position.y, 0);
			}*/
		}
	}
}
