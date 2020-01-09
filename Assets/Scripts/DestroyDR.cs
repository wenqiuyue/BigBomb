using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DestroyDR : MonoBehaviour {
	public GameObject success;
	public Text _kills;
	int a;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Bombxiaoguo") {
			Destroy (gameObject);
			success.SetActive (true);
			a=GameObject.FindObjectOfType<Player1> ()._kills1=0;
			_kills.text =a.ToString ();
			GameObject.Find ("player").GetComponent<Move2> ().enabled = false;
			GameObject.Find ("player").GetComponent<Player1> ().enabled = false;
		}
}
}
